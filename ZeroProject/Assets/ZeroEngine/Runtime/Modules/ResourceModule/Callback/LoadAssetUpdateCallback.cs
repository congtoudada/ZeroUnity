/****************************************************
  文件：LoadAssetUpdateCallback.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月03日 20:25:55
  功能：
*****************************************************/

namespace ZeroEngine
{
    /// <summary>
    /// 加载资源更新回调函数。
    /// </summary>
    /// <param name="assetName">要加载的资源名称。</param>
    /// <param name="progress">加载资源进度。</param>
    /// <param name="userData">用户自定义数据。</param>
    public delegate void LoadAssetUpdateCallback(string assetName, float progress, object userData);
}
