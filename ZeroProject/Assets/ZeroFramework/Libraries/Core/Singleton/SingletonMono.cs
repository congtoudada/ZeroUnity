/****************************************************
  文件：SingletonMono.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月22日 22:30:37
  功能：
*****************************************************/

using System.Linq;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ZeroFramework
{
    public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
    {
        protected static T _instance;
        public static T Instance => _instance;
        
        private static int _initCount = 0;
        [ShowInInspector, ReadOnly] public int Instance_ID { get; private set; } = 0;
        
        /// <summary>
        /// 实现接口的单例初始化，只在创建的时候初始化一次
        /// </summary>
        protected virtual void OnSingletonInit()
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
            if (_instance == null) return;
            OnDispose();
            DestroyImmediate(gameObject);
            _instance = null;
        }
        
        /// <summary>
        /// 释放当前对象
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            Dispose();
        }

        //子类记得重写父类，并调用base.Awake()
        protected virtual void Awake()
        {
            //判断T实例存在的条件是否满足
            if (!Application.isPlaying)
                return;
            
            //TODO: 避免重复创建对象
            //判断当前场景中是否存在T实例，存在则调用初始化函数后返回
            if (UnityEngine.Object.FindObjectsOfType(typeof(T)) is T[] instances)
            {
                if (instances.Length > 1)
                {
                    var unuse = instances.Where(obj => obj.Instance_ID == 0).ToArray()[0];
                    if (unuse == this)
                    {
                        DestroyImmediate(gameObject);
                        return;
                    }
                }
            }

            _instance = this as T;
            DontDestroyOnLoad(gameObject);
            OnSingletonInit();
        }
    }
}