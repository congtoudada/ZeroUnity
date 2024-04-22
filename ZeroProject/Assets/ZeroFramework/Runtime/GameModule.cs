/****************************************************
  文件：GameModule.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月22日 18:46:25
  功能：
*****************************************************/

using UnityEngine;

namespace ZeroFramework.Runtime
{
    /// <summary>
    /// 游戏模块。
    /// </summary>
    public partial class GameModule : MonoBehaviour
    {
        #region 框架模块

        /// <summary>
        /// 获取游戏基础模块。
        /// </summary>
        public static BaseModule Base;
        // {
        //     // get => _base ??= Get<BaseModule>();
        //     // private set => _base = value;
        // }

        private static BaseModule _base;
        #endregion
    }
}