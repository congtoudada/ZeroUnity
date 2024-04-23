/****************************************************
  文件：ServerArea.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月22日 14:56:00
  功能：
*****************************************************/

using System;
using System.Collections.Generic;

namespace ZeroFramework
{
    [Serializable]
    public class ServerIpAndPort
    {
        public string serverName;
        public string ip;
        public int port;
    }

    [Serializable]
    public class ServerChannelInfo
    {
        public string channelName;
        public string curUseServerName;
        public List<ServerIpAndPort> serverIpAndPorts;
    }
}