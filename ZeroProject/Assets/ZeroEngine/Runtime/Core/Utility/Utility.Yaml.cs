/****************************************************
  文件：Utility_Yaml.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年09月02日 17:14:38
  功能：
*****************************************************/

using System;

namespace ZeroEngine
{
    public static partial class Utility
    {
        /// <summary>
        /// YAML 相关的实用函数。
        /// </summary>
        public static partial class Yaml
        {
            private static IYamlHelper _yamlHelper = new DefaultYamlHelper();

            /// <summary>
            /// 设置 YAML 辅助器。
            /// </summary>
            /// <param name="yamlHelper">要设置的 YAML 辅助器。</param>
            public static void SetYamlHelper(IYamlHelper yamlHelper)
            {
                _yamlHelper = yamlHelper;
            }
            
            /// <summary>
            /// 将对象序列化为 Yaml 字符串。
            /// </summary>
            /// <param name="obj">要序列化的对象。</param>
            /// <returns>序列化后的 Yaml 字符串。</returns>
            public static string ToYaml(object obj)
            {
                if (_yamlHelper == null)
                {
                    throw new GameFrameworkException("YAML helper is invalid.");
                }
                
                try
                {
                    return _yamlHelper.ToYaml(obj);
                }
                catch (Exception exception)
                {
                    if (exception is GameFrameworkException)
                    {
                        throw;
                    }
                    throw new GameFrameworkException(Text.Format("Can not convert to YAML with exception '{0}'.", exception), exception);
                }

            }
    
            /// <summary>
            /// 将 Yaml 字符串反序列化为对象。
            /// </summary>
            /// <typeparam name="T">对象类型。</typeparam>
            /// <param name="yaml">要反序列化的 Yaml 字符串。</param>
            /// <returns>反序列化后的对象。</returns>
            public static T ToObject<T>(string yaml)
            {
                if (_yamlHelper == null)
                {
                    throw new GameFrameworkException("YAML helper is invalid.");
                }

                try
                {
                    return _yamlHelper.ToObject<T>(yaml);
                }
                catch (Exception exception)
                {
                    if (exception is GameFrameworkException)
                    {
                        throw;
                    }

                    throw new GameFrameworkException(Text.Format("Can not convert to object with exception '{0}'.", exception), exception);
                }
            }
    
            /// <summary>
            /// 将 Yaml 字符串反序列化为对象。
            /// </summary>
            /// <param name="objectType">对象类型。</param>
            /// <param name="yaml">要反序列化的 Yaml 字符串。</param>
            /// <returns>反序列化后的对象。</returns>
            public static object ToObject(Type objectType, string yaml)
            {
                if (_yamlHelper == null)
                {
                    throw new GameFrameworkException("YAML helper is invalid.");
                }

                if (objectType == null)
                {
                    throw new GameFrameworkException("Object type is invalid.");
                }

                try
                {
                    return _yamlHelper.ToObject(objectType, yaml);
                }
                catch (Exception exception)
                {
                    if (exception is GameFrameworkException)
                    {
                        throw;
                    }

                    throw new GameFrameworkException(Text.Format("Can not convert to object with exception '{0}'.", exception), exception);
                }
            }
    
            /// <summary>
            /// Yaml字符串转Json字符串
            /// </summary>
            /// <param name="yaml"></param>
            /// <returns></returns>
            public static string YamlToJson(string yaml)
            {
                return _yamlHelper.YamlToJson(yaml);
            }

        }
    }
}