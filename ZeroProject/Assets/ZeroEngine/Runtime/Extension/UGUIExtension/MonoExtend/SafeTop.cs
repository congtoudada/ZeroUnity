/****************************************************
  文件：SafeTop.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年10月16日 17:23:24
  功能：
*****************************************************/
using UnityEngine;

namespace ZeroEngine
{
    public class SafeTop : MonoBehaviour
    {
        void Start()
        {
            var topRect = gameObject.transform as RectTransform;
            CheckNotch(true);
            if (topRect != null)
            {
                var anchoredPosition = topRect.anchoredPosition;
                anchoredPosition = new Vector2(anchoredPosition.x, anchoredPosition.y - _notchHeight);
                topRect.anchoredPosition = anchoredPosition;
            }
        }

        private static float _notchHeight;

        public static void CheckNotch(bool applyEditorNotch = true)
        {
#if UNITY_EDITOR
            _notchHeight = applyEditorNotch ? Screen.safeArea.y > 0f ? Screen.safeArea.y : Screen.currentResolution.height - Screen.safeArea.height : 0f;
            if (_notchHeight < 0)
            {
                _notchHeight = 0;
            }
#else
            _notchHeight = Screen.safeArea.y > 0f ? Screen.safeArea.y : Screen.currentResolution.height - Screen.currentResolution.height;
#endif
            Debug.Log($"CheckNotch :{_notchHeight}");
        }
    }
}