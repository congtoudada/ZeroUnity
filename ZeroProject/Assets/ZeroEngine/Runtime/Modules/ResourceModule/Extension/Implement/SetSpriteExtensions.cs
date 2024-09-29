/****************************************************
  文件：SetSpriteExtensions.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月29日 14:47:03
  功能：
*****************************************************/

using ZeroEngine;
using UnityEngine;
using UnityEngine.UI;

public static class SetSpriteExtensions
{
    /// <summary>
    /// 设置图片。
    /// </summary>
    /// <param name="image">UI/Image。</param>
    /// <param name="location">资源定位地址。</param>
    /// <param name="setNativeSize">是否使用原始分辨率。</param>
    public static void SetSprite(this Image image, string location, bool setNativeSize = false)
    {
        GameModule.ResourceExt.SetAssetByResources<Sprite>(SetSpriteObject.Create(image, location, setNativeSize)).Forget();
    }

    /// <summary>
    /// 设置图片。
    /// </summary>
    /// <param name="spriteRenderer">2D/SpriteRender。</param>
    /// <param name="location">资源定位地址。</param>
    public static void SetSprite(this SpriteRenderer spriteRenderer, string location)
    {
        GameModule.ResourceExt.SetAssetByResources<Sprite>(SetSpriteObject.Create(spriteRenderer, location)).Forget();
    }
}