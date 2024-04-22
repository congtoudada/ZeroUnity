/****************************************************
  文件：PlayerDataSerializerTest.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月21日 14:01:40
  功能：
*****************************************************/

using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using ZeroFramework.Runtime;

namespace ZeroFramework.Samples
{
  public class PlayerDataSerializerTest : ZeroFrameworkSerializer<PlayerDataTest>
  {
    private static PlayerDataSerializerTest _serializer = new PlayerDataSerializerTest();
    static PlayerDataSerializerTest()
    {
      _serializer.RegisterSerializeCallback(1, _Serialize);
      _serializer.RegisterDeserializeCallback(1, _Deserialize);
    }
    
    protected override byte[] GetHeader()
    {
      // 这里可以定义一个特定的标识头，用于识别序列化的数据类型
      return new byte[] { 0x50, 0x44, 0x53 }; // 例如：PDS
    }
    
    //序列化方法扩展: 本地流
    public static void SerializeToDisk(string path, PlayerDataTest playerData)
    {
      using (FileStream stream = new FileStream(path, FileMode.Create))
      {
        _serializer.CallSerialize(stream, playerData);
      }
      Debug.Log("Player data serialized to Disk.");
    }
    
    //序列化方法扩展: 内存流
    public static byte[] SerializeToMemory(PlayerDataTest playerData)
    {
      byte[] ret;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        _serializer.CallSerialize(memoryStream, playerData);
        ret =  memoryStream.ToArray();
      }
      Debug.Log("Player data serialized to Memory.");
      return ret;
    }
    
    public static PlayerDataTest Deserialize(Stream stream)
    {
      return _serializer.CallDeserialize(stream);
    }
    
    // 序列化方法实现
    private static bool _Serialize(Stream stream, PlayerDataTest playerData)
    {
      // //手动把流包装成二进制流
      // using BinaryWriter writer = new BinaryWriter(stream);
      // writer.Write(playerData.Id);
      // writer.Write(playerData.Name);
      
      //自动序列化，需标记[Serializable]
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      binaryFormatter.Serialize(stream, playerData);
      
      return true; // 表示序列化成功
    }
  
    // 反序列化方法实现
    private static PlayerDataTest _Deserialize(Stream stream)
    {
      //手动反序列化
      // PlayerDataTest playerData = new PlayerDataTest();
      // using BinaryReader reader = new BinaryReader(stream);
      // playerData.Id = reader.ReadInt32();
      // playerData.Name = reader.ReadString();
      
      //自动反序列化，需标记[Serializable]
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      PlayerDataTest playerData = (PlayerDataTest)binaryFormatter.Deserialize(stream);
      return playerData;
    }
  }
}
