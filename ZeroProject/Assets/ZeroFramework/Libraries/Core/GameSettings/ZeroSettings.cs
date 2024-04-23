/****************************************************
  文件：ZeroSettings.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月22日 15:08:33
  功能：
*****************************************************/

using UnityEngine;

namespace ZeroFramework
{
    [CreateAssetMenu(fileName = "ZeroGlobalSettings", menuName = "Zero/ZeroGlobalSettings")]
    public class ZeroSettings : ScriptableObject
    {
        [Header("Framework")] [SerializeField] 
        private FrameworkGlobalSettings m_FrameworkGlobalSettings;
        public FrameworkGlobalSettings FrameworkGlobalSettings => m_FrameworkGlobalSettings;
        
        [Header("HybridCLR")] [SerializeField] 
        private HybridCLRCustomGlobalSettings m_BybridCLRCustomGlobalSettings;
        public HybridCLRCustomGlobalSettings BybridCLRCustomGlobalSettings => m_BybridCLRCustomGlobalSettings;
        
        public void Set(FrameworkGlobalSettings globalSettings,HybridCLRCustomGlobalSettings hybridClrCustomGlobalSettings)
        {
            m_FrameworkGlobalSettings = globalSettings;
            m_BybridCLRCustomGlobalSettings = hybridClrCustomGlobalSettings;
        }
    }
}