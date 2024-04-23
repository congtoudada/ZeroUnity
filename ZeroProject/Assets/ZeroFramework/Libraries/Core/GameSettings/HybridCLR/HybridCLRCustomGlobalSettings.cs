/****************************************************
  文件：HybridCLRCustomGlobalSettings.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月22日 14:58:01
  功能：
*****************************************************/

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ZeroFramework
{
    [Serializable]
    public class HybridCLRCustomGlobalSettings
    {
        public bool Enable
        {
            get
            {
#if ENABLE_HYBRIDCLR
                return true;
#else
                return false;
#endif
            }
        }
        
        [LabelText("热更程序集（自动从HybridCLRGlobalSettings同步）")]
        public List<string> HotUpdateAssemblies = new List<string>() { "GameBase.dll","GameProto.dll","GameLogic.dll"};
        
        [LabelText("AOT程序集（手动配置）")]
        public List<string> AOTMetaAssemblies= new List<string>() {"mscorlib.dll","System.dll","System.Core.dll" };
        
        [LabelText("热更主程序集")]
        public string LogicMainDllName = "GameLogic.dll";
        
        [LabelText("程序集文本资产打包Asset后缀名")]
        public string AssemblyTextAssetExtension = ".bytes";
        
        [LabelText("程序集文本资产资源目录")]
        public string AssemblyTextAssetPath = "AssetRaw/DLL";
    }
}