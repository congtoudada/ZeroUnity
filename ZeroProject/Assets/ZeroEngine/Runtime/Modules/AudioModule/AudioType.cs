/****************************************************
  文件：AudioType.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年10月12日 16:41:55
  功能：
*****************************************************/

namespace ZeroEngine
{
    /// <summary>
    /// 音效分类，可分别关闭/开启对应分类音效。
    /// </summary>
    /// <remarks>命名与AudioMixer中分类名保持一致。</remarks>
    public enum AudioType
    {
        /// <summary>
        /// 声音音效。
        /// </summary>
        Sound,

        /// <summary>
        /// UI声效。
        /// </summary>
        UISound,

        /// <summary>
        /// 背景音乐音效。
        /// </summary>
        Music,

        /// <summary>
        /// 人声音效。
        /// </summary>
        Voice,

        /// <summary>
        /// 最大。
        /// </summary>
        Max
    }
}