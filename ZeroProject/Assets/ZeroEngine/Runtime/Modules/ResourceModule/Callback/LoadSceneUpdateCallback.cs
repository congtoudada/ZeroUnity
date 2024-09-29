/****************************************************
  文件：LoadSceneUpdateCallback.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月03日 20:31:33
  功能：
*****************************************************/

namespace ZeroEngine
{
    /// <summary>
    /// 加载场景更新回调函数。
    /// </summary>
    /// <param name="sceneAssetName">要加载的场景资源名称。</param>
    /// <param name="progress">加载场景进度。</param>
    /// <param name="userData">用户自定义数据。</param>
    public delegate void LoadSceneUpdateCallback(string sceneAssetName, float progress, object userData);
}