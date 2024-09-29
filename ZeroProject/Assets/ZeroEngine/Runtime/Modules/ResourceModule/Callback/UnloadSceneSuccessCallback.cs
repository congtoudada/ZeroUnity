/****************************************************
  文件：UnloadSceneSuccessCallback.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月03日 20:36:09
  功能：
*****************************************************/

namespace ZeroEngine
{
    /// <summary>
    /// 卸载场景成功回调函数。
    /// </summary>
    /// <param name="sceneAssetName">要卸载的场景资源名称。</param>
    /// <param name="userData">用户自定义数据。</param>
    public delegate void UnloadSceneSuccessCallback(string sceneAssetName, object userData);
}
