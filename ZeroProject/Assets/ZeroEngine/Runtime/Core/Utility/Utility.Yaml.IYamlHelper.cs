/****************************************************
  文件：Utility_Yaml_IYamlHelper.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月02日 17:22:45
  功能：
*****************************************************/

using System;

namespace ZeroEngine
{
    public static partial class Utility
    {
        public static partial class Yaml
        {
            /// <summary>
            /// Yaml 辅助器接口。
            /// </summary>
            public interface IYamlHelper
            {
                /// <summary>
                /// 将对象序列化为 Yaml 字符串。
                /// </summary>
                /// <param name="obj">要序列化的对象。</param>
                /// <returns>序列化后的 Yaml 字符串。</returns>
                string ToYaml(object obj);

                /// <summary>
                /// 将 Yaml 字符串反序列化为对象。
                /// </summary>
                /// <typeparam name="T">对象类型。</typeparam>
                /// <param name="yaml">要反序列化的 Yaml 字符串。</param>
                /// <returns>反序列化后的对象。</returns>
                T ToObject<T>(string yaml);

                /// <summary>
                /// 将 Yaml 字符串反序列化为对象。
                /// </summary>
                /// <param name="objectType">对象类型。</param>
                /// <param name="yaml">要反序列化的 Yaml 字符串。</param>
                /// <returns>反序列化后的对象。</returns>
                object ToObject(Type objectType, string yaml);

                /// <summary>
                /// Yaml字符串转Json字符串
                /// </summary>
                /// <param name="yaml"></param>
                /// <returns></returns>
                string YamlToJson(string yaml);
            }
        }
    }
}