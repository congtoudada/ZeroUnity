/****************************************************
  文件：IMemory.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年08月22日 14:06:11
  功能：
*****************************************************/

namespace ZeroEngine
{
    /// <summary>
    /// 内存对象Interface。
    /// </summary>
    public interface IMemory
    {
        /// <summary>
        /// 清理内存对象回收入池。
        /// </summary>
        void Clear();
    }
}