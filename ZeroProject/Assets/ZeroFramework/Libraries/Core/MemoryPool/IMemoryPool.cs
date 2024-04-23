/****************************************************
  文件：IMemoryPool.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月18日 18:29:44
  功能：
*****************************************************/

using System;

namespace ZeroFramework
{
    public interface IMemoryPool
    {
        /// <summary>
        /// 获取或设置是否开启强制检查。
        /// </summary>
        bool EnableStrictCheck { get; set; }
        
        /// <summary>
        /// 获取内存池的数量。
        /// </summary>
        // ReSharper disable once InconsistentlySynchronizedField
        int Count { get; }

        /// <summary>
        /// 获取所有内存池的信息。
        /// </summary>
        /// <returns>所有内存池的信息。</returns>
        MemoryPoolInfo[] GetAllMemoryPoolInfos();

        /// <summary>
        /// 清除所有内存池。
        /// </summary>
        void ClearAll();

        /// <summary>
        /// 从内存池获取内存对象。（含类型检查）
        /// </summary>
        /// <typeparam name="T">内存对象类型。</typeparam>
        /// <returns>内存对象。</returns>
        T Acquire<T>() where T : class, IMemory, new();

        /// <summary>
        /// 从内存池获取内存对象。（不含类型检查）
        /// </summary>
        /// <param name="memoryType">内存对象类型。</param>
        /// <returns>内存对象。</returns>
        IMemory Acquire(Type memoryType);

        /// <summary>
        /// 将内存对象归还内存池。
        /// </summary>
        /// <param name="memory">内存对象。</param>
        void Release(IMemory memory);

        /// <summary>
        /// 向内存池中追加指定数量的内存对象。
        /// </summary>
        /// <typeparam name="T">内存对象类型。</typeparam>
        /// <param name="count">追加数量。</param>
        void Add<T>(int count) where T : class, IMemory, new();

        /// <summary>
        /// 向内存池中追加指定数量的内存对象。
        /// </summary>
        /// <param name="memoryType">内存对象类型。</param>
        /// <param name="count">追加数量。</param>
        void Add(Type memoryType, int count);

        /// <summary>
        /// 从内存池中移除指定数量的内存对象。
        /// </summary>
        /// <typeparam name="T">内存对象类型。</typeparam>
        /// <param name="count">移除数量。</param>
        void Remove<T>(int count) where T : class, IMemory;

        /// <summary>
        /// 从内存池中移除指定数量的内存对象。
        /// </summary>
        /// <param name="memoryType">内存对象类型。</param>
        /// <param name="count">移除数量。</param>
        void Remove(Type memoryType, int count);

        /// <summary>
        /// 从内存池中移除所有的内存对象。
        /// </summary>
        /// <typeparam name="T">内存对象类型。</typeparam>
        void RemoveAll<T>() where T : class, IMemory;

        /// <summary>
        /// 从内存池中移除所有的内存对象。
        /// </summary>
        /// <param name="memoryType">内存对象类型。</param>
        void RemoveAll(Type memoryType);
    }
}