/****************************************************
  文件：MemoryPool.MemoryCollection.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月18日 17:56:49
  功能：
*****************************************************/

using System;
using System.Collections.Generic;

namespace ZeroFramework
{
    public partial class MemoryPool
    {
        /// <summary>
        /// 内存池收集器。
        /// </summary>
        private sealed class MemoryCollection
        {
            private readonly Queue<IMemory> _memories;
            private readonly Type _memoryType;
            private int _usingMemoryCount;
            private int _acquireMemoryCount;
            private int _releaseMemoryCount;
            private int _addMemoryCount;
            private int _removeMemoryCount;
            
            public MemoryCollection(Type memoryType)
            {
                _memories = new Queue<IMemory>();
                _memoryType = memoryType;
                _usingMemoryCount = 0;
                _acquireMemoryCount = 0;
                _releaseMemoryCount = 0;
                _addMemoryCount = 0;
                _removeMemoryCount = 0;
            }
            
            public Type MemoryType => _memoryType;

            public int UnusedMemoryCount => _memories.Count;

            public int UsingMemoryCount => _usingMemoryCount;

            public int AcquireMemoryCount => _acquireMemoryCount;

            public int ReleaseMemoryCount => _releaseMemoryCount;

            public int AddMemoryCount => _addMemoryCount;

            public int RemoveMemoryCount => _removeMemoryCount;
            
            /// <summary>
            /// 申请内存对象（含类型检查）
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            public T Acquire<T>() where T : class, IMemory, new()
            {
                if (typeof(T) != _memoryType)
                {
                    throw new Exception("Type is invalid.");
                }
                
                _usingMemoryCount++;
                _acquireMemoryCount++;
                //如果可以，优先从内存池获取
                T mem = null;
                lock (_memories)
                {
                    if (_memories.Count > 0)
                    {
                        mem = (T)_memories.Dequeue();
                    }
                }
                //如果没有缓存，就new一个
                if (mem == null)
                {
                    mem = new T();
                    _addMemoryCount++;
                }
                mem.OnGet();
                return mem;
            }
            
            /// <summary>
            /// 申请内存对象（不含类型检查）
            /// </summary>
            /// <returns></returns>
            public IMemory Acquire()
            {
                _usingMemoryCount++;
                _acquireMemoryCount++;
                IMemory mem = null;
                lock (_memories)
                {
                    if (_memories.Count > 0)
                    {
                        mem = _memories.Dequeue();
                    }
                }

                if (mem == null)
                {
                    mem = (IMemory)Activator.CreateInstance(_memoryType);
                    _addMemoryCount++;
                }
                mem.OnGet();
                return mem;
            }
            
            /// <summary>
            /// 回收内存对象
            /// </summary>
            /// <param name="memory"></param>
            /// <exception cref="Exception"></exception>
            public void Release(IMemory memory)
            {
                memory.OnRelease();
                lock (_memories)
                {
                    if (Instance._enableStrictCheck && _memories.Contains(memory))
                    {
                        throw new Exception("The memory has been released.");
                    }

                    _memories.Enqueue(memory);
                }

                _releaseMemoryCount++;
                _usingMemoryCount--;
            }
            
            /// <summary>
            /// 主动添加内存对象
            /// </summary>
            /// <param name="count"></param>
            /// <typeparam name="T"></typeparam>
            /// <exception cref="Exception"></exception>
            public void Add<T>(int count) where T : class, IMemory, new()
            {
                if (typeof(T) != _memoryType)
                {
                    throw new Exception("Type is invalid.");
                }

                lock (_memories)
                {
                    _addMemoryCount += count;
                    while (count-- > 0)
                    {
                        _memories.Enqueue(new T());
                    }
                }
            }
            
            /// <summary>
            /// 主动添加指定数量的内存对象
            /// </summary>
            /// <param name="count"></param>
            public void Add(int count)
            {
                lock (_memories)
                {
                    _addMemoryCount += count;
                    while (count-- > 0)
                    {
                        _memories.Enqueue((IMemory)Activator.CreateInstance(_memoryType));
                    }
                }
            }
            
            /// <summary>
            /// 主动移除内存对象
            /// </summary>
            /// <param name="count"></param>
            public void Remove(int count)
            {
                lock (_memories)
                {
                    if (count > _memories.Count)
                    {
                        count = _memories.Count;
                    }

                    _removeMemoryCount += count;
                    while (count-- > 0)
                    {
                        _memories.Dequeue();
                    }
                }
            }
            
            /// <summary>
            /// 移除所有内存对象
            /// </summary>
            public void RemoveAll()
            {
                lock (_memories)
                {
                    _removeMemoryCount += _memories.Count;
                    _memories.Clear();
                }
            }
        }
    }
}