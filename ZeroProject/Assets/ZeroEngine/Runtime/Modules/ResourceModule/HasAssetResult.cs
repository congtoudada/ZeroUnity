/****************************************************
  文件：HasAssetResult.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月03日 22:15:24
  功能：
*****************************************************/

namespace ZeroEngine
{
    /// <summary>
    /// 检查资源是否存在的结果。
    /// </summary>
    public enum HasAssetResult : byte
    {
        /// <summary>
        /// 资源不存在。
        /// </summary>
        NotExist = 0,

        /// <summary>
        /// 资源需要从远端更新下载。
        /// </summary>
        AssetOnline,

        /// <summary>
        /// 存在资源且存储在磁盘上。
        /// </summary>
        AssetOnDisk,

        /// <summary>
        /// 存在资源且存储在文件系统里。
        /// </summary>
        AssetOnFileSystem,

        /// <summary>
        /// 存在二进制资源且存储在磁盘上。
        /// </summary>
        BinaryOnDisk,

        /// <summary>
        /// 存在二进制资源且存储在文件系统里。
        /// </summary>
        BinaryOnFileSystem,

        /// <summary>
        /// 资源定位地址无效。
        /// </summary>
        Valid,
    }
}