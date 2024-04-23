/****************************************************
  文件：GameFrameworkLogLevel.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月18日 17:25:15
  功能：
*****************************************************/

namespace ZeroFramework
{
    /// <summary>
    /// 游戏框架日志等级。
    /// </summary>
    public enum GameFrameworkLogLevel : byte
    {
        /// <summary>
        /// 调试。
        /// </summary>
        Debug = 0,

        /// <summary>
        /// 信息。
        /// </summary>
        Info,

        /// <summary>
        /// 警告。
        /// </summary>
        Warning,

        /// <summary>
        /// 错误。
        /// </summary>
        Error,

        /// <summary>
        /// 严重错误。
        /// </summary>
        Fatal
    }
}