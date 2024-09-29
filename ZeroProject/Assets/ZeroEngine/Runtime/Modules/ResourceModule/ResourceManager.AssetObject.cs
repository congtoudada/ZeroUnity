/****************************************************
  文件：ResourceManager_AssetObject.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月04日 19:53:07
  功能：
*****************************************************/

using System.Collections.Generic;
using YooAsset;

namespace ZeroEngine
{
    internal partial class ResourceManager
    {
        /// <summary>
        /// 资源对象。
        /// </summary>
        private sealed class AssetObject : ObjectBase
        {
            private AssetHandle m_AssetHandle;
            private ResourceManager m_ResourceManager;

            public AssetObject()
            {
                m_AssetHandle = null;
            }

            public static AssetObject Create(string name, object target, object assetHandle, ResourceManager resourceManager)
            {
                if (assetHandle == null)
                {
                    throw new GameFrameworkException("Resource is invalid.");
                }

                if (resourceManager == null)
                {
                    throw new GameFrameworkException("Resource Manager is invalid.");
                }

                AssetObject assetObject = MemoryPool.Acquire<AssetObject>();
                assetObject.Initialize(name, target);
                assetObject.m_AssetHandle = (AssetHandle)assetHandle;
                assetObject.m_ResourceManager = resourceManager;
                return assetObject;
            }

            /// <summary>
            /// 内存池释放该对象时调用
            /// </summary>
            public override void Clear()
            {
                base.Clear();
                m_AssetHandle = null;
            }
            
            /// <summary>
            /// 对象池释放该对象时调用
            /// </summary>
            /// <param name="isShutdown"></param>
            protected internal override void Release(bool isShutdown)
            {
                if (!isShutdown)
                {
                    AssetHandle handle = m_AssetHandle;
                    if (handle is { IsValid: true })
                    {
                        handle.Dispose();
                    }
                    handle = null;
                }
            }
        }
    }
}