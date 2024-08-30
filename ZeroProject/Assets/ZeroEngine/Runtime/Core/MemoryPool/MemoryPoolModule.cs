/****************************************************
  文件：MemoryPoolModule.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年08月22日 14:28:27
  功能：
*****************************************************/

using UnityEngine;

namespace ZeroEngine
{
    /// <summary>
    /// 内存强制检查类型。
    /// </summary>
    public enum MemoryStrictCheckType : byte
    {
        /// <summary>
        /// 总是启用。
        /// </summary>
        AlwaysEnable = 0,

        /// <summary>
        /// 仅在开发模式时启用。
        /// </summary>
        OnlyEnableWhenDevelopment,

        /// <summary>
        /// 仅在编辑器中启用。
        /// </summary>
        OnlyEnableInEditor,

        /// <summary>
        /// 总是禁用。
        /// </summary>
        AlwaysDisable,
    }

    /// <summary>
    /// 内存池模块。
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class MemoryPoolModule : Module
    {
        [SerializeField] private MemoryStrictCheckType m_EnableStrictCheck = MemoryStrictCheckType.AlwaysEnable;

        /// <summary>
        /// 获取或设置是否开启强制检查。
        /// </summary>
        public bool EnableStrictCheck
        {
            get => MemoryPool.EnableStrictCheck;
            set
            {
                MemoryPool.EnableStrictCheck = value;
                if (value)
                {
                    Log.Info("Strict checking is enabled for the Memory Pool. It will drastically affect the performance.");
                }
            }
        }

        private void Start()
        {
            switch (m_EnableStrictCheck)
            {
                case MemoryStrictCheckType.AlwaysEnable:
                    EnableStrictCheck = true;
                    break;

                case MemoryStrictCheckType.OnlyEnableWhenDevelopment:
                    EnableStrictCheck = Debug.isDebugBuild;
                    break;

                case MemoryStrictCheckType.OnlyEnableInEditor:
                    EnableStrictCheck = Application.isEditor;
                    break;

                default:
                    EnableStrictCheck = false;
                    break;
            }
        }
    }
}