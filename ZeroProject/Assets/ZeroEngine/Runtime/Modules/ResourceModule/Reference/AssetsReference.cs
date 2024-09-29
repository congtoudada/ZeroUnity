/****************************************************
  文件：AssetsReference.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月03日 20:39:46
  功能：
*****************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ZeroEngine
{
    [Serializable]
    public struct AssetsRefInfo
    {
        public int instanceId;

        public Object refAsset;

        public AssetsRefInfo(Object refAsset)
        {
            this.refAsset = refAsset;
            instanceId = this.refAsset.GetInstanceID();
        }
    }

    public sealed class AssetsReference : MonoBehaviour
    {
        [SerializeField] private GameObject _sourceGameObject;

        [SerializeField] private List<AssetsRefInfo> _refAssetInfoList;

        private IResourceManager _resourceManager;

        /// <summary>
        /// 在OnDestroy时减少持有内容的引用计数
        /// </summary>
        /// <exception cref="GameFrameworkException"></exception>
        private void OnDestroy()
        {
            if (_resourceManager == null)
            {
                _resourceManager = ModuleImpSystem.GetModule<IResourceManager>();
            }

            if (_resourceManager == null)
            {
                throw new GameFrameworkException($"ResourceManager is null.");
            }

            if (_sourceGameObject != null)
            {
                _resourceManager.UnloadAsset(_sourceGameObject);
            }

            if (_refAssetInfoList != null)
            {
                foreach (var refInfo in _refAssetInfoList)
                {
                    _resourceManager.UnloadAsset(refInfo.refAsset);
                }

                _refAssetInfoList.Clear();
                _refAssetInfoList = null;
            }
        }

        public AssetsReference Ref(GameObject source, IResourceManager resourceManager = null)
        {
            if (source == null)
            {
                throw new GameFrameworkException($"Source gameObject is null.");
            }

            if (source.scene.name != null)
            {
                throw new GameFrameworkException($"Source gameObject is in scene.");
            }

            _resourceManager = resourceManager;
            _sourceGameObject = source;
            return this;
        }

        public AssetsReference Ref<T>(T source, IResourceManager resourceManager = null) where T : UnityEngine.Object
        {
            if (source == null)
            {
                throw new GameFrameworkException($"Source gameObject is null.");
            }

            _resourceManager = resourceManager;
            if (_refAssetInfoList == null)
            {
                _refAssetInfoList = new List<AssetsRefInfo>();
            }
            _refAssetInfoList.Add(new AssetsRefInfo(source));
            return this;
        }

        public static AssetsReference Instantiate(GameObject source, Transform parent = null, IResourceManager resourceManager = null)
        {
            if (source == null)
            {
                throw new GameFrameworkException($"Source gameObject is null.");
            }

            if (source.scene.name != null)
            {
                throw new GameFrameworkException($"Source gameObject is in scene.");
            }

            GameObject instance = Object.Instantiate(source, parent);
            return instance.AddComponent<AssetsReference>().Ref(source, resourceManager);
        }

        public static AssetsReference Ref(GameObject source, GameObject instance, IResourceManager resourceManager = null)
        {
            if (source == null)
            {
                throw new GameFrameworkException($"Source gameObject is null.");
            }

            if (source.scene.name != null)
            {
                throw new GameFrameworkException($"Source gameObject is in scene.");
            }

            return instance.GetOrAddComponent<AssetsReference>().Ref(source, resourceManager);
        }

        public static AssetsReference Ref<T>(T source, GameObject instance, IResourceManager resourceManager = null) where T : UnityEngine.Object
        {
            if (source == null)
            {
                throw new GameFrameworkException($"Source gameObject is null.");
            }

            return instance.GetOrAddComponent<AssetsReference>().Ref(source, resourceManager);
        }
    }
}