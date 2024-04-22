/****************************************************
  文件：SettingsUtils.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月22日 15:08:04
  功能：辅助快速获取框架配置
*****************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ZeroFramework.Runtime
{
    public class SettingsUtils : Singleton<SettingsUtils>, ISettingsUtils
    {
        private readonly string GlobalSettingsPath = $"ZeroGlobalSettings"; //放在Resources内
        private ZeroSettings _globalSettings;
        
        /// <summary>
        /// Zero全局配置
        /// </summary>
        public ZeroSettings GlobalSettings
        {
            get
            {
                if (_globalSettings == null)
                {
                    _globalSettings = GetSingletonAssetsByResources<ZeroSettings>(GlobalSettingsPath);
                }

                return _globalSettings;
            }
        }
        
        /// <summary>
        /// 获取FrameworkGlobalSettings
        /// </summary>
        public FrameworkGlobalSettings FrameworkGlobalSettings => GlobalSettings.FrameworkGlobalSettings;
        
        /// <summary>
        /// 获取HybridCLRCustomGlobalSettings
        /// </summary>
        public HybridCLRCustomGlobalSettings HybridCLRCustomGlobalSettings => GlobalSettings.BybridCLRCustomGlobalSettings;
        
        /// <summary>
        /// 获取ResourcesArea
        /// </summary>
        public ResourcesArea ResourcesArea => GlobalSettings.FrameworkGlobalSettings.ResourcesArea;
        
        public void SetHybridCLRHotUpdateAssemblies(List<string> hotUpdateAssemblies)
        {
            HybridCLRCustomGlobalSettings.HotUpdateAssemblies.Clear();
            HybridCLRCustomGlobalSettings.HotUpdateAssemblies.AddRange(hotUpdateAssemblies);
        }
        
        public void SetHybridCLRAOTMetaAssemblies(List<string> aOTMetaAssemblies)
        {
            HybridCLRCustomGlobalSettings.AOTMetaAssemblies.Clear();
            HybridCLRCustomGlobalSettings.AOTMetaAssemblies.AddRange(aOTMetaAssemblies);
        }
        
        /// <summary>
        /// 是否启用UpdateData
        /// </summary>
        /// <returns></returns>
        public bool EnableUpdateData()
        {
            return FrameworkGlobalSettings.EnableUpdateData;
        }
        
        /// <summary>
        /// 根据当前平台获取相应UpdateDataURL
        /// </summary>
        /// <returns></returns>
        public string GetUpdateDataUrl()
        {
            string url = null;
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            url = FrameworkGlobalSettings.WindowsUpdateDataUrl;
#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
            url = FrameworkGlobalSettings.MacOSUpdateDataUrl;
#elif UNITY_IOS
            url = FrameworkGlobalSettings.IOSUpdateDataUrl;
#elif UNITY_ANDROID
            url = FrameworkGlobalSettings.AndroidUpdateDataUrl;
#elif UNITY_WEBGL
            url = FrameworkGlobalSettings.WebGLUpdateDataUrl;
#endif
            return url;
        }
        
        /// <summary>
        /// 获得资源下载路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetResDownLoadPath(string fileName = "")
        {
            return Path.Combine(CompleteDownLoadPath, $"{ResourcesArea.ResAdminType}_{ResourcesArea.ResAdminCode}", GetPlatformName(), fileName).Replace("\\", "/");
        }
        
        /// <summary>
        /// 获取下载路径前缀（平台无关）
        /// </summary>
        public string CompleteDownLoadPath
        {
            get
            {
                string url = "";
                if (ResourcesArea.ServerType == ServerTypeEnum.Extranet)
                {
                    url = ResourcesArea.ExtraResourceSourceUrl;
                }
                else if (ResourcesArea.ServerType == ServerTypeEnum.Formal)
                {
                    url = ResourcesArea.FormalResourceSourceUrl;
                }
                else
                {
                    url = ResourcesArea.InnerResourceSourceUrl;
                }

                return url;
            }
        }
        
        private ServerIpAndPort FindServerIpAndPort(string channelName = "")
        {
            if (string.IsNullOrEmpty(channelName))
            {
                channelName = FrameworkGlobalSettings.CurUseServerChannel;
            }

            foreach (var serverChannelInfo in FrameworkGlobalSettings.ServerChannelInfos)
            {
                if (serverChannelInfo.channelName.Equals(channelName)) //channel相同
                {
                    foreach (var serverIpAndPort in serverChannelInfo.serverIpAndPorts)
                    {
                        if (serverIpAndPort.serverName.Equals(serverChannelInfo.curUseServerName)) //servername相同
                        {
                            return serverIpAndPort;
                        }
                    }
                }
            }

            return null;
        }
        
        /// <summary>
        /// 获取服务器ip
        /// </summary>
        /// <param name="channelName"></param>
        /// <returns></returns>
        public string GetServerIp(string channelName = "")
        {
            ServerIpAndPort serverIpAndPort = FindServerIpAndPort(channelName);
            if (serverIpAndPort != null)
            {
                return serverIpAndPort.ip;
            }

            return string.Empty;
        }
        
        /// <summary>
        /// 获取服务器port
        /// </summary>
        /// <param name="channelName"></param>
        /// <returns></returns>
        public int GetServerPort(string channelName = "")
        {
            ServerIpAndPort serverIpAndPort = FindServerIpAndPort(channelName);
            if (serverIpAndPort != null)
            {
                return serverIpAndPort.port;
            }

            return 0;
        }
        
        /// <summary>
        /// 从Resources加载全局配置
        /// </summary>
        /// <param name="assetsPath"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private T GetSingletonAssetsByResources<T>(string assetsPath) where T : ScriptableObject, new()
        {
            string assetType = typeof(T).Name;
#if UNITY_EDITOR
            //检查唯一性
            string[] globalAssetPaths = UnityEditor.AssetDatabase.FindAssets($"t:{assetType}");
            if (globalAssetPaths.Length > 1)
            {
                foreach (var assetPath in globalAssetPaths)
                {
                    Debug.LogError($"Could not had Multiple {assetType}. Repeated Path: {UnityEditor.AssetDatabase.GUIDToAssetPath(assetPath)}");
                }

                throw new Exception($"Could not had Multiple {assetType}");
            }
#endif
            T customGlobalSettings = Resources.Load<T>(assetsPath);
            if (customGlobalSettings == null)
            {
                Log.Error($"Could not found {assetType} asset，so auto create:{assetsPath}.");
                return null;
            }

            return customGlobalSettings;
        }
        
        
        /// <summary>
        /// 平台名字
        /// </summary>
        /// <returns></returns>
        public string GetPlatformName()
        {
#if UNITY_ANDROID
        return "Android";
#elif UNITY_IOS
        return "IOS";
#else
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                    return "Windows64";
                case RuntimePlatform.WindowsPlayer:
                    return "Windows64";

                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                    return "MacOS";

                case RuntimePlatform.IPhonePlayer:
                    return "IOS";

                case RuntimePlatform.Android:
                    return "Android";
                case RuntimePlatform.WebGLPlayer:
                    return "WebGL";
                default:
                    throw new NotSupportedException($"Platform '{Application.platform.ToString()}' is not supported.");
            }
#endif
        }
        
        /// <summary>
        /// 获取UI映射规则
        /// </summary>
        /// <returns></returns>
        public List<ScriptGenerateRuler> GetScriptGenerateRule()
        {
            return FrameworkGlobalSettings.ScriptGenerateRule;
        }
        
        /// <summary>
        /// 获取UI命名空间
        /// </summary>
        /// <returns></returns>
        public string GetUINameSpace()
        {
            return FrameworkGlobalSettings.NameSpace;
        }
    }
}