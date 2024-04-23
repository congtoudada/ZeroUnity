/****************************************************
  文件：ModuleTest.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024/4/23 14:33:58
  功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZeroFramework;

public class ModuleTest : MonoBehaviour
{
  private IEnumerator Start()
  {
    // yield return ModuleRestartTest();
    ObjectPoolTest();
    yield return null;
  }
  
  #region 对象池模块
  private void ObjectPoolTest()
  {
    //单次获取的对象池
    // var singlePool = GameModule.Instance.ObjectPool.CreateSingleSpawnObjectPool<MyAssetItemObject>();
    // GameObject go1 = new GameObject("go1");
    // GameObject go2 = new GameObject("go2");
    // GameObject go3 = new GameObject("go3");
    // singlePool.Register(MyAssetItemObject.Create("", go1, "A"), false);
    // singlePool.Register(MyAssetItemObject.Create("", go2, "B"), false);
    // singlePool.Register(MyAssetItemObject.Create("", go3, "C"), false);
    // var obj1 = singlePool.Spawn();
    // var obj2 = singlePool.Spawn();
    // Log.Info(obj1.asset_name); //A
    // Log.Info(obj2.asset_name); //B
    // singlePool.Unspawn(obj2);
    // singlePool.ReleaseObject(obj1); //没有obj1的操作权
    // singlePool.ReleaseObject(obj2);
    
    //多次获取的对象池
    Log.Info("--------------------------------------------------------------");
    var multiPool = GameModule.Instance.ObjectPool.CreateMultiSpawnObjectPool<MyAssetItemObject>();
    GameObject mgo1 = new GameObject("mgo1");
    GameObject mgo2 = new GameObject("mgo2");
    multiPool.Register(MyAssetItemObject.Create("", mgo1, "A"), false);
    multiPool.Register(MyAssetItemObject.Create("", mgo2, "B"), false);
    var mObj1 = multiPool.Spawn();
    var mObj2 = multiPool.Spawn();
    Log.Info(mObj1.asset_name); //A
    Log.Info(mObj2.asset_name); //A
  }
  
  private class MyAssetItemObject : ObjectBase
  {
    public string asset_name;

    protected override void OnSpawn()
    {
      Log.Info($"{asset_name} 被获取了！");
      (Target as GameObject)?.SetActive(true);
    }

    protected override void OnUnspawn()
    {
      Log.Info($"{asset_name} 被回收了");
      (Target as GameObject)?.SetActive(false);
    }

    public static MyAssetItemObject Create(string location, UnityEngine.Object target, string asset_name)
    {
      MyAssetItemObject item = MemoryPool.Instance.Acquire<MyAssetItemObject>();
      item.asset_name = asset_name;
      item.Initialize(location, target);
      return item;
    }
    
    protected override void OnRelease(bool isShutdown)
    {
      if (Target == null)
      {
        return;
      }
      Destroy(Target as GameObject);
      Log.Info($"释放对象: {asset_name}");
    }
  }
  #endregion

  #region 框架初始化和销毁
  private IEnumerator ModuleRestartTest()
  {
    yield return new WaitForSeconds(1);
    GameModule.Instance.Shutdown(ShutdownType.None);
    yield return new WaitForSeconds(1);
    SceneManager.LoadScene(0);
  }
  #endregion
}