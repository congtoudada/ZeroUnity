/****************************************************
  文件：ErrorLogger.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月29日 16:36:05
  功能：
*****************************************************/
using System;
using UnityEngine;

namespace ZeroEngine
{
    public class ErrorLogger : IDisposable
    {
        public ErrorLogger()
        {
            Application.logMessageReceived += LogHandler;
        }

        public void Dispose()
        {
            Application.logMessageReceived -= LogHandler;
        }

        private void LogHandler(string condition, string stacktrace, LogType type)
        {
            if (type == LogType.Exception)
            {
                string des = $"客户端报错, \n#内容#：---{condition} \n#位置#：---{stacktrace}";
                GameModule.UI.ShowUIAsync<LogUI>(des);
            }
        }
    }
}