/****************************************************
  文件：BuildinFileManifest.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月03日 21:08:37
  功能：
*****************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroEngine
{
    /// <summary>
    /// 内置资源清单
    /// </summary>
    public class BuildinFileManifest : ScriptableObject
    {
        [Serializable]
        public class Element
        {
            public string PackageName;
            public string FileName;
            public string FileCRC32;
        }

        public List<Element> BuildinFiles = new List<Element>();
    }
}