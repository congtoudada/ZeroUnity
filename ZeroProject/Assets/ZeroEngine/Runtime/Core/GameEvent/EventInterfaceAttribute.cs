/****************************************************
  文件：EventInterfaceAttribute.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年08月23日 20:58:44
  功能：
*****************************************************/

using System;

namespace ZeroEngine
{
    /// <summary>
    /// 事件分组枚举。
    /// </summary>
    public enum EEventGroup
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
        private EEventGroup _eGroup;
        public EEventGroup EventGroup => _eGroup;
        public EventInterfaceAttribute(EEventGroup group)
        {
            _eGroup = group;
        }
    }
}
