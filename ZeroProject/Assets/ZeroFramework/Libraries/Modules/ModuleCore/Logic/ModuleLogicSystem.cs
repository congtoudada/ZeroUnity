/****************************************************
  文件：ModuleLogicSystem.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月22日 21:18:48
  功能：
*****************************************************/

using System;
using System.Collections.Generic;

namespace ZeroFramework
{
    /// <summary>
    /// 游戏框架模块Logic管理系统。
    /// </summary>
    public sealed class ModuleLogicSystem : Singleton<ModuleLogicSystem>
    {
        private ModuleLogicSystem()
        {
        }
        /// <summary>
        /// 默认设计的模块数量。
        /// <remarks>有增删可以自行修改减少内存分配与GCAlloc。</remarks>
        /// </summary>
        internal const int DesignModuleCount = 16;
        private const string ModuleRootNameSpace = "ZeroFramework.";
        //逻辑模块字典，根据类型快速找到ModuleLogic
        private readonly Dictionary<Type, ModuleLogic> _moduleMaps = new Dictionary<Type, ModuleLogic>(DesignModuleCount);
        //逻辑模块链表，根据优先级排序
        private readonly ZeroFrameworkLinkedList<ModuleLogic> _modules = new ZeroFrameworkLinkedList<ModuleLogic>();
        //逻辑模块update候选队列，根据优先级排序
        private readonly ZeroFrameworkLinkedList<ModuleLogic> _updateModules = new ZeroFrameworkLinkedList<ModuleLogic>();
        //逻辑模块update队列，根据优先级依次更新
        private readonly List<ModuleLogic> _updateExecuteList = new List<ModuleLogic>(DesignModuleCount);
        private bool _isExecuteListDirty;
        
        /// <summary>
        /// 所有游戏框架模块轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (_isExecuteListDirty)
            {
                _isExecuteListDirty = false;
                BuildExecuteList();
            }
            
            int executeCount = _updateExecuteList.Count;
            for (int i = 0; i < executeCount; i++)
            {
                _updateExecuteList[i].Update(elapseSeconds, realElapseSeconds);
            }
        }
        
        /// <summary>
        /// 关闭并清理所有游戏框架模块。
        /// </summary>
        public void Shutdown()
        {
            Log.Info("关闭ModuleLogicSystem...");
            for (LinkedListNode<ModuleLogic> current = _modules.Last; current != null; current = current.Previous)
            {
                current.Value.Shutdown();
            }

            _modules.Clear();
            _moduleMaps.Clear();
            _updateModules.Clear();
            _updateExecuteList.Clear();
            MemoryPool.Instance.ClearAll();
            Utility.Marshal.FreeCachedHGlobal();
        }
        
        /// <summary>
        /// 获取游戏框架模块。
        /// </summary>
        /// <typeparam name="T">要获取的游戏框架模块类型。</typeparam>
        /// <returns>要获取的游戏框架模块。</returns>
        /// <remarks>如果要获取的游戏框架模块不存在，则自动创建该游戏框架模块。</remarks>
        public T GetModule<T>() where T : class
        {
            Type module = typeof(T);

            if (module.FullName != null && !module.FullName.StartsWith(ModuleRootNameSpace, StringComparison.Ordinal))
            {
                throw new GameFrameworkException(Utility.Text.Format("You must get a Framework module, but '{0}' is not.", module.FullName));
            }

            string moduleName = Utility.Text.Format("{0}.{1}", module.Namespace, module.Name.Substring(1));
            Type moduleType = Type.GetType(moduleName);
            if (moduleType == null)
            {
                moduleName = Utility.Text.Format("{0}.{1}", module.Namespace, module.Name);
                moduleType = Type.GetType(moduleName);
                if (moduleType == null)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not find Game Framework module type '{0}'.", moduleName));
                }
            }

            return GetModule(moduleType) as T;
        }
        
        /// <summary>
        /// 获取游戏框架模块。
        /// </summary>
        /// <param name="moduleType">要获取的游戏框架模块类型。</param>
        /// <returns>要获取的游戏框架模块。</returns>
        /// <remarks>如果要获取的游戏框架模块不存在，则自动创建该游戏框架模块。</remarks>
        private ModuleLogic GetModule(Type moduleType)
        {
            return _moduleMaps.TryGetValue(moduleType, out ModuleLogic module) ? module : CreateModule(moduleType);
        }
        
        /// <summary>
        /// 创建游戏框架模块。
        /// </summary>
        /// <param name="moduleType">要创建的游戏框架模块类型。</param>
        /// <returns>要创建的游戏框架模块。</returns>
        private ModuleLogic CreateModule(Type moduleType)
        {
            ModuleLogic moduleLogic = (ModuleLogic)Activator.CreateInstance(moduleType);
            if (moduleLogic == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Can not create module '{0}'.", moduleType.FullName));
            }

            _moduleMaps[moduleType] = moduleLogic;

            LinkedListNode<ModuleLogic> current = _modules.First;
            while (current != null)
            {
                //找到第一个比当前节点优先级小的点返回
                if (moduleLogic.Priority > current.Value.Priority)
                {
                    break;
                }

                current = current.Next;
            }

            if (current != null)
            {
                _modules.AddBefore(current, moduleLogic);
            }
            else
            {
                _modules.AddLast(moduleLogic);
            }
            
            //如果有update特性，则将其添加到update候选队列指定优先级位置，标记为脏
            if (Attribute.GetCustomAttribute(moduleType, typeof(UpdateModuleAttribute)) is UpdateModuleAttribute updateModuleAttribute)
            {
                LinkedListNode<ModuleLogic> currentUpdate = _updateModules.First;
                while (currentUpdate != null)
                {
                    if (moduleLogic.Priority > currentUpdate.Value.Priority)
                    {
                        break;
                    }

                    currentUpdate = currentUpdate.Next;
                }

                if (currentUpdate != null)
                {
                    _updateModules.AddBefore(currentUpdate, moduleLogic);
                }
                else
                {
                    _updateModules.AddLast(moduleLogic);
                }
                
                _isExecuteListDirty = true;
            }

            return moduleLogic;
        }
        
        /// <summary>
        /// 构造执行队列。
        /// </summary>
        private void BuildExecuteList()
        {
            _updateExecuteList.Clear();
            _updateExecuteList.AddRange(_updateModules);
        }
    }
}