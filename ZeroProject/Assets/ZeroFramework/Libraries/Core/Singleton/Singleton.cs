/****************************************************
  文件：Singleton.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2021/11/10 22:47:13
  功能：单例模式基类（实现类一定要写私有无参构造，否则创建不了）
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroFramework
{
    /// <summary>
    /// 单例抽象基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> where T : Singleton<T>
    {
        /// <summary>
        /// 静态实例
        /// </summary>
        protected static T _instance;

        /// <summary>
        /// 标签锁：确保当一个线程位于代码的临界区时，另一个线程不进入临界区。
        /// 如果其他线程试图进入锁定的代码，则它将一直等待（即被阻止），直到该对象被释放
        /// </summary>
        protected static readonly object _lock = new object();

        /// <summary>
        /// 静态属性
        /// </summary>
        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = SingletonCreator.CreateSingleton<T>();
                    }
                }

                return _instance;
            }
        }
        
        private static int _initCount = 0;
        public int Instance_ID { get; private set; } = 0;
        
        /// <summary>
        /// 单例初始化方法
        /// </summary>
        public virtual void OnSingletonInit()
        {
            _initCount++;
            Instance_ID = _initCount;
        }
        
        /// <summary>
        /// 资源释放时调用
        /// </summary>
        protected virtual void OnDispose()
        {
            
        }
        
        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            OnDispose();
            _instance = null;
        }
    }
}
