/****************************************************
  文件：ResourceArea.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月22日 14:09:10
  功能：
*****************************************************/

using System;
using Sirenix.OdinInspector;
using UnityEngine;


namespace ZeroFramework
{
    /// <summary>
    /// 资源存放地址。
    /// </summary>
    [Serializable]
    public class ResourcesArea
    {
        [LabelText("资源管理类型")] [SerializeField]
        private string m_ResAdminType = "Default";
        public string ResAdminType => m_ResAdminType;
        
        [LabelText("资源管理编号")] [SerializeField]
        private string m_ResAdminCode = "0";
        public string ResAdminCode => m_ResAdminCode;
        
        [LabelText("服务器类型")] [SerializeField]
        private ServerTypeEnum m_ServerType = ServerTypeEnum.Intranet;
        public ServerTypeEnum ServerType => m_ServerType;
        
        [LabelText("是否在构建资源的时候清理上传到服务端目录的老资源")] [SerializeField]
        private bool m_CleanCommitPathRes = true;
        public bool CleanCommitPathRes => m_CleanCommitPathRes;
        
        [LabelText("内网地址")] [SerializeField] private string m_InnerResourceSourceUrl = "http://127.0.0.1:8088";
        public string InnerResourceSourceUrl => m_InnerResourceSourceUrl;

        [LabelText("外网地址")] [SerializeField] private string m_ExtraResourceSourceUrl = "http://127.0.0.1:8088";
        public string ExtraResourceSourceUrl => m_ExtraResourceSourceUrl;

        [LabelText("正式地址")] [SerializeField] private string m_FormalResourceSourceUrl = "http://127.0.0.1:8088";
        public string FormalResourceSourceUrl => m_FormalResourceSourceUrl;
    }
}