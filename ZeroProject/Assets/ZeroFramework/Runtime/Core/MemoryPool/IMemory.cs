/****************************************************
  文件：IMemory.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024/4/18 17:06:15
  功能：
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroFramework.Runtime
{
  /// <summary>
  /// 内存对象Interface。
  /// </summary>
  public interface IMemory
  {
    /// <summary>
    /// 从内存池获取对象时调用
    /// </summary>
    void OnGet();
    
    /// <summary>
    /// 清理内存对象回收入池时调用
    /// </summary>
    void OnRelease();
  }
}