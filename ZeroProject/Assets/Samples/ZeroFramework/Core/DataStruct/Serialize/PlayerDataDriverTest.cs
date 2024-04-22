/****************************************************
  文件：PlayerDataDriverTest.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月21日 14:16:26
  功能：
*****************************************************/

using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ZeroFramework.Samples
{
  public class PlayerDataDriverTest : MonoBehaviour
  {
    private void Start()
    {
      PlayerDataTest data = new PlayerDataTest()
      {
        Id = 1,
        Name = "congtou"
      };
      //序列化到硬盘再读取
      Debug.Log("---------------------- Disk ------------------");
      string output_path = Path.Combine(Application.dataPath,
        "Samples/ZeroFramework/Core/DataStruct/Serialize/Output/playerDataTest.dat");
      PlayerDataSerializerTest.SerializeToDisk(output_path, data);
      FileStream stream = new FileStream(output_path, FileMode.Open);
      PlayerDataTest data1 = PlayerDataSerializerTest.Deserialize(stream);
      Debug.Log(data1.ToString());
    
      //序列化到Memory再读取
      Debug.Log("---------------------- Memory ------------------");
      byte[] ret = PlayerDataSerializerTest.SerializeToMemory(data);
      MemoryStream stream2 = new MemoryStream(ret);
      PlayerDataTest data2 = PlayerDataSerializerTest.Deserialize(stream2);
      Debug.Log(data2.ToString());
      //手动计算内存
      // Debug.Log(data2.GetBytesLen());
      
      //Marshal.SizeOf仅限值类型对象 16
      //如果使用特殊的内存布局，就会报错。这点真的不如C++方便诶
      //Debug.Log(Marshal.SizeOf<PlayerDataTest>());
      
      //下面测量class的方式不理想，打印0
      // long flag = GC.GetTotalMemory(false);
      // PlayerDataTest t1 = new PlayerDataTest();
      // flag = GC.GetTotalMemory(false) - flag;
      // Debug.Log(flag);

    }
  }
}