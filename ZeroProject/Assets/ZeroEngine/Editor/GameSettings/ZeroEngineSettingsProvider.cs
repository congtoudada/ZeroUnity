/****************************************************
  文件：ZeroEngineSettingsProvider.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年10月17日 16:02:59
  功能：
*****************************************************/
using System.IO;
using UnityEditor;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine;
using ZeroEngine;

public class ZeroEngineSettingsProvider : SettingsProvider
{
    const string k_SettingsPathHeader = "Assets/ZeroEngine/ResRaw/Resources/";
    const string k_SettingsPath = "Assets/ZeroEngine/ResRaw/Resources/ZeroEngineGlobalSettings.asset";
    private const string headerName = "ZeroEngine/ZeroEngineSettings";
    private SerializedObject m_CustomSettings;

    private static string m_SettingsPath = k_SettingsPath;
    internal static SerializedObject GetSerializedSettings()
    {
        return new SerializedObject(SettingsUtils.GlobalSettings);
    }

    public static bool IsSettingsAvailable()
    {
        var pathes = AssetDatabase.FindAssets("ZeroEngineGlobalSettings", new string[2] { k_SettingsPathHeader,"Packages/com.ZeroEngine/" });
        if (pathes.Length > 0)
        {
            m_SettingsPath = AssetDatabase.GUIDToAssetPath(pathes[0]);
        }
        return pathes.Length > 0;
    }

    public override void OnActivate(string searchContext, VisualElement rootElement)
    {
        base.OnActivate(searchContext, rootElement);
        m_CustomSettings = GetSerializedSettings();
    }

    public override void OnDeactivate()
    {
        base.OnDeactivate();

        // 确保只有在有修改时才保存
        if (m_CustomSettings != null && m_CustomSettings.hasModifiedProperties)
        {
            EditorApplication.delayCall += () => SaveAssetData(k_SettingsPath);
        }
    }

    void SaveAssetData(string path)
    {
        ZeroEngineSettings old = AssetDatabase.LoadAssetAtPath<ZeroEngineSettings>(k_SettingsPath);
        if (old == null)
        {
            Debug.LogError($"Failed to load ZeroEngineSettings from path: {k_SettingsPath}");
            return;
        }

        ZeroEngineSettings data = ScriptableObject.CreateInstance<ZeroEngineSettings>();
        data.Set(old.FrameworkGlobalSettings, old.BybridCLRCustomGlobalSettings);

        if (AssetDatabase.DeleteAsset(path))
        {
            AssetDatabase.CreateAsset(data, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        else
        {
            Debug.LogError($"Failed to delete existing asset at path: {path}");
        }
    }


    public override void OnGUI(string searchContext)
    {
        base.OnGUI(searchContext);
        using var changeCheckScope = new EditorGUI.ChangeCheckScope();
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("m_FrameworkGlobalSettings"));

        if (GUILayout.Button("Refresh HotUpdateAssemblies"))
        {
            SyncAssemblyContent.RefreshAssembly();
            m_CustomSettings.ApplyModifiedPropertiesWithoutUndo();
            m_CustomSettings = null;
            m_CustomSettings = GetSerializedSettings();
        }

        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("m_HybridCLRCustomGlobalSettings"));
        EditorGUILayout.Space(20);
        if (!changeCheckScope.changed)
        {
            return;
        }
        m_CustomSettings.ApplyModifiedPropertiesWithoutUndo();
    }

    public ZeroEngineSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path, scopes, keywords)
    {
    }

    [SettingsProvider]
    private static SettingsProvider CreateSettingProvider()
    {
        if (IsSettingsAvailable())
        {
            var provider = new ZeroEngineSettingsProvider(headerName, SettingsScope.Project);
            provider.keywords = GetSearchKeywordsFromGUIContentProperties<ZeroEngineSettings>();
            return provider;
        }
        else
        {
            Debug.LogError($"Open ZeroEngine Settings error,Please Create ZeroEngine ZeroEngineGlobalSettings.assets File in Path ZeroEngine/ResRaw/Resources/");
        }

        return null;
    }
}