/****************************************************
  文件：LoadAssetSuccessCallback.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月03日 20:25:28
  功能：
*****************************************************/

namespace ZeroEngine
{
    /// <summary>
    /// 加载资源成功回调函数。
    /// </summary>
    /// <param name="assetName">要加载的资源名称。</param>
    /// <param name="asset">已加载的资源。</param>
    /// <param name="duration">加载持续时间。</param>
    /// <param name="userData">用户自定义数据。</param>
    public delegate void LoadAssetSuccessCallback(string assetName, object asset, float duration, object userData);
}
