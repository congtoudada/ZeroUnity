/****************************************************
  文件：ServerTypeEnum.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月21日 22:56:14
  功能：
*****************************************************/

namespace ZeroFramework.Runtime
{
    /// <summary>
    /// 服务器类型枚举。
    /// </summary>
    public enum ServerTypeEnum
    {
        /// <summary>
        /// 无。
        /// </summary>
        None = 0,

        /// <summary>
        /// 内网。
        /// </summary>
        Intranet = 1,

        /// <summary>
        /// 外网。
        /// </summary>
        Extranet = 2,

        /// <summary>
        /// 正式服。
        /// </summary>
        Formal = 3
    }
}