/****************************************************
  文件：ZeroFrameworkMultiDictionary.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年04月21日 11:13:01
  功能：
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ZeroFramework.Runtime
{
    /// <summary>
    /// 游戏框架多值字典类。(一个Key对应多个Value，Value相当于对应链表上的一个范围)
    /// </summary>
    /// <typeparam name="TKey">指定多值字典的主键类型。</typeparam>
    /// <typeparam name="TValue">指定多值字典的值类型。</typeparam>
    public sealed class ZeroFrameworkMultiDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, ZeroFrameworkLinkedListRange<TValue>>>, IEnumerable
    {
        private readonly ZeroFrameworkLinkedList<TValue> _linkedList;
        private readonly Dictionary<TKey, ZeroFrameworkLinkedListRange<TValue>> _dictionary;
        
        /// <summary>
        /// 初始化游戏框架多值字典类的新实例。
        /// </summary>
        public ZeroFrameworkMultiDictionary()
        {
            _linkedList = new ZeroFrameworkLinkedList<TValue>();
            _dictionary = new Dictionary<TKey, ZeroFrameworkLinkedListRange<TValue>>();
        }
        
        /// <summary>
        /// 获取多值字典中实际包含的主键数量。
        /// </summary>
        public int Count => _dictionary.Count;
        
        /// <summary>
        /// 获取多值字典中指定主键的范围。
        /// </summary>
        /// <param name="key">指定的主键。</param>
        /// <returns>指定主键的范围。</returns>
        public ZeroFrameworkLinkedListRange<TValue> this[TKey key]
        {
            get
            {
                ZeroFrameworkLinkedListRange<TValue> range = default(ZeroFrameworkLinkedListRange<TValue>);
                _dictionary.TryGetValue(key, out range);
                return range;
            }
        }
        
        /// <summary>
        /// 清理多值字典。
        /// </summary>
        public void Clear()
        {
            _dictionary.Clear();
            _linkedList.Clear();
        }
        
        /// <summary>
        /// 检查多值字典中是否包含指定主键。
        /// </summary>
        /// <param name="key">要检查的主键。</param>
        /// <returns>多值字典中是否包含指定主键。</returns>
        public bool Contains(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }
        
        /// <summary>
        /// 检查多值字典中是否包含指定值。
        /// </summary>
        /// <param name="key">要检查的主键。</param>
        /// <param name="value">要检查的值。</param>
        /// <returns>多值字典中是否包含指定值。</returns>
        public bool Contains(TKey key, TValue value)
        {
            ZeroFrameworkLinkedListRange<TValue> range = default(ZeroFrameworkLinkedListRange<TValue>);
            if (_dictionary.TryGetValue(key, out range))
            {
                return range.Contains(value);
            }

            return false;
        }
        
        /// <summary>
        /// 尝试获取多值字典中指定主键的范围。
        /// </summary>
        /// <param name="key">指定的主键。</param>
        /// <param name="range">指定主键的范围。</param>
        /// <returns>是否获取成功。</returns>
        public bool TryGetValue(TKey key, out ZeroFrameworkLinkedListRange<TValue> range)
        {
            return _dictionary.TryGetValue(key, out range);
        }
        
        /// <summary>
        /// 向指定的主键增加指定的值。(每次新增都需要重新new Range)
        /// </summary>
        /// <param name="key">指定的主键。</param>
        /// <param name="value">指定的值。</param>
        public void Add(TKey key, TValue value)
        {
            ZeroFrameworkLinkedListRange<TValue> range = default(ZeroFrameworkLinkedListRange<TValue>);
            if (_dictionary.TryGetValue(key, out range))
            {
                _linkedList.AddBefore(range.Terminal, value);
            }
            else
            {
                LinkedListNode<TValue> first = _linkedList.AddLast(value); //左闭
                LinkedListNode<TValue> terminal = _linkedList.AddLast(default(TValue)); //右开
                _dictionary.Add(key, new ZeroFrameworkLinkedListRange<TValue>(first, terminal));
            }
        }
        
        /// <summary>
        /// 从指定的主键中移除指定的值。
        /// </summary>
        /// <param name="key">指定的主键。</param>
        /// <param name="value">指定的值。</param>
        /// <returns>是否移除成功。</returns>
        public bool Remove(TKey key, TValue value)
        {
            ZeroFrameworkLinkedListRange<TValue> range = default(ZeroFrameworkLinkedListRange<TValue>);
            if (_dictionary.TryGetValue(key, out range))
            {
                //在key对应的范围内搜索
                for (LinkedListNode<TValue> current = range.First; current != null && current != range.Terminal; current = current.Next)
                {
                    if (current.Value.Equals(value))
                    {
                        if (current == range.First) //如果是range第一个节点
                        {
                            LinkedListNode<TValue> next = current.Next;
                            if (next == range.Terminal) //range只有一个节点，直接删除
                            {
                                _linkedList.Remove(next);
                                _dictionary.Remove(key);
                            }
                            else //否则重新new一个Range
                            {
                                _dictionary[key] = new ZeroFrameworkLinkedListRange<TValue>(next, range.Terminal);
                            }
                        }

                        _linkedList.Remove(current); //从链表移除
                        return true;
                    }
                }
            }
            return false;
        }
        
        /// <summary>
        /// 从指定的主键中移除所有的值。
        /// </summary>
        /// <param name="key">指定的主键。</param>
        /// <returns>是否移除成功。</returns>
        public bool RemoveAll(TKey key)
        {
            ZeroFrameworkLinkedListRange<TValue> range = default(ZeroFrameworkLinkedListRange<TValue>);
            if (_dictionary.TryGetValue(key, out range))
            {
                _dictionary.Remove(key);

                LinkedListNode<TValue> current = range.First;
                while (current != null)
                {
                    LinkedListNode<TValue> next = current != range.Terminal ? current.Next : null;
                    _linkedList.Remove(current);
                    current = next;
                }

                return true;
            }

            return false;
        }
        
        /// <summary>
        /// 返回循环访问集合的枚举数。
        /// </summary>
        /// <returns>循环访问集合的枚举数。</returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(_dictionary);
        }
        
        /// <summary>
        /// 返回循环访问集合的枚举数。（泛型返回值）
        /// </summary>
        /// <returns>循环访问集合的枚举数。</returns>
        IEnumerator<KeyValuePair<TKey, ZeroFrameworkLinkedListRange<TValue>>> IEnumerable<KeyValuePair<TKey, ZeroFrameworkLinkedListRange<TValue>>>.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        /// <summary>
        /// 返回循环访问集合的枚举数。（object返回值）
        /// </summary>
        /// <returns>循环访问集合的枚举数。</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        /// <summary>
        /// 循环访问集合的枚举数。
        /// </summary>
        [StructLayout(LayoutKind.Auto)]
        public struct Enumerator : IEnumerator<KeyValuePair<TKey, ZeroFrameworkLinkedListRange<TValue>>>, IEnumerator
        {
            private Dictionary<TKey, ZeroFrameworkLinkedListRange<TValue>>.Enumerator m_Enumerator;

            internal Enumerator(Dictionary<TKey, ZeroFrameworkLinkedListRange<TValue>> dictionary)
            {
                if (dictionary == null)
                {
                    throw new GameFrameworkException("Dictionary is invalid.");
                }

                m_Enumerator = dictionary.GetEnumerator();
            }

            /// <summary>
            /// 获取当前结点。
            /// </summary>
            public KeyValuePair<TKey, ZeroFrameworkLinkedListRange<TValue>> Current => m_Enumerator.Current;

            /// <summary>
            /// 获取当前的枚举数。
            /// </summary>
            object IEnumerator.Current => m_Enumerator.Current;

            /// <summary>
            /// 清理枚举数。
            /// </summary>
            public void Dispose()
            {
                m_Enumerator.Dispose();
            }

            /// <summary>
            /// 获取下一个结点。
            /// </summary>
            /// <returns>返回下一个结点。</returns>
            public bool MoveNext()
            {
                return m_Enumerator.MoveNext();
            }

            /// <summary>
            /// 重置枚举数。
            /// </summary>
            void IEnumerator.Reset()
            {
                ((IEnumerator<KeyValuePair<TKey, ZeroFrameworkLinkedListRange<TValue>>>)m_Enumerator).Reset();
            }
        }
        
    }
}