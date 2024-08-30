/****************************************************
  文件：ModuleImp.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年08月22日 15:42:23
  功能：
*****************************************************/

using System;

namespace ZeroEngine
{
    /// <summary>
    /// 模块需要框架轮询属性。
    /// </summary>
    /// <remarks> 注入此属性标识模块需要轮询。</remarks>
    [AttributeUsage(AttributeTargets.Class)]
    internal class UpdateModuleAttribute : Attribute
    {
    }

    /// <summary>
    /// 游戏框架模块抽象类。
    /// <remarks>实现游戏框架具体逻辑。</remarks>
    /// </summary>
    internal abstract class ModuleImp
    {
        /// <summary>
        /// 获取游戏框架模块优先级。
        /// </summary>
        /// <remarks>优先级较高的模块会优先轮询，并且关闭操作会后进行。</remarks>
        internal virtual int Priority => 0;

        /// <summary>
        /// 游戏框架模块轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        internal virtual void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// 关闭并清理游戏框架模块。
        /// </summary>
        internal abstract void Shutdown();
    }
}