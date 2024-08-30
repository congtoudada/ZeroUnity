/****************************************************
  文件：ZeroEngineSetting.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年08月30日 15:50:36
  功能：
*****************************************************/

using UnityEngine;

namespace ZeroEngine
{
  [CreateAssetMenu(fileName = "TEngineGlobalSettings", menuName = "TEngine/TEngineSettings")]

  public class ZeroEngineSettings : ScriptableObject
  {
    [Header("Framework")] [SerializeField] private FrameworkGlobalSettings m_FrameworkGlobalSettings;

    public FrameworkGlobalSettings FrameworkGlobalSettings => m_FrameworkGlobalSettings;

    [Header("HybridCLR")] [SerializeField] private HybridCLRCustomGlobalSettings m_HybridCLRCustomGlobalSettings;

    public HybridCLRCustomGlobalSettings BybridCLRCustomGlobalSettings => m_HybridCLRCustomGlobalSettings;

    public void Set(FrameworkGlobalSettings globalSettings, HybridCLRCustomGlobalSettings hybridClrCustomGlobalSettings)
    {
      m_FrameworkGlobalSettings = globalSettings;
      m_HybridCLRCustomGlobalSettings = hybridClrCustomGlobalSettings;
    }
  }
}