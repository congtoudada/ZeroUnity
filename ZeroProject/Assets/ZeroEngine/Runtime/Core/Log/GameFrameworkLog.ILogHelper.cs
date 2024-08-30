/****************************************************
  文件：GameFrameworkLog_ILogHelper.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年08月22日 14:37:02
  功能：
*****************************************************/

namespace ZeroEngine
{
    public static partial class GameFrameworkLog
    {
        /// <summary>
        /// 游戏框架日志辅助器接口。
        /// </summary>
        public interface ILogHelper
        {
            /// <summary>
            /// 记录日志。
            /// </summary>
            /// <param name="level">游戏框架日志等级。</param>
            /// <param name="message">日志内容。</param>
            void Log(GameFrameworkLogLevel level, object message);
        }
    }
}
