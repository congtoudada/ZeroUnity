/****************************************************
  文件：Utility_File.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月22日 16:54:45
  功能：
*****************************************************/

using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace ZeroFramework.Runtime
{
    /// <summary>
    /// Unity平台路径类型。
    /// </summary>
    public enum UnityPlatformPathType : int
    {
        dataPath = 0,
        streamingAssetsPath,
        persistentDataPath,
        temporaryCachePath,
    }

    public static partial class Utility
    {
        /// <summary>
        /// 文件相关的实用函数。
        /// </summary>
        public static class File
        {
            /// <summary>
            /// 创建文件
            /// </summary>
            /// <param name="filePath"></param>
            /// <param name="isCreateDir"></param>
            /// <returns></returns>
            public static bool CreateFile(string filePath, bool isCreateDir = true)
            {
                if (!System.IO.File.Exists(filePath))
                {
                    string dir = System.IO.Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(dir))
                    {
                        if (isCreateDir)
                        {
                            Directory.CreateDirectory(dir);
                        }
                        else
                        {
#if UNITY_EDITOR
                            EditorUtility.DisplayDialog("Tips", "文件夹不存在", "CANCEL");
#endif
                            Log.Error("文件夹不存在 Path=" + dir);
                            return false;
                        }
                    }

                    System.IO.File.Create(filePath);
                }

                return true;
            }
            
            /// <summary>
            /// 创建or追加数据
            /// </summary>
            /// <param name="filePath"></param>
            /// <param name="info"></param>
            /// <param name="isCreateDir"></param>
            /// <returns></returns>
            public static bool CreateFile(string filePath, string info, bool isCreateDir = true)
            {
                StreamWriter sw;
                FileInfo t = new FileInfo(filePath);
                if (!t.Exists)
                {
                    string dir = System.IO.Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(dir))
                    {
                        if (isCreateDir)
                        {
                            Directory.CreateDirectory(dir);
                        }
                        else
                        {
#if UNITY_EDITOR
                            EditorUtility.DisplayDialog("Tips", "文件夹不存在", "CANCEL");
#endif
                            Log.Error("文件夹不存在 Path=" + dir);
                            return false;
                        }
                    }

                    sw = t.CreateText();
                }
                else
                {
                    sw = t.AppendText();
                }

                sw.WriteLine(info);
                sw.Close();
                sw.Dispose();
                return true;
            }
            
            public static string GetPath(string path)
            {
                return path.Replace("\\", "/");
            }
            
            /// <summary>
            /// MD5加密文件(不可逆)
            /// </summary>
            /// <param name="pathName"></param>
            /// <returns></returns>
            public static string Md5ByPathName(string pathName)
            {
                try
                {
                    FileStream file = new FileStream(pathName, FileMode.Open);
                    System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                    byte[] retVal = md5.ComputeHash(file);
                    file.Close();

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < retVal.Length; i++)
                    {
                        sb.Append(retVal[i].ToString("x2"));
                    }

                    return sb.ToString();
                }
                catch (Exception ex)
                {
                    Log.Error("to md5 fail,error:" + ex.Message);
                    return "Error";
                }
            }
            
            /// <summary>
            /// 根据字节数返回相应大小的字符串 (Bytes,KB,MB,GB,TB,PB,EB)
            /// :F2 是格式化字符串的一种方式，它用于指定浮点数的格式化输出。具体来说，F2 表示将浮点数保留两位小数。
            /// </summary>
            /// <param name="byteLength"></param>
            /// <returns></returns>
            public static string GetByteLengthString(long byteLength)
            {
                if (byteLength < 1024L) // 2 ^ 10
                {
                    return Utility.Text.Format("{0} Bytes", byteLength.ToString());
                }

                if (byteLength < 1048576L) // 2 ^ 20
                {
                    return Utility.Text.Format("{0} KB", (byteLength / 1024f).ToString("F2"));
                }

                if (byteLength < 1073741824L) // 2 ^ 30
                {
                    return Utility.Text.Format("{0} MB", (byteLength / 1048576f).ToString("F2"));
                }

                if (byteLength < 1099511627776L) // 2 ^ 40
                {
                    return Utility.Text.Format("{0} GB", (byteLength / 1073741824f).ToString("F2"));
                }

                if (byteLength < 1125899906842624L) // 2 ^ 50
                {
                    return Utility.Text.Format("{0} TB", (byteLength / 1099511627776f).ToString("F2"));
                }

                if (byteLength < 1152921504606846976L) // 2 ^ 60
                {
                    return Utility.Text.Format("{0} PB", (byteLength / 1125899906842624f).ToString("F2"));
                }

                return Utility.Text.Format("{0} EB", (byteLength / 1152921504606846976f).ToString("F2"));
            }
            
            /// <summary>
            /// 根据字节数组获取utf8 string
            /// </summary>
            /// <param name="total"></param>
            /// <returns></returns>
            public static string BinToUtf8(byte[] total)
            {
                byte[] result = total;
                if (total[0] == 0xef && total[1] == 0xbb && total[2] == 0xbf)
                {
                    // utf8文件的前三个字节为特殊占位符，要跳过
                    result = new byte[total.Length - 3];
                    System.Array.Copy(total, 3, result, 0, total.Length - 3);
                }

                string utf8string = System.Text.Encoding.UTF8.GetString(result);
                return utf8string;
            }
            
            /// <summary>
            /// 获取文件大小
            /// </summary>
            /// <param name="path">文件路径</param>
            /// <returns></returns>
            public static long GetFileSize(string path)
            {
                using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    return file.Length;
                }
            }
        }
    }
}