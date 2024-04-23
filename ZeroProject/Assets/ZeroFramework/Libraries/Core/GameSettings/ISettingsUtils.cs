/****************************************************
  文件：ISettingsUtils.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月22日 15:25:06
  功能：
*****************************************************/

using System;
using System.Collections.Generic;

namespace ZeroFramework
{
    interface ISettingsUtils
    {
        /// <summary>
        /// Zero全局配置
        /// </summary>
        ZeroSettings GlobalSettings { get; }

        /// <summary>
        /// 获取FrameworkGlobalSettings
        /// </summary>
        FrameworkGlobalSettings FrameworkGlobalSettings { get; }
        
        /// <summary>
        /// 获取HybridCLRCustomGlobalSettings
        /// </summary>
        HybridCLRCustomGlobalSettings HybridCLRCustomGlobalSettings { get; }
        
        /// <summary>
        /// 获取ResourcesArea
        /// </summary>
        ResourcesArea ResourcesArea { get; }

        /// <summary>
        /// 设置热更程序集
        /// </summary>
        /// <param name="hotUpdateAssemblies"></param>
        void SetHybridCLRHotUpdateAssemblies(List<string> hotUpdateAssemblies);

        /// <summary>
        /// 设置AOT程序集
        /// </summary>
        /// <param name="aOTMetaAssemblies"></param>
        void SetHybridCLRAOTMetaAssemblies(List<string> aOTMetaAssemblies);

        /// <summary>
        /// 是否启用UpdateData
        /// </summary>
        /// <returns></returns>
        bool EnableUpdateData();

        /// <summary>
        /// 根据当前平台获取相应UpdateDataURL
        /// </summary>
        /// <returns></returns>
        string GetUpdateDataUrl();

        /// <summary>
        /// 获得资源下载路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string GetResDownLoadPath(string fileName = "");

        /// <summary>
        /// 获取下载路径前缀（平台无关）
        /// </summary>
        string CompleteDownLoadPath { get; }

        /// <summary>
        /// 根据channel获取ip（不传则默认为框架当前channel）
        /// </summary>
        /// <param name="channelName"></param>
        /// <returns></returns>
        string GetServerIp(string channelName = "");

        /// <summary>
        /// 根据channel获取port（不传则默认为框架当前channel）
        /// </summary>
        /// <param name="channelName"></param>
        /// <returns></returns>
        int GetServerPort(string channelName = "");

        /// <summary>
        /// 平台名字
        /// </summary>
        /// <returns></returns>
        string GetPlatformName();

        /// <summary>
        /// 获取UI映射规则
        /// </summary>
        /// <returns></returns>
        List<ScriptGenerateRuler> GetScriptGenerateRule();

        /// <summary>
        /// 获取UI命名空间
        /// </summary>
        /// <returns></returns>
        string GetUINameSpace();
    }
}