/****************************************************
  文件：Utility_Path.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年08月31日 16:06:09
  功能：
*****************************************************/

using System.IO;
using UnityEngine;

namespace ZeroEngine
{
    public static partial class Utility
    {
        /// <summary>
        /// 路径相关的实用函数。
        /// </summary>
        public static class Path
        {
            #region 原始
            /// <summary>
            /// 获取规范的路径。
            /// </summary>
            /// <param name="path">要规范的路径。</param>
            /// <returns>规范的路径。</returns>
            public static string GetRegularPath(string path)
            {
                if (path == null)
                {
                    return null;
                }

                return path.Replace('\\', '/');
            }

            /// <summary>
            /// 获取远程格式的路径（带有file:// 或 http:// 前缀）。
            /// </summary>
            /// <param name="path">原始路径。</param>
            /// <param name="prefix">路径前缀</param>
            /// <returns>远程格式路径。</returns>
            public static string GetRemotePath(string path, string prefix="file:///")
            {
                string regularPath = GetRegularPath(path);
                if (regularPath == null)
                {
                    return null;
                }

                return regularPath.Contains("://") ? regularPath : (prefix + regularPath).Replace(prefix+"/", prefix);
            }

            /// <summary>
            /// 移除空文件夹。
            /// </summary>
            /// <param name="directoryName">要处理的文件夹名称。</param>
            /// <returns>是否移除空文件夹成功。</returns>
            public static bool RemoveEmptyDirectory(string directoryName)
            {
                if (string.IsNullOrEmpty(directoryName))
                {
                    throw new GameFrameworkException("Directory name is invalid.");
                }

                try
                {
                    if (!Directory.Exists(directoryName))
                    {
                        return false;
                    }

                    // 不使用 SearchOption.AllDirectories，以便于在可能产生异常的环境下删除尽可能多的目录
                    string[] subDirectoryNames = Directory.GetDirectories(directoryName, "*");
                    int subDirectoryCount = subDirectoryNames.Length;
                    foreach (string subDirectoryName in subDirectoryNames)
                    {
                        if (RemoveEmptyDirectory(subDirectoryName))
                        {
                            subDirectoryCount--;
                        }
                    }

                    if (subDirectoryCount > 0)
                    {
                        return false;
                    }

                    if (Directory.GetFiles(directoryName, "*").Length > 0)
                    {
                        return false;
                    }

                    Directory.Delete(directoryName);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            #endregion
            
            #region 拓展
            /// <summary>
            /// 将Assets相对路径转换为绝对路径 (Assets/xxx --> E:/yyy/Assets/xxx)
            /// </summary>
            /// <param name="assetRelativePath"></param>
            /// <returns></returns>
            public static string AssetsRelativeToAbsolute(string assetRelativePath)
            {
                if (assetRelativePath.StartsWith("Assets"))
                {
                    return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.dataPath), assetRelativePath)
                        .Replace("\\", "/");
                }
            
                return System.IO.Path.Combine(Application.dataPath, assetRelativePath)
                    .Replace("\\", "/");
            }

            /// <summary>
            /// 获取ZeroFrameowrk的绝对路径
            /// </summary>
            /// <returns></returns>
            public static string GetZeroFolderAbsolute()
            {
                return AssetsRelativeToAbsolute(Directory.GetDirectories("Assets", "ZeroEngine", SearchOption.AllDirectories)[0]);
            }

            /// <summary>
            /// 返回ZeroFramework相对于Asstes的路径（含Assets）
            /// </summary>
            /// <returns></returns>
            public static string GetZeroFolderRelative()
            {
                return Directory.GetDirectories("Assets", "ZeroEngine", SearchOption.AllDirectories)[0];
            }
            
            public static string GetStreamingAssetsPath()
            {
#if UNITY_EDITOR
                return Application.streamingAssetsPath + "/Win/";
#elif UNITY_STANDALONE_WIN
                return Application.streamingAssetsPath + "/Win/";
#elif UNITY_ANDROID
                return Application.streamingAssetsPath + "/Android/";
#elif UNITY_IOS
                return Application.streamingAssetsPath + "/IOS/";
#else
                return Application.streamingAssetsPath + "/Win/";
#endif
            }
            #endregion
        }
    }
}
