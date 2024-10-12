/****************************************************
  文件：IAutoBindRuleHelper.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月29日 16:24:38
  功能：
*****************************************************/
using System.Collections.Generic;
using UnityEngine;

namespace ZeroEngine
{
    /// <summary>
    /// 自动绑定规则辅助器接口
    /// </summary>
    public interface IAutoBindRuleHelper
    {
        /// <summary>
        /// 是否为有效绑定
        /// </summary>
        bool IsValidBind(Transform target,List<string> filedNames,List<string> componentTypeNames);
    }
}