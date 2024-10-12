/****************************************************
  文件：UIModuleImpl.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月30日 16:02:32
  功能：
*****************************************************/

using System.Collections.Generic;

namespace ZeroEngine
{

    [UpdateModule]
    internal sealed partial class UIModuleImpl : ModuleImp
    {
        private List<UIWindow> _stack;

        internal void Initialize(List<UIWindow> stack)
        {
            _stack = stack;
        }

        internal override void Shutdown()
        {
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (_stack == null)
            {
                return;
            }

            int count = _stack.Count;
            for (int i = 0; i < _stack.Count; i++)
            {
                if (_stack.Count != count)
                {
                    break;
                }

                var window = _stack[i];
                window.InternalUpdate();
            }
        }
    }
}