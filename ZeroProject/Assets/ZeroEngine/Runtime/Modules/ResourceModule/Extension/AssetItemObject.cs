/****************************************************
  文件：AssetItemObject.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月29日 14:38:05
  功能：
*****************************************************/

namespace ZeroEngine
{
    public class AssetItemObject : ObjectBase
    {
        public static AssetItemObject Create(string location, UnityEngine.Object target)
        {
            AssetItemObject item = MemoryPool.Acquire<AssetItemObject>();
            item.Initialize(location, target);
            return item;
        }

        protected internal override void Release(bool isShutdown)
        {
            if (Target == null)
            {
                return;
            }

            if (GameModule.Resource != null)
            {
                GameModule.Resource.UnloadAsset(Target);
            }
        }
    }
}