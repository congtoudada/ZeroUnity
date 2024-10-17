/****************************************************
  文件：SettingsMenu.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年10月17日 16:01:19
  功能：
*****************************************************/

using UnityEditor;

public static class SettingsMenu
{
    [MenuItem("ZeroEngine/Settings/ZeroEngineSettings", priority = -1)]
    public static void OpenSettings() => SettingsService.OpenProjectSettings("ZeroEngine/ZeroEngineSettings");
}