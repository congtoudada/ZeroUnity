/****************************************************
  文件：SingletonMono.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2023/11/22 14:29:21
  功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroFramework.Runtime
{
    /// <summary>
    /// Mono单例抽象基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
    {
        /// <summary>
        /// 静态实例
        /// </summary>
        protected static T mInstance;

        /// <summary>
        /// 静态属性：封装相关实例对象
        /// </summary>
        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = SingletonCreator.CreateMonoSingleton<T>();
                }

                return mInstance;
            }
        }
        
        private static int _InitCount = 0;
        public int ID = 0;

        /// <summary>
        /// 实现接口的单例初始化，只在创建的时候初始化一次
        /// </summary>
        public virtual void OnSingletonInit()
        {
            _InitCount++;
            ID = _InitCount;
        }

        /// <summary>
        /// 资源释放
        /// </summary>
        public virtual void Dispose()
        {
            if (mInstance == null) return;
            DestroyImmediate(gameObject);
            mInstance = null;
        }
        
        /// <summary>
        /// 释放当前对象
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            Dispose();
        }
        
        /// <summary>
        /// 重置ID
        /// </summary>
        public virtual void ResetID()
        {
            _InitCount = 0;
            ID = 0;
        }
        
        /// <summary>
        /// 单例ID
        /// </summary>
        /// <returns></returns>
        public virtual int GetID()
        {
            return ID;
        }
    }
}
