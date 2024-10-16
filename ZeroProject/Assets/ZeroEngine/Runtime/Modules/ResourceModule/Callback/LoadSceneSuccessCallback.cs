﻿/****************************************************
  文件：LoadSceneSuccessCallback.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月03日 20:31:10
  功能：
*****************************************************/

namespace ZeroEngine
{
    /// <summary>
    /// 加载场景成功回调函数。
    /// </summary>
    /// <param name="sceneAssetName">要加载的场景资源名称。</param>
    /// <param name="scene">场景对象。</param>
    /// <param name="duration">加载持续时间。</param>
    /// <param name="userData">用户自定义数据。</param>
    public delegate void LoadSceneSuccessCallback(string sceneAssetName,UnityEngine.SceneManagement.Scene scene, float duration, object userData);
}