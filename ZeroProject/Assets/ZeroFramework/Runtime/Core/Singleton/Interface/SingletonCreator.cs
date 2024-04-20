/****************************************************
  文件：SingletonCreator.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2023-11-30 16:24:56
  功能：
*****************************************************/

using System;
using System.Reflection;
using UnityEngine;

namespace ZeroFramework.Runtime
{
    /// <summary>
    /// 单例生成器，用于创建单例对象
    /// </summary>
    public static class SingletonCreator
    {
        /// <summary>
        /// 调用T类型的非public构造函数，用于生成单例对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static T CreateNonPublicConstructorObject<T>() where T : class
        {
            var type = typeof(T);
            // 获取私有构造函数
            var constructorInfos = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            // 获取无参构造函数
            var ctor = Array.Find(constructorInfos, c => c.GetParameters().Length == 0);

            if (ctor == null)
            {
                throw new Exception("Non-Public Constructor() not found! in " + type);
            }

            return ctor.Invoke(null) as T;
        }
        
        /// <summary>
        /// 创建单例对象：Mono单例或非Mono单例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateSingleton<T>() where T : Singleton<T>
        {
            var type = typeof(T);
            var instance = CreateNonPublicConstructorObject<T>();
            instance.OnSingletonInit();
            return instance;
            // var monoBehaviourType = typeof(MonoBehaviour);
            //
            // if (monoBehaviourType.IsAssignableFrom(type))
            // {
            //     return CreateMonoSingleton<T>();
            // }
            // else
            // {
            //     var instance = CreateNonPublicConstructorObject<T>();
            //     instance.OnSingletonInit();
            //     return instance;
            // }
        }
        
        /// <summary>
        /// 创建Mono单例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateMonoSingleton<T>() where T : SingletonMono<T>
        {
            T instance = null;
            var type = typeof(T);

            //判断T实例存在的条件是否满足
            if (!Application.isPlaying)
                return instance;

            //判断当前场景中是否存在T实例，存在则调用初始化函数后返回
            instance = UnityEngine.Object.FindObjectOfType(type) as T;
            if (instance != null)
            {
                instance.OnSingletonInit();
                return instance;
            }
            
            //根据路径特性，创建单例对象
            //MemberInfo：获取有关成员属性的信息并提供对成员元数据的访问
            MemberInfo info = typeof(T);
            //获取T类型 自定义属性，并找到相关路径属性，利用该属性创建T实例
            var attributes = info.GetCustomAttributes(true);
            foreach (var atribute in attributes)
            {
                var defineAttri = atribute as MonoSingletonPathAttribute;
                if (defineAttri == null)
                {
                    continue;
                }
                instance = CreateComponentOnGameObject<T>(defineAttri.PathInHierarchy, true);
                break;
            }

            //如果没有声明路径特性，则主动去创建同名Obj 并挂载相关脚本 组件
            if (instance == null)
            {
                var obj = new GameObject(typeof(T).Name);
                UnityEngine.Object.DontDestroyOnLoad(obj);
                instance = obj.AddComponent(typeof(T)) as T;
            }

            instance.OnSingletonInit();
            return instance;
        }

        /// <summary>
        /// 根据路径特性，创建Mono单例对象
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dontDestroy"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static T CreateComponentOnGameObject<T>(string path, bool dontDestroy) where T : class
        {
            var obj = FindGameObject(path, true, dontDestroy);
            if (obj == null)
            {
                obj = new GameObject("Singleton of " + typeof(T).Name);
                if (dontDestroy)
                {
                    UnityEngine.Object.DontDestroyOnLoad(obj);
                }
            }

            return obj.AddComponent(typeof(T)) as T;
        }
        
        private static GameObject FindGameObject(string path, bool build, bool dontDestroy)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            var subPath = path.Split('/');
            if (subPath == null || subPath.Length == 0)
            {
                return null;
            }

            return FindGameObject(null, subPath, 0, build, dontDestroy);
        }
        
        private static GameObject FindGameObject(GameObject root, string[] subPath, int index, bool build,
            bool dontDestroy)
        {
            GameObject client = null;

            if (root == null)
            {
                client = GameObject.Find(subPath[index]);
            }
            else
            {
                var child = root.transform.Find(subPath[index]);
                if (child != null)
                {
                    client = child.gameObject;
                }
            }

            if (client == null)
            {
                if (build)
                {
                    client = new GameObject(subPath[index]);
                    if (root != null)
                    {
                        client.transform.SetParent(root.transform);
                    }

                    if (dontDestroy && index == 0)
                    {
                        GameObject.DontDestroyOnLoad(client);
                    }
                }
            }

            if (client == null)
            {
                return null;
            }

            return ++index == subPath.Length ? client : FindGameObject(client, subPath, index, build, dontDestroy);
        }
    }
}