/****************************************************
  文件：MemoryObject.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月18日 17:49:43
  功能：
*****************************************************/

namespace ZeroFramework.Runtime
{
    /// <summary>
    /// 内存池对象基类。
    /// </summary>
    public abstract class MemoryObject : IMemory
    {
        /// <summary>
        /// 从内存池获取对象时调用
        /// </summary>
        public virtual void OnGet()
        {
        }

        /// <summary>
        /// 清理内存对象回收入池。
        /// </summary>
        public virtual void OnRelease()
        {
        }
    }
}