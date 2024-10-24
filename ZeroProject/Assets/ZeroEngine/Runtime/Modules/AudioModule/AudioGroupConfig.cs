﻿/****************************************************
  文件：AudioGroupConfig.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年10月12日 15:28:33
  功能：
*****************************************************/

using System;
using UnityEngine;

namespace ZeroEngine
{
    /// <summary>
    /// 音频轨道组配置。
    /// </summary>
    [Serializable]
    public sealed class AudioGroupConfig
    {
        [SerializeField] private string m_Name = null;

        [SerializeField] private bool m_Mute = false;

        [SerializeField, Range(0f, 1f)] private float m_Volume = 1f;

        [SerializeField] private int m_AgentHelperCount = 1;

        public AudioType AudioType;

        public AudioRolloffMode audioRolloffMode = AudioRolloffMode.Logarithmic;

        public float minDistance = 1f;

        public float maxDistance = 500f;

        public string Name
        {
            get { return m_Name; }
        }

        public bool Mute
        {
            get { return m_Mute; }
        }

        public float Volume
        {
            get { return m_Volume; }
        }

        public int AgentHelperCount
        {
            get { return m_AgentHelperCount; }
        }
    }
}