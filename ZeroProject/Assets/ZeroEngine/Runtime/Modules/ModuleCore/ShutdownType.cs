/****************************************************
  文件：ShutdownType.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年08月22日 15:19:46
  功能：
*****************************************************/

namespace ZeroEngine
{
    /// <summary>
    /// 关闭游戏框架类型。
    /// </summary>
    public enum ShutdownType : byte
    {
        /// <summary>
        /// 仅关闭游戏框架。
        /// </summary>
        None = 0,

        /// <summary>
        /// 关闭游戏框架并重启游戏。
        /// </summary>
        Restart,

        /// <summary>
        /// 关闭游戏框架并退出游戏。
        /// </summary>
        Quit,
    }
}