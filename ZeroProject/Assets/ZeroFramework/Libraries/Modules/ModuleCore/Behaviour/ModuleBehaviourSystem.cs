/****************************************************
  文件：ModuleSystem.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月22日 19:07:17
  功能：
*****************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZeroFramework
{
    /// <summary>
    /// 游戏框架模块Behaviour管理系统。
    /// </summary>
    public sealed class ModuleBehaviourSystem : Singleton<ModuleBehaviourSystem>
    {
        private readonly ZeroFrameworkLinkedList<ModuleBehaviour> _modules = new ZeroFrameworkLinkedList<ModuleBehaviour>();

        private ModuleBehaviourSystem()
        {
        }

        /// <summary>
        /// 游戏框架所在的场景编号。
        /// </summary>
        internal const int GameFrameworkSceneId = 0;
        
        /// <summary>
        /// 获取游戏框架模块。
        /// </summary>
        /// <typeparam name="T">要获取的游戏框架模块类型。</typeparam>
        /// <returns>要获取的游戏框架模块。</returns>
        public T GetModule<T>() where T : ModuleBehaviour
        {
            return (T)GetModule(typeof(T));
        }
        
        /// <summary>
        /// 获取游戏框架模块。
        /// </summary>
        /// <param name="type">要获取的游戏框架模块类型。</param>
        /// <returns>要获取的游戏框架模块。</returns>
        public ModuleBehaviour GetModule(Type type)
        {
            LinkedListNode<ModuleBehaviour> current = _modules.First;
            while (current != null)
            {
                if (current.Value.GetType() == type)
                {
                    return current.Value;
                }
                current = current.Next;
            }

            return null;
        }
        
        /// <summary>
        /// 获取游戏框架模块。
        /// </summary>
        /// <param name="typeName">要获取的游戏框架模块类型名称。</param>
        /// <returns>要获取的游戏框架模块。</returns>
        public ModuleBehaviour GetModule(string typeName)
        {
            LinkedListNode<ModuleBehaviour> current = _modules.First;
            while (current != null)
            {
                Type type = current.Value.GetType();
                if (type.FullName == typeName || type.Name == typeName)
                {
                    return current.Value;
                }

                current = current.Next;
            }

            return null;
        }
        
        /// <summary>
        /// 关闭游戏框架。
        /// </summary>
        /// <param name="shutdownType">关闭游戏框架类型。</param>
        internal void Shutdown(ShutdownType shutdownType)
        {
            Log.Info("关闭ModuleBehaviourSystem Mode:({0})...", shutdownType);
            Utility.Unity.Shutdown();
            // RootModule rootModule = GetModule<RootModule>();
            // if (rootModule != null)
            // {
            //     rootModule.Shutdown();
            //     rootModule = null;
            // }
            _modules.Clear();

            // GameModule.Instance.Shutdown(shutdownType);
            
            if (shutdownType == ShutdownType.None)
            {
                return;
            }

            if (shutdownType == ShutdownType.Restart)
            {
                SceneManager.LoadScene(GameFrameworkSceneId);
                return;
            }

            if (shutdownType == ShutdownType.Quit)
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }
        
        /// <summary>
        /// 注册游戏框架模块。
        /// </summary>
        /// <param name="moduleBehaviour">要注册的游戏框架模块。</param>
        public void RegisterModule(ModuleBehaviour moduleBehaviour)
        {
            if (moduleBehaviour == null)
            {
                Log.Error("TEngine Module is invalid.");
                return;
            }

            Type type = moduleBehaviour.GetType();

            LinkedListNode<ModuleBehaviour> current = _modules.First;
            while (current != null)
            {
                if (current.Value.GetType() == type)
                {
                    Log.Error("Game Framework component type '{0}' is already exist.", type.FullName);
                    return;
                }

                current = current.Next;
            }

            _modules.AddLast(moduleBehaviour);
        }
    }
}