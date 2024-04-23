/****************************************************
  文件：GameModule.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月22日 18:46:25
  功能：
*****************************************************/

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace ZeroFramework
{
    /// <summary>
    /// 游戏模块。
    /// </summary>
    public partial class GameModule : SingletonMono<GameModule>
    {
        private readonly Dictionary<Type, ModuleBehaviour> _moduleMaps = new Dictionary<Type, ModuleBehaviour>(ModuleLogicSystem.DesignModuleCount);

        private GameObject _gameModuleRoot;
        
        #region 框架模块

        /// <summary>
        /// 获取游戏基础模块。
        /// </summary>
        public RootModule Root
        {
            get => _root ??= Get<RootModule>();
            private set => _root = value;
        }

        private RootModule _root;
        #endregion
        
        /// <summary>
        /// 获取游戏框架模块类。
        /// </summary>
        /// <typeparam name="T">游戏框架模块类。</typeparam>
        /// <returns>游戏框架模块实例。</returns>
        public T Get<T>() where T : ModuleBehaviour
        {
            Type type = typeof(T);

            if (_moduleMaps.TryGetValue(type, out var ret))
            {
                return ret as T;
            }

            T module = ModuleBehaviourSystem.Instance.GetModule<T>();

            Log.Assert(condition: module != null, $"{typeof(T)} is null");

            _moduleMaps.Add(type, module);

            return module;
        }

        private void Start()
        {
            Log.Info("GameModule Active");
            gameObject.name = $"[{nameof(GameModule)}]";
            _gameModuleRoot = gameObject;
            // DontDestroyOnLoad(gameObject);
        }

        public void Shutdown(ShutdownType shutdownType)
        {
            Log.Info("GameModule Shutdown");
            if (_gameModuleRoot != null)
            {
                Destroy(_gameModuleRoot);
                _gameModuleRoot = null;
            }
            _moduleMaps.Clear();
            
            _root = null;
            // _debugger = null;
            // _fsm = null;
            // _procedure = null;
            // _objectPool = null;
            // _resource = null;
            // _audio = null;
            // _setting = null;
            // _ui = null;
            // _localization = null;
            // _scene = null;
            // _timer = null;
            // _resourceExt = null;
        }
    }
}