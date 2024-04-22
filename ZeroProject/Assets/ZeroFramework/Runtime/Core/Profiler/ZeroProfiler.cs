/****************************************************
  文件：ZeroProfiler.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024/4/21 9:48:04
  功能：
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Profiling;

namespace ZeroFramework.Runtime
{
    /// <summary>
    /// 游戏框架Profiler分析器类。
    /// </summary>
    public class ZeroProfiler
    {
        /// <summary>
        /// 这个变量表示分析器的等级。当设置了分析器等级后，只有当前代码中的层级小于等于这个等级时，才会进行性能分析。
        /// 如果没有设置分析器等级，或者分析器等级小于0，则不进行限制，所有代码都会被分析。
        /// </summary>
        private static int _profileLevel = -1;
        /// <summary>
        /// 这个变量表示当前代码的层级。每当开始一段代码的自定义采样分析时，层级会增加；结束时，层级会减少。
        /// </summary>
        private static int _currLevel = 0;
        /// <summary>
        /// 这个变量表示当前采样的层级。当开始一段自定义采样分析时，会增加采样层级；结束时，会减少采样层级。
        /// 这个变量用于确保在层级嵌套时正确地结束采样。
        /// </summary>
        private static int _sampleLevel = 0;
        
        /// <summary>
        /// 设置分析器等级。
        /// </summary>
        /// <param name="level">调试器等级。</param>
        public static void SetProfileLevel(int level)
        {
            _profileLevel = level;
        }
        
        /// <summary>
        /// 开始使用自定义采样分析一段代码。
        /// </summary>
        /// <param name="name">用于在Profiler窗口中标识样本的字符串。</param>
        [Conditional("FIRST_PROFILER")]
        public static void BeginFirstSample(string name)
        {
            _currLevel++;
            if (_profileLevel >= 0 && _currLevel > _profileLevel)
            {
                return;
            }

            _sampleLevel++;
            Profiler.BeginSample(name);
        }
        
        /// <summary>
        /// 结束本次自定义采样分析。
        /// </summary>
        [Conditional("FIRST_PROFILER")]
        public static void EndFirstSample()
        {
            if (_currLevel <= _sampleLevel)
            {
                Profiler.EndSample();
                _sampleLevel--;
            }

            _currLevel--;
        }
        
        /// <summary>
        /// 开始使用自定义采样分析一段代码。
        /// </summary>
        /// <param name="name">用于在Profiler窗口中标识样本的字符串。</param>
        [Conditional("ZERO_PROFILER")]
        public static void BeginSample(string name)
        {
            _currLevel++;
            if (_profileLevel >= 0 && _currLevel > _profileLevel)
            {
                return;
            }

            _sampleLevel++;
            Profiler.BeginSample(name);
        }
        
        /// <summary>
        /// 结束本次自定义采样分析。
        /// </summary>
        [Conditional("ZERO_PROFILER")]
        public static void EndSample()
        {
            if (_currLevel <= _sampleLevel)
            {
                Profiler.EndSample();
                _sampleLevel--;
            }

            _currLevel--;
        }
    }
}