/****************************************************
  文件：ProfilerDefineSymbols.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年10月17日 15:40:03
  功能：
*****************************************************/

using UnityEditor;

namespace ZeroEngine.Editor
{
    /// <summary>
    /// Profiler分析器宏定义操作类。
    /// </summary>
    public class ProfilerDefineSymbols
    {
        private const string EnableFirstProfiler = "FIRST_PROFILER";
        private const string EnableZeroProfiler = "ZERO_PROFILER";
        
        private static readonly string[] AllProfilerDefineSymbols = new string[]
        {
            EnableFirstProfiler,
            EnableZeroProfiler,
        };
        
        /// <summary>
        /// 禁用所有日志脚本宏定义。
        /// </summary>
        [MenuItem("ZeroEngine/Profiler Define Symbols/Disable All Profiler", false, 30)]
        public static void DisableAllProfiler()
        {
            foreach (string aboveLogScriptingDefineSymbol in AllProfilerDefineSymbols)
            {
                ScriptingDefineSymbols.RemoveScriptingDefineSymbol(aboveLogScriptingDefineSymbol);
            }
        }

        /// <summary>
        /// 开启所有日志脚本宏定义。
        /// </summary>
        [MenuItem("ZeroEngine/Profiler Define Symbols/Enable All Profiler", false, 31)]
        public static void EnableAllProfiler()
        {
            DisableAllProfiler();
            foreach (string aboveLogScriptingDefineSymbol in AllProfilerDefineSymbols)
            {
                ScriptingDefineSymbols.AddScriptingDefineSymbol(aboveLogScriptingDefineSymbol);
            }
        }
    }
}