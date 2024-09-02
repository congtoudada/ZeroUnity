/****************************************************
  文件：DefaultVersionHelper.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年08月31日 15:55:17
  功能：
*****************************************************/

using UnityEngine;

namespace ZeroEngine
{
    /// <summary>
    /// 默认版本号辅助器。
    /// </summary>
    public class DefaultVersionHelper : Version.IVersionHelper
    {
        /// <summary>
        /// 获取游戏版本号。
        /// </summary>
        public string GameVersion => Application.version;

        /// <summary>
        /// 获取内部游戏版本号。
        /// </summary>
        public string InternalGameVersion => string.Empty;
    }
}
