/****************************************************
  文件：ReleaseObjectFilterCallback.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月04日 00:17:32
  功能：
*****************************************************/

using System;
using System.Collections.Generic;

namespace ZeroEngine
{
    /// <summary>
    /// 释放对象筛选函数。
    /// </summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="candidateObjects">要筛选的对象集合。</param>
    /// <param name="toReleaseCount">需要释放的对象数量。</param>
    /// <param name="expireTime">对象过期参考时间。</param>
    /// <returns>经筛选需要释放的对象集合。</returns>
    public delegate List<T> ReleaseObjectFilterCallback<T>(List<T> candidateObjects, int toReleaseCount, DateTime expireTime) where T : ObjectBase;
}
