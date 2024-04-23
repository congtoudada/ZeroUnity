/****************************************************
  文件：IModuleBehaviourSystem.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月22日 21:15:33
  功能：
*****************************************************/

using System;

namespace ZeroFramework
{
    /// <summary>
    /// 游戏框架模块Behaviour管理系统接口
    /// </summary>
    interface IModuleBehaviourSystem
    {
        /// <summary>
        /// 获取游戏框架模块。
        /// </summary>
        /// <typeparam name="T">要获取的游戏框架模块类型。</typeparam>
        /// <returns>要获取的游戏框架模块。</returns>
        T GetModule<T>() where T : ModuleBehaviour;

        /// <summary>
        /// 获取游戏框架模块。
        /// </summary>
        /// <param name="type">要获取的游戏框架模块类型。</param>
        /// <returns>要获取的游戏框架模块。</returns>
        ModuleBehaviour GetModule(Type type);

        /// <summary>
        /// 获取游戏框架模块。
        /// </summary>
        /// <param name="typeName">要获取的游戏框架模块类型名称。</param>
        /// <returns>要获取的游戏框架模块。</returns>
        ModuleBehaviour GetModule(string typeName);

        /// <summary>
        /// 关闭游戏框架。
        /// </summary>
        /// <param name="shutdownType">关闭游戏框架类型。</param>
        void Shutdown(ShutdownType shutdownType);

        /// <summary>
        /// 注册游戏框架模块。
        /// </summary>
        /// <param name="moduleBehaviour">要注册的游戏框架模块。</param>
        void RegisterModule(ModuleBehaviour moduleBehaviour);
    }
}