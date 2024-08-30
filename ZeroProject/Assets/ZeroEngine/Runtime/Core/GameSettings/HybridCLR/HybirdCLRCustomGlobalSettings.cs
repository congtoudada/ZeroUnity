/****************************************************
  文件：HybirdCLRCustomGlobalSettings.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年08月30日 15:38:26
  功能：
*****************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ZeroEngine
{
  /// <summary>
  /// HybridCLRCustomGlobalSettings.
  /// </summary>
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

    [Header("Auto sync with [HybridCLRGlobalSettings]")]
    [Tooltip("You should modify the file from file path [Assets/CustomHybridCLR/Settings/HybridCLRGlobalSettings.asset]")]
    public List<string> HotUpdateAssemblies = new List<string>() { "GameBase.dll","GameProto.dll","GameLogic.dll"};

    [Header("Need manual setting!")] public List<string> AOTMetaAssemblies= new List<string>() {"mscorlib.dll","System.dll","System.Core.dll" };

    /// <summary>
    /// Dll of main business logic assembly
    /// </summary>
    public string LogicMainDllName = "GameLogic.dll";

    /// <summary>
    /// 程序集文本资产打包Asset后缀名
    /// </summary>
    public string AssemblyTextAssetExtension = ".bytes";

    /// <summary>
    /// 程序集文本资产资源目录
    /// </summary>
    public string AssemblyTextAssetPath = "AssetRaw/DLL";

    /// <summary>
    /// Resources HybridCLRGlobalSettings Dir
    /// </summary>
    public string HybridCLRGlobalSettings = "Settings/HybridCLRGlobalSettings";
  }
}