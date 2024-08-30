/****************************************************
  文件：MemoryPoolExtension.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年08月22日 14:18:41
  功能：
*****************************************************/

using System;

namespace ZeroEngine
{
    /// <summary>
    /// 内存池对象基类。
    /// </summary>
    public abstract class MemoryObject : IMemory
    {
        /// <summary>
        /// 清理内存对象回收入池。
        /// </summary>
        public virtual void Clear()
        {
        }

        /// <summary>
        /// 从内存池中初始化。
        /// </summary>
        public abstract void InitFromPool();

        /// <summary>
        /// 回收到内存池。
        /// </summary>
        public abstract void RecycleToPool();
    }

    public static partial class MemoryPool
    {
        /// <summary>
        /// 从内存池获取内存对象。
        /// </summary>
        /// <typeparam name="T">内存对象类型。</typeparam>
        /// <returns>内存对象。</returns>
        public static T Alloc<T>() where T : MemoryObject, new()
        {
            T memory = Acquire<T>();
            memory.InitFromPool();
            return memory;
        }

        /// <summary>
        /// 将内存对象归还内存池。
        /// </summary>
        /// <param name="memory">内存对象。</param>
        public static void Dealloc(MemoryObject memory)
        {
            if (memory == null)
            {
                throw new Exception("Memory is invalid.");
            }

            memory.RecycleToPool();
            Release(memory);
        }
    }
}