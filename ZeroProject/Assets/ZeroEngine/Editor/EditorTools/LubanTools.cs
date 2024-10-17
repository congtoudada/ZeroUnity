/****************************************************
  文件：LubanTools.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年10月17日 15:56:01
  功能：
*****************************************************/
using UnityEditor;
using UnityEngine;

namespace ZeroEngine.Editor
{
    public static class LubanTools
    {
        [MenuItem("ZeroEngine/Tools/Luban 转表")]
        public static void BuildLubanExcel()
        {
            Application.OpenURL(Application.dataPath + @"/../../Configs/GameConfig/gen_code_bin_to_project_lazyload.bat");
        }
        
        [MenuItem("ZeroEngine/Tools/打开表格目录")]
        public static void OpenConfigFolder()
        {
            OpenFolderHelper.Execute(Application.dataPath + @"/../../Configs/GameConfig");
        }
    }
}