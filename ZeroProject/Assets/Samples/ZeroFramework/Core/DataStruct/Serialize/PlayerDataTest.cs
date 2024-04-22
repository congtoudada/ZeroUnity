/****************************************************
文件：PlayerDataTest.cs
作者：聪头
邮箱：1322080797@qq.com
日期：2024/4/21 13:59:09
功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ZeroFramework.Samples
{
    // [StructLayout(LayoutKind.Auto)]
    // public struct PlayerDataTest
    [Serializable]
    public class PlayerDataTest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}";
        }

        // public int GetBytesLen()
        // {
        //     return sizeof(int) + sizeof(char) * Name.Length;
        // }
    }

}
