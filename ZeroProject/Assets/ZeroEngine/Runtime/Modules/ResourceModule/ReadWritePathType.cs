/****************************************************
  文件：ReadWritePathType.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月03日 22:38:51
  功能：
*****************************************************/

namespace ZeroEngine
{
    /// <summary>
    /// 读写区路径类型。
    /// </summary>
    public enum ReadWritePathType : byte
    {
        /// <summary>
        /// 未指定。
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// 临时缓存。
        /// </summary>
        TemporaryCache,

        /// <summary>
        /// 持久化数据。
        /// </summary>
        PersistentData,
    }
}