/****************************************************
  文件：LoadResourceStatus.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月03日 20:27:56
  功能：
*****************************************************/

namespace ZeroEngine
{
    /// <summary>
    /// 加载资源状态。
    /// </summary>
    public enum LoadResourceStatus : byte
    {
        /// <summary>
        /// 加载资源完成。
        /// </summary>
        Success = 0,

        /// <summary>
        /// 资源不存在。
        /// </summary>
        NotExist,

        /// <summary>
        /// 资源尚未准备完毕。
        /// </summary>
        NotReady,

        /// <summary>
        /// 依赖资源错误。
        /// </summary>
        DependencyError,

        /// <summary>
        /// 资源类型错误。
        /// </summary>
        TypeError,

        /// <summary>
        /// 加载资源错误。
        /// </summary>
        AssetError
    }
}
