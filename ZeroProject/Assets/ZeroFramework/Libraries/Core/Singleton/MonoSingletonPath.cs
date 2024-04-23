/****************************************************
  文件：MonoSingletonPath.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2023-11-30 16:27:59
  功能：
*****************************************************/

using System;

namespace ZeroFramework
{
    /// <summary>
    /// Mono单例特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)] //这个特性只能标记在Class上
    public class MonoSingletonPathAttribute : Attribute
    {
        public string PathInHierarchy { get; private set; }
        
        public MonoSingletonPathAttribute(string pathInHierarchy)
        {
            PathInHierarchy = pathInHierarchy;
        }
    }
}