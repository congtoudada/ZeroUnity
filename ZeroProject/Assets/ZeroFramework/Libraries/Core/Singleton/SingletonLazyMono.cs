/****************************************************
  文件：SingletonLazyMono.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2023/11/22 14:29:21
  功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ZeroFramework
{
    /// <summary>
    /// Mono单例抽象基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonLazyMono<T> : MonoBehaviour where T : SingletonLazyMono<T>
    {
        /// <summary>
        /// 静态实例
        /// </summary>
        private static T _instance;

        /// <summary>
        /// 静态属性：封装相关实例对象
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = SingletonCreator.CreateMonoSingleton<T>();
                }

                return _instance;
            }
        }
        
        private static int _initCount = 0;
        [ShowInInspector, ReadOnly] public int Instance_ID { get; private set; } = 0;

        /// <summary>
        /// 实现接口的单例初始化，只在创建的时候初始化一次
        /// </summary>
        public virtual void OnSingletonInit()
        {
            _initCount++;
            Instance_ID = _initCount;
        }

        /// <summary>
        /// 当资源释放时调用
        /// </summary>
        public virtual void OnDispose()
        {
            
        }
        
        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            OnDispose();
            DestroyImmediate(gameObject);
            _instance = null;
        }
        
        /// <summary>
        /// 释放当前对象
        /// </summary>
        protected void OnApplicationQuit()
        {
            Dispose();
        }
    }
}
