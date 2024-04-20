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

namespace ZeroFramework.Runtime
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
        protected static T mInstance;

        /// <summary>
        /// 标签锁：确保当一个线程位于代码的临界区时，另一个线程不进入临界区。
        /// 如果其他线程试图进入锁定的代码，则它将一直等待（即被阻止），直到该对象被释放
        /// </summary>
        static object mLock = new object();

        /// <summary>
        /// 静态属性
        /// </summary>
        public static T Instance
        {
            get
            {
                lock (mLock)
                {
                    if (mInstance == null)
                    {
                        mInstance = SingletonCreator.CreateSingleton<T>();
                    }
                }

                return mInstance;
            }
        }
        
        private static int _InitCount = 0;
        public int ID = 0;

        /// <summary>
        /// 资源释放
        /// </summary>
        public virtual void Dispose()
        {
            mInstance = null;
        }

        /// <summary>
        /// 单例初始化方法
        /// </summary>
        public virtual void OnSingletonInit()
        {
            _InitCount++;
            ID = _InitCount;
        }

        public virtual void ResetID()
        {
            _InitCount = 0;
            ID = 0;
        }

        public virtual int GetID()
        {
            return ID;
        }
    }
}
