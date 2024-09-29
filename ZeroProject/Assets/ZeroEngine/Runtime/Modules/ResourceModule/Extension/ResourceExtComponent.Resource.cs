/****************************************************
  文件：ResourceExtComponent_Resource.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月29日 14:36:00
  功能：
*****************************************************/

using Cysharp.Threading.Tasks;

namespace ZeroEngine
{
    public partial class ResourceExtComponent
    {
        /// <summary>
        /// 资源组件。
        /// </summary>
        private ResourceModule m_ResourceModule;

        private LoadAssetCallbacks m_LoadAssetCallbacks;

        private void InitializedResources()
        {
            m_ResourceModule = GameModule.Get<ResourceModule>();
            m_LoadAssetCallbacks = new LoadAssetCallbacks(OnLoadAssetSuccess, OnLoadAssetFailure);
        }

        private void OnLoadAssetFailure(string assetName, LoadResourceStatus status, string errormessage, object userdata)
        {
            _assetLoadingList.Remove(assetName);
            Log.Error("Can not load asset from '{1}' with error message '{2}'.", assetName, errormessage);
        }

        private void OnLoadAssetSuccess(string assetName, object asset, float duration, object userdata)
        {
            _assetLoadingList.Remove(assetName);
            ISetAssetObject setAssetObject = (ISetAssetObject)userdata;
            UnityEngine.Object assetObject = asset as UnityEngine.Object;
            if (assetObject != null)
            {
                m_AssetItemPool.Register(AssetItemObject.Create(setAssetObject.Location, assetObject), true);
                SetAsset(setAssetObject, assetObject);
            }
            else
            {
                Log.Error($"Load failure asset type is {asset.GetType()}.");
            }
        }

        /// <summary>
        /// 通过资源系统设置资源。
        /// </summary>
        /// <param name="setAssetObject">需要设置的对象。</param>
        public async UniTaskVoid SetAssetByResources<T>(ISetAssetObject setAssetObject) where T : UnityEngine.Object
        {
            await TryWaitingLoading(setAssetObject.Location);
            
            //如果共享对象池存在该对象的缓存，则直接绑定
            if (m_AssetItemPool.CanSpawn(setAssetObject.Location))
            {
                var assetObject = (T)m_AssetItemPool.Spawn(setAssetObject.Location).Target;
                SetAsset(setAssetObject, assetObject);
            }
            else
            {
                //如果不存在缓存，则调用ResourceModule模块进行加载
                //在加载成功的回调中，会将资源注册到当前m_AssetItemPool中
                _assetLoadingList.Add(setAssetObject.Location);
                m_ResourceModule.LoadAssetAsync(setAssetObject.Location, typeof(T), m_LoadAssetCallbacks, setAssetObject);
            }
        }
    }
}