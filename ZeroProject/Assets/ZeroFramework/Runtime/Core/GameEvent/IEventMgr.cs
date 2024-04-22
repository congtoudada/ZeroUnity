/****************************************************
  文件：IEventMgr.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月21日 20:26:02
  功能：
*****************************************************/

namespace ZeroFramework.Runtime
{
    public interface IEventMgr
    {
        /// <summary>
        /// 分发注册器。
        /// </summary>
        EventDispatcher Dispatcher { get; }
        
        /// <summary>
        /// 事件管理器获取接口。
        /// </summary>
        /// <typeparam name="T">接口类型。</typeparam>
        /// <returns>接口实例。</returns>
        T GetInterface<T>();

        /// <summary>
        /// 注册wrap的函数。
        /// </summary>
        /// <typeparam name="T">Wrap接口类型。</typeparam>
        /// <param name="callerWrap">callerWrap接口名字。</param>
        void RegWrapInterface<T>(T callerWrap);

        /// <summary>
        /// 注册wrap的函数。
        /// </summary>
        /// <param name="typeName">类型名称。</param>
        /// <param name="callerWrap">调用接口名。</param>
        public void RegWrapInterface(string typeName, object callerWrap);
    }
}