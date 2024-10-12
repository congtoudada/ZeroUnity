/****************************************************
  文件：UIAutoBindRuleHelper.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月29日 16:28:20
  功能：
*****************************************************/
using System.Collections.Generic;
using UnityEngine;

namespace ZeroEngine
{
    /// <summary>
    /// UI自动绑定规则辅助器
    /// </summary>
    public class UIAutoBindRuleHelper: IAutoBindRuleHelper
    {
        public bool IsValidBind( Transform targetTransform, List<string> filedNames, List<string> componentTypeNames)
        {
            string uiElementName = targetTransform.name;
            string[] strArray = targetTransform.name.Split('_');

            if (strArray.Length == 1)
            {
                return false;
            }

            string filedName = strArray[^1];
            //根据前缀获取匹配的Ruler
            var rule = SettingsUtils.GetScriptGenerateRule().Find(t => uiElementName.StartsWith(t.uiElementRegex));

            if (rule != null)
            {
                filedNames.Add($"{filedName}");  // 获取控件名称
                componentTypeNames.Add(rule.componentName); //获取实际Unity类型
                return true;
            }
            Debug.LogWarning($"{targetTransform.name}的命名中{uiElementName}不存在对应的组件类型，绑定失败");
            return false;
        }
    }
}