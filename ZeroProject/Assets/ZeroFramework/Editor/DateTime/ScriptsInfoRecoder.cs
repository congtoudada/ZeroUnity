/************************************************************
  文件: ScriptsInfoRecoder.cs
  作者: 聪头
  邮箱: 1322080797@qq.com
  日期: 2021年3月19日17:00:59
  功能: 记录脚本信息
*************************************************************/
using System;
using System.IO;

namespace Zero.Editor
{
    /// <summary>
    /// 填充脚本中关于日期的注释
    /// </summary>
    public class ScriptsInfoRecoder : UnityEditor.AssetModificationProcessor
    {
        private static void OnWillCreateAsset(string path)
        {
            path = path.Replace(".meta", "");
            if (path.EndsWith(".cs"))
            {
                string str = File.ReadAllText(path);
                str = str.Replace("DateTime", DateTime.UtcNow.AddHours(8).ToString());
                File.WriteAllText(path, str);
            }
        }
    }
}
