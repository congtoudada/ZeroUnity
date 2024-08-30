/****************************************************
  文件：Module.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年08月22日 15:19:02
  功能：
*****************************************************/

using UnityEngine;

namespace ZeroEngine
{
    /// <summary>
    /// 游戏框架模块抽象类。
    /// </summary>
    public abstract class Module : MonoBehaviour
    {
        /// <summary>
        /// 游戏框架模块初始化。
        /// </summary>
        protected virtual void Awake()
        {
            ModuleSystem.RegisterModule(this);
        }
    }
}