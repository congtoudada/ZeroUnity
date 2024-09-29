/****************************************************
  文件：LoadSceneFailureCallback.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月03日 20:30:50
  功能：
*****************************************************/

namespace ZeroEngine
{
    /// <summary>
    /// 加载场景失败回调函数。
    /// </summary>
    /// <param name="sceneAssetName">要加载的场景资源名称。</param>
    /// <param name="status">加载场景状态。</param>
    /// <param name="errorMessage">错误信息。</param>
    /// <param name="userData">用户自定义数据。</param>
    public delegate void LoadSceneFailureCallback(string sceneAssetName, LoadResourceStatus status, string errorMessage, object userData);
}
