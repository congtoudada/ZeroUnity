/****************************************************
  文件：AudioAgentRuntimeState.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年10月12日 15:27:52
  功能：
*****************************************************/

namespace ZeroEngine
{
    /// <summary>
    /// 音频代理辅助器运行时状态枚举。
    /// </summary>
    public enum AudioAgentRuntimeState
    {
        /// <summary>
        /// 无状态。
        /// </summary>
        None,

        /// <summary>
        /// 加载中状态。
        /// </summary>
        Loading,

        /// <summary>
        /// 播放中状态。
        /// </summary>
        Playing,

        /// <summary>
        /// 渐渐消失状态。
        /// </summary>
        FadingOut,

        /// <summary>
        /// 结束状态。
        /// </summary>
        End,
    };
}