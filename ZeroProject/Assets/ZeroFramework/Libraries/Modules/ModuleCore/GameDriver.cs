/****************************************************
  文件：GameDriver.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月23日 16:42:24
  功能：
*****************************************************/

using System;

namespace ZeroFramework
{
    public class GameDriver  : SingletonMono<GameDriver>
    {
        private void Start()
        {
            GameModule.Instance.Init(gameObject);
        }
    }
}