/****************************************************
  文件：DefaultJsonHelper.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年9月2日 17:26:25
  功能：
*****************************************************/

using System;
using System.IO;
using YamlDotNet.Serialization;

namespace ZeroEngine
{
    /// <summary>
    /// 默认 JSON 函数集辅助器。
    /// </summary>
    public class DefaultYamlHelper : Utility.Yaml.IYamlHelper
    {
        /// <summary>
        /// 将对象序列化为 Yaml 字符串。
        /// </summary>
        /// <param name="obj">要序列化的对象。</param>
        /// <returns>序列化后的 Yaml 字符串。</returns>
        public string ToYaml(object obj)
        {
            var yaml = new Serializer().Serialize(obj);
            return yaml;
        }

        /// <summary>
        /// 将 Yaml 字符串反序列化为对象。
        /// </summary>
        /// <typeparam name="T">对象类型。</typeparam>
        /// <param name="yaml">要反序列化的 Yaml 字符串。</param>
        /// <returns>反序列化后的对象。</returns>
        public T ToObject<T>(string yaml)
        {
            return new Deserializer().Deserialize<T>(yaml);
        }

        /// <summary>
        /// 将 Yaml 字符串反序列化为对象。
        /// </summary>
        /// <param name="objectType">对象类型。</param>
        /// <param name="yaml">要反序列化的 Yaml 字符串。</param>
        /// <returns>反序列化后的对象。</returns>
        public object ToObject(Type objectType, string yaml)
        {
            return new Deserializer().Deserialize(yaml);
        }

        /// <summary>
        /// Yaml字符串转Json字符串
        /// </summary>
        /// <param name="yaml"></param>
        /// <returns></returns>
        public string YamlToJson(string yaml)
        {
            var deserializer = new DeserializerBuilder().Build();
            var yamlObject = deserializer.Deserialize(new StringReader(yaml));
            if (yamlObject != null)
            {
                var serializer = new SerializerBuilder()
                    .JsonCompatible()
                    .Build();
                return serializer.Serialize(yamlObject);
            }
            return "";
        }
    }
}