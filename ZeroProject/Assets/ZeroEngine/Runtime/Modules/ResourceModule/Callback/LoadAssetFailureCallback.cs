/****************************************************
  文件：LoadAssetFailureCallback.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月03日 20:25:02
  功能：
*****************************************************/

namespace ZeroEngine
{
    /// <summary>
    /// 加载资源失败回调函数。
    /// </summary>
    /// <param name="assetName">要加载的资源名称。</param>
    /// <param name="status">加载资源状态。</param>
    /// <param name="errorMessage">错误信息。</param>
    /// <param name="userData">用户自定义数据。</param>
    public delegate void LoadAssetFailureCallback(string assetName, LoadResourceStatus status, string errorMessage, object userData);
}
