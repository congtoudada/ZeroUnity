/****************************************************
  文件：LoadAssetObject.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月29日 14:45:27
  功能：
*****************************************************/

using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace ZeroEngine
{
    [Serializable]
    public class LoadAssetObject
    {
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public ISetAssetObject AssetObject { get; }
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public UnityEngine.Object AssetTarget { get; }
#if UNITY_EDITOR
        public bool IsSelect { get; set; }
#endif
        public LoadAssetObject(ISetAssetObject obj, UnityEngine.Object assetTarget)
        {
            AssetObject = obj;
            AssetTarget = assetTarget;
        }
    }
}