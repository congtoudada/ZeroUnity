/****************************************************
  文件：GameFrameworkLogLevel.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年08月22日 14:37:24
  功能：
*****************************************************/

namespace ZeroEngine
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
