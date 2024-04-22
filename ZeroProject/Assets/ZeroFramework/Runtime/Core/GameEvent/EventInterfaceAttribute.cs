/****************************************************
  文件：EventInterfaceAttribute.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月21日 21:01:27
  功能：
*****************************************************/

using System;

namespace ZeroFramework.Runtime
{
    /// <summary>
    /// 事件分组枚举。
    /// </summary>
    public enum EventGroup
    {
        /// <summary>
        /// UI相关的交互。
        /// </summary>
        GroupUI,   

        /// <summary>
        /// 逻辑层内部相关的交互。
        /// </summary>
        GroupLogic,
    }
    
    [System.AttributeUsage(System.AttributeTargets.Interface)]
    public class EventInterfaceAttribute : Attribute
    {
        private EventGroup _eGroup;
        public EventGroup EventGroup => _eGroup;
        public EventInterfaceAttribute(EventGroup group)
        {
            _eGroup = group;
        }
    }
}