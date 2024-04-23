/****************************************************
  文件：IModuleLogicSystem.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月22日 22:18:22
  功能：
*****************************************************/

namespace ZeroFramework
{
    public interface IModuleLogicSystem
    {
       /// <summary>
       /// 所有游戏框架模块轮询。
       /// </summary>
       /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
       /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
      void Update(float elapseSeconds, float realElapseSeconds);

       /// <summary>
       /// 获取游戏框架模块。
       /// </summary>
       /// <typeparam name="T">要获取的游戏框架模块类型。</typeparam>
       /// <returns>要获取的游戏框架模块。</returns>
       /// <remarks>如果要获取的游戏框架模块不存在，则自动创建该游戏框架模块。</remarks>
      T GetModule<T>() where T : class;
    }
}