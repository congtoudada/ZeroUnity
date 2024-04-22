/****************************************************
  文件：FrameworkGlobalSettings.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月22日 14:08:30
  功能：
*****************************************************/

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
// using Sirenix.OdinInspector;
using UnityEditor;
#endif

namespace ZeroFramework.Runtime
{
    [Serializable]
    public class FrameworkGlobalSettings
    {
        [SerializeField] [LabelText("作者")]
        private string m_ScriptAuthor = "Default";
        public string ScriptAuthor => m_ScriptAuthor;
        
        [SerializeField] [LabelText("版本")]
        private string m_ScriptVersion = "0.1";
        public string ScriptVersion => m_ScriptVersion;
        
        [SerializeField] [LabelText("App阶段标识枚举")]
        private AppStageEnum m_AppStageEnum = AppStageEnum.Debug;
        public AppStageEnum AppStageEnum => m_AppStageEnum;
        
        [Header("Font")] [LabelText("字体")] [SerializeField] private string m_DefaultFont = "Arial";
        public string DefaultFont => m_DefaultFont;
        
        [Header("Resources")] [LabelText("资源存放地")] [SerializeField]
        private ResourcesArea m_ResourcesArea;
        public ResourcesArea ResourcesArea => m_ResourcesArea;
        
        [Header("SpriteCollection")]  [LabelText("图集文件夹")] [SerializeField]
        private string m_AtlasFolder = "Assets/AssetRaw/Atlas";
        public string AtlasFolder => m_AtlasFolder;
        
        [Header("Hotfix")]
        public string HostServerURL = "http://127.0.0.1:8081";
        public string FallbackHostServerURL = "http://127.0.0.1:8081";
        public bool EnableUpdateData = false;
        public string WindowsUpdateDataUrl = "http://127.0.0.1";
        public string MacOSUpdateDataUrl = "http://127.0.0.1";
        public string IOSUpdateDataUrl = "http://127.0.0.1";
        public string AndroidUpdateDataUrl = "http://127.0.0.1";
        public string WebGLUpdateDataUrl = "http://127.0.0.1";
        
        [Header("Server")][SerializeField] 
        private string m_CurUseServerChannel;
        public string CurUseServerChannel => m_CurUseServerChannel;
        
        [SerializeField]
        private List<ServerChannelInfo> m_ServerChannelInfos;
        public List<ServerChannelInfo> ServerChannelInfos => m_ServerChannelInfos;
        
        [SerializeField] private string @namespace = "GameLogic";
        public string NameSpace => @namespace;
        
        [SerializeField] private List<ScriptGenerateRuler> scriptGenerateRule = new List<ScriptGenerateRuler>()
        {
            new ScriptGenerateRuler("m_go", "GameObject"),
            new ScriptGenerateRuler("m_item", "GameObject"),
            new ScriptGenerateRuler("m_tf", "Transform"),
            new ScriptGenerateRuler("m_rect", "RectTransform"),
            new ScriptGenerateRuler("m_text", "Text"),
            new ScriptGenerateRuler("m_richText", "RichTextItem"),
            new ScriptGenerateRuler("m_btn", "Button"),
            new ScriptGenerateRuler("m_img", "Image"),
            new ScriptGenerateRuler("m_rimg", "RawImage"),
            new ScriptGenerateRuler("m_scrollBar", "Scrollbar"),
            new ScriptGenerateRuler("m_scroll", "ScrollRect"),
            new ScriptGenerateRuler("m_input", "InputField"),
            new ScriptGenerateRuler("m_grid", "GridLayoutGroup"),
            new ScriptGenerateRuler("m_hlay", "HorizontalLayoutGroup"),
            new ScriptGenerateRuler("m_vlay", "VerticalLayoutGroup"),
            new ScriptGenerateRuler("m_red", "RedNoteBehaviour"),
            new ScriptGenerateRuler("m_slider", "Slider"),
            new ScriptGenerateRuler("m_group", "ToggleGroup"),
            new ScriptGenerateRuler("m_curve", "AnimationCurve"),
            new ScriptGenerateRuler("m_canvasGroup", "CanvasGroup"),
#if ENABLE_TEXTMESHPRO
        new ScriptGenerateRuler("m_tmp","TextMeshProUGUI"),
#endif
        };
        public List<ScriptGenerateRuler> ScriptGenerateRule => scriptGenerateRule;
    }
    
    [Serializable]
    public class ScriptGenerateRuler
    {
        public string uiElementRegex;
        public string componentName;

        public ScriptGenerateRuler(string uiElementRegex, string componentName)
        {
            this.uiElementRegex = uiElementRegex;
            this.componentName = componentName;
        }
    }
    
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ScriptGenerateRuler))]
    public class ScriptGenerateRulerDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var uiElementRegexRect = new Rect(position.x, position.y, 120, position.height);
            var componentNameRect = new Rect(position.x + 125, position.y, 150, position.height);
            EditorGUI.PropertyField(uiElementRegexRect, property.FindPropertyRelative("uiElementRegex"), GUIContent.none);
            EditorGUI.PropertyField(componentNameRect, property.FindPropertyRelative("componentName"), GUIContent.none);
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
#endif
}