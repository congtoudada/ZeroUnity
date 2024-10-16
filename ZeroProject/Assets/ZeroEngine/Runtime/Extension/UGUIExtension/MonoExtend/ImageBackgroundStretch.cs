/****************************************************
  文件：ImageBackgroundStretch.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年10月16日 17:51:43
  功能：
*****************************************************/

using UnityEngine;

namespace ZeroEngine
{
    /// <summary>
    /// 背景图片等比拉伸。
    /// </summary>
    public class ImageBackgroundStretch : MonoBehaviour
    {
        public float standardAspectValue = 9 / 16f;
        
        protected virtual void Start()
        {
            DoImageStretch(standardAspectValue);
        }

        private void DoImageStretch(float standardAspect)
        {
            float deviceAspect = Screen.width / (float)Screen.height;
            if (standardAspect > deviceAspect)
            {
                float scale = standardAspect / deviceAspect;
                transform.localScale = new Vector3(scale, scale, 1f);
            }
            else if (standardAspect < deviceAspect)
            {
                float scale = deviceAspect / standardAspect;
                transform.localScale = new Vector3(scale, scale, 1f);
            }
        }
    }
}
