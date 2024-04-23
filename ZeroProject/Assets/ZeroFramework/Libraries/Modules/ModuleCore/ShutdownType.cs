/****************************************************
  文件：ShutdownType.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月22日 19:14:43
  功能：
*****************************************************/

namespace ZeroFramework
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