/****************************************************
  文件：ISetAssetObject.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月29日 14:44:31
  功能：
*****************************************************/

namespace ZeroEngine
{
    public interface ISetAssetObject : IMemory
    {
        /// <summary>
        /// 资源定位地址。
        /// </summary>
        string Location { get; }

        /// <summary>
        /// 设置资源。
        /// </summary>
        void SetAsset(UnityEngine.Object asset);

        /// <summary>
        /// 是否可以回收。
        /// </summary>
        bool IsCanRelease();
    }
}