/****************************************************
  文件：ComponentAutoBindTool.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月29日 16:03:49
  功能：
*****************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroEngine
{
    /// <summary>
    /// 组件自动绑定工具
    /// </summary>
    public class ComponentAutoBindTool : MonoBehaviour
    {
#if UNITY_EDITOR
        [Serializable]
        public class BindData
        {
            public BindData()
            {
            }

            public BindData(string name, Component bindCom, bool isGameObject = false)
            {
                Name = name;
                BindCom = bindCom;
                IsGameObject = isGameObject;
            }

            public string Name;
            public Component BindCom;
            public bool IsGameObject;
        }

        public List<BindData> BindDatas = new List<BindData>();

        [SerializeField]
        private string m_ClassName;

        [SerializeField]
        private string m_Namespace;

        [SerializeField]
        private string m_CodePath;

        [SerializeField]
        private bool m_IsWidget;
    
        public string ClassName => m_ClassName;

        public string Namespace => m_Namespace;

        public string CodePath => m_CodePath;
    

        public bool IsWidget => m_IsWidget;

        public IAutoBindRuleHelper RuleHelper
        {
            get;
            set;
        }
#endif

        [SerializeField]
        public List<Component> bindComponents = new List<Component>();

        public T GetBindComponent<T>(int index) where T : Component
        {
            if (index >= bindComponents.Count)
            {
                Debug.LogError("索引无效");
                return null;
            }

            T bindCom = bindComponents[index] as T;

            if (bindCom == null)
            {
                Debug.LogError("类型无效");
                return null;
            }

            return bindCom;
        }
    }
}