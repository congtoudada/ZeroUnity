/****************************************************
  文件：GameFrameworkLog.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月18日 17:21:49
  功能：
*****************************************************/

namespace ZeroFramework.Runtime
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

