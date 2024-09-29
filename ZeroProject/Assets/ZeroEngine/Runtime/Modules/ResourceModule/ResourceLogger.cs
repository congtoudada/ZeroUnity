/****************************************************
  文件：ResourceLogger.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月03日 22:46:04
  功能：
*****************************************************/

namespace ZeroEngine
{
    internal class ResourceLogger : YooAsset.ILogger
    {
        public void Log(string message)
        {
            ZeroEngine.Log.Info(message);
        }

        public void Warning(string message)
        {
            ZeroEngine.Log.Warning(message);
        }

        public void Error(string message)
        {
            ZeroEngine.Log.Error(message);
        }

        public void Exception(System.Exception exception)
        {
            ZeroEngine.Log.Fatal(exception.Message);
        }
    }
}