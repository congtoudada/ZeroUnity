using System;
using System.Collections.Generic;
using ZeroEngine;

namespace GameLogic
{
    /// <summary>
    /// 逻辑层事件接口。
    /// </summary>
    internal class RegisterEventInterface_Logic
    {
        /// <summary>
        /// 注册逻辑层事件接口。
        /// </summary>
        /// <param name="mgr">事件管理器。</param>
        public static void Register(EventMgr mgr)
        {
            HashSet<Type> types = CodeTypes.Instance.GetTypes(typeof(EventInterfaceImpAttribute));

            foreach (Type type in types)
            {
                object[] attrs = type.GetCustomAttributes(typeof(EventInterfaceImpAttribute), false);
                if (attrs.Length == 0)
                {
                    continue;
                }

                EventInterfaceImpAttribute httpHandlerAttribute = (EventInterfaceImpAttribute)attrs[0];

                if (httpHandlerAttribute.EventGroup != EEventGroup.GroupLogic)
                {
                    continue;
                }

                object obj = Activator.CreateInstance(type, mgr.Dispatcher);

                mgr.RegWrapInterface(obj.GetType().GetInterfaces()[0]?.FullName, obj);
            }
        }
    }
}