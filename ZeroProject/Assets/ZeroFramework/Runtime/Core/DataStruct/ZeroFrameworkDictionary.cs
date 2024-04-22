/****************************************************
  文件：ZeroFrameworkDictionary.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月21日 10:00:40
  功能：
*****************************************************/

using System.Collections.Generic;

namespace ZeroFramework.Runtime
{
    /// <summary>
    /// 游戏框架字典类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ZeroFrameworkDictionary<TKey, TValue>
    {
        protected readonly List<TKey> KeyList = new List<TKey>();
        protected readonly Dictionary<TKey, TValue> Dictionary = new Dictionary<TKey, TValue>();

        /// <summary>
        /// 存储键的列表
        /// </summary>
        public List<TKey> Keys => KeyList;

        /// <summary>
        /// 存储字典实例
        /// </summary>
        public int Count => KeyList.Count;

        /// <summary>
        /// 通过KEY的数组下标获取元素。
        /// </summary>
        /// <param name="index">下标。</param>
        /// <returns>TValue。</returns>
        public TValue GetValueByIndex(int index)
        {
            return Dictionary[KeyList[index]];
        }
        
        /// <summary>
        /// 通过KEY的数组下标设置元素。
        /// </summary>
        /// <param name="index">下标。</param>
        /// <param name="item">TValue。</param>
        public void SetValue(int index, TValue item)
        {
            Dictionary[KeyList[index]] = item;
        }
        
        /// <summary>
        /// 字典索引器。
        /// </summary>
        /// <param name="key">TKey。</param>
        public TValue this[TKey key]
        {
            get => Dictionary[key];
            set
            {
                if (!ContainsKey(key))
                {
                    Add(key, value);
                }
                else
                {
                    Dictionary[key] = value;
                }
            }
        }
        
        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            KeyList.Clear();
            Dictionary.Clear();
        }
        
        /// <summary>
        /// 新增Key,Value（不会去重判断，避免key重复，推荐使用[]新建和赋值）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        public virtual void Add(TKey key, TValue item)
        {
            KeyList.Add(key);
            Dictionary.Add(key, item);
        }
        
        /// <summary>
        /// 尝试获取元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return Dictionary.TryGetValue(key, out value);
        }
        
        /// <summary>
        /// 是否包含Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return Dictionary.ContainsKey(key);
        }
        
        /// <summary>
        /// 根据下标获取key
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TKey GetKey(int index)
        {
            return KeyList[index];
        }
        
        /// <summary>
        /// 从KeyList和Dictionary移除Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            return KeyList.Remove(key) && Dictionary.Remove(key);
        }
    }

    /// <summary>
    /// 游戏框架顺序字典类。
    /// </summary>
    /// <typeparam name="TKey">指定字典Key的元素类型</typeparam>
    /// <typeparam name="TValue">指定字典Value的元素类型</typeparam>
    public class GameFrameworkSortedDictionary<TKey, TValue> : ZeroFrameworkDictionary<TKey, TValue>
    {
        public override void Add(TKey key, TValue item)
        {
            base.Add(key, item);
            KeyList.Sort();
        }
    }
    
}