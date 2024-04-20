/****************************************************
  文件：MemoryPool.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月18日 17:20:19
  功能：
*****************************************************/

using System;
using System.Collections.Generic;

namespace ZeroFramework.Runtime
{
    /// <summary>
    /// 内存池实现
    /// </summary>
    public partial class MemoryPool : Singleton<MemoryPool>, IMemoryPool
    {
        private MemoryPool()
        {
        }

        //根据不同类型收集内存碎片
        private readonly Dictionary<Type, MemoryCollection> _memoryCollections = new Dictionary<Type, MemoryCollection>();
        private bool _enableStrictCheck = false;
        
        #region 实现接口
        public bool EnableStrictCheck
        {
            get => _enableStrictCheck;
            set => _enableStrictCheck = value;
        }
        public int Count => _memoryCollections.Count;
        
        public MemoryPoolInfo[] GetAllMemoryPoolInfos()
        {
            int index = 0;
            MemoryPoolInfo[] results = null;

            lock (_memoryCollections)
            {
                results = new MemoryPoolInfo[_memoryCollections.Count];
                foreach (KeyValuePair<Type, MemoryCollection> memoryCollection in _memoryCollections)
                {
                    results[index++] = new MemoryPoolInfo(memoryCollection.Key, memoryCollection.Value.UnusedMemoryCount, memoryCollection.Value.UsingMemoryCount, memoryCollection.Value.AcquireMemoryCount, memoryCollection.Value.ReleaseMemoryCount, memoryCollection.Value.AddMemoryCount, memoryCollection.Value.RemoveMemoryCount);
                }
            }

            return results;
        }

        public void ClearAll()
        {
            lock (_memoryCollections)
            {
                foreach (KeyValuePair<Type, MemoryCollection> memoryCollection in _memoryCollections)
                {
                    memoryCollection.Value.RemoveAll();
                }

                _memoryCollections.Clear();
            }
        }

        public T Acquire<T>() where T : class, IMemory, new()
        {
            return GetMemoryCollection(typeof(T)).Acquire<T>();
        }

        public IMemory Acquire(Type memoryType)
        {
            InternalCheckMemoryType(memoryType);
            return GetMemoryCollection(memoryType).Acquire();
        }

        public void Release(IMemory memory)
        {
            if (memory == null)
            {
                throw new Exception("Memory is invalid.");
            }

            Type memoryType = memory.GetType();
            InternalCheckMemoryType(memoryType);
            GetMemoryCollection(memoryType).Release(memory);
        }

        public void Add<T>(int count) where T : class, IMemory, new()
        {
            GetMemoryCollection(typeof(T)).Add<T>(count);
        }

        public void Add(Type memoryType, int count)
        {
            InternalCheckMemoryType(memoryType);
            GetMemoryCollection(memoryType).Add(count);
        }

        public void Remove<T>(int count) where T : class, IMemory
        {
            GetMemoryCollection(typeof(T)).Remove(count);
        }

        public void Remove(Type memoryType, int count)
        {
            InternalCheckMemoryType(memoryType);
            GetMemoryCollection(memoryType).Remove(count);
        }

        public void RemoveAll<T>() where T : class, IMemory
        {
            GetMemoryCollection(typeof(T)).RemoveAll();
        }

        public void RemoveAll(Type memoryType)
        {
            InternalCheckMemoryType(memoryType);
            GetMemoryCollection(memoryType).RemoveAll();
        }
        #endregion
        
        #region 私有
        /// <summary>
        /// 安全性检查
        /// </summary>
        /// <param name="memoryType"></param>
        /// <exception cref="Exception"></exception>
        private void InternalCheckMemoryType(Type memoryType)
        {
            if (!_enableStrictCheck)
            {
                return;
            }

            if (memoryType == null)
            {
                throw new Exception("Memory type is invalid.");
            }

            if (!memoryType.IsClass || memoryType.IsAbstract)
            {
                throw new Exception("Memory type is not a non-abstract class type.");
            }

            if (!typeof(IMemory).IsAssignableFrom(memoryType))
            {
                throw new Exception(string.Format("Memory type '{0}' is invalid.", memoryType.FullName));
            }
        }
        
        /// <summary>
        /// 获取MemoryCollection
        /// </summary>
        /// <param name="memoryType"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private MemoryCollection GetMemoryCollection(Type memoryType)
        {
            if (memoryType == null)
            {
                throw new Exception("MemoryType is invalid.");
            }

            MemoryCollection memoryCollection = null;
            lock (_memoryCollections)
            {
                if (!_memoryCollections.TryGetValue(memoryType, out memoryCollection))
                {
                    memoryCollection = new MemoryCollection(memoryType);
                    _memoryCollections.Add(memoryType, memoryCollection);
                }
            }

            return memoryCollection;
        }
        #endregion
    }
}