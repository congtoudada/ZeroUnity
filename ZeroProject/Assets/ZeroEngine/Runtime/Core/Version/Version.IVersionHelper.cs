/****************************************************
  文件：Version_IVersionHelper.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年08月31日 15:55:59
  功能：
*****************************************************/

namespace ZeroEngine
{
    public static partial class Version
    {
        /// <summary>
        /// 版本号辅助器接口。
        /// </summary>
        public interface IVersionHelper
        {
            /// <summary>
            /// 获取游戏版本号。
            /// </summary>
            string GameVersion
            {
                get;
            }

            /// <summary>
            /// 获取内部游戏版本号。
            /// </summary>
            string InternalGameVersion
            {
                get;
            }
        }
    }
}
