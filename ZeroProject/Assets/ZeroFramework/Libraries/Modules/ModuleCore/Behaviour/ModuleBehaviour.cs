/****************************************************
  文件：Module.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月22日 19:06:28
  功能：
*****************************************************/

using System;
using UnityEngine;

namespace ZeroFramework
{
    /// <summary>
    /// 游戏框架模块Behaviour抽象类。
    /// </summary>
    public abstract class ModuleBehaviour : MonoBehaviour
    {
        /// <summary>
        /// 游戏框架模块Behaviour初始化。
        /// </summary>
        protected virtual void Awake()
        {
            ModuleBehaviourSystem.Instance.RegisterModule(this);
        }
    }
}