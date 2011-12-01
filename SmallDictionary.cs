﻿using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jtc.CsQuery
{
    /// <summary>
    /// A lightweight dictionary for small lists. 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SmallDictionary<TKey,TValue>: IDictionary<TKey,TValue>
    {
        // Note - it may be faster to not use lazy creation in our context. The users of this dictionary should
        // already use lazy creation for the object itself, so this would just be another redundant check.
        // TODO set up some good test/comparison cases

        //private List<KeyValuePair<TKey,TValue>> InnerList {
        //    get {
        //        if (_InnerList==null) {
        //            _InnerList = new List<KeyValuePair<TKey,TValue>>();
        //        }
        //        return _InnerList;
        //    }
        //}
        //protected List<KeyValuePair<TKey,TValue>> _InnerList;
        protected List<KeyValuePair<TKey, TValue>> InnerList = new List<KeyValuePair<TKey, TValue>>();

        public bool ContainsKey(TKey key)
        {
 	        if (InnerList.Count==0) {
                return false;
            } else {
                for (int i=0;i<InnerList.Count;i++) {
                    if (InnerList[i].Key.Equals(key)) {
                        return true;
                    }
                }
            }
            return false;
        }

        public ICollection<TKey> Keys
        {
	        get {
                List<TKey> keys = new List<TKey>();
                for (int i = 0; i < InnerList.Count; i++)
                {
                    keys.Add(InnerList[i].Key);
                }
                return keys;
            }
        }

        public bool Remove(TKey key)
        {
            for (int i = 0; i < InnerList.Count; i++)
            {
                if (InnerList[i].Key.Equals(key))
                {
                    InnerList.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (InnerList.Count > 0)
            {
                for (int i = 0; i < InnerList.Count; i++)
                {
                    var item = InnerList[i];
                    if (item.Key.Equals(key))
                    {
                        value = item.Value;
                        return true;
                    }
                }
            }
            value = default(TValue);
            return false;
        }

        public ICollection<TValue> Values
        {
	        get {
                List<TValue> values = new List<TValue>();
                for (int i = 0; i < InnerList.Count; i++)
                {
                    values.Add(InnerList[i].Value);
                }
                return values;
            }
        }

        public TValue this[TKey key]
        {
	        get 
	        { 
		        TValue value;
                if (TryGetValue(key,out value)) {
                    return value;
                } else {
                    throw new Exception("The value was not found.");
                }
	        }
	        set 
	        {
                if (InnerList.Count > 0)
                {
                    KeyValuePair<TKey,TValue> newVal = new KeyValuePair<TKey,TValue>(key,value);
                    for (int i = 0; i < InnerList.Count; i++)
                    {
                        var item = InnerList[i];
                        if (item.Key.Equals(key))
                        {
                            if (item.Value.Equals(value))
                            {
                                return;
                            }
                            else
                            {
                                InnerList.RemoveAt(i);
                            }
                        }
                    }
                }
                InnerList.Add(new KeyValuePair<TKey,TValue>(key,value));
            }
        }

        public void Add(KeyValuePair<TKey,TValue> item)
        {
            this[item.Key] = item.Value;
        }
        public void Add(TKey key, TValue value)
        {
            this[key] = value;
        }
        public void  Clear()
        {
            InnerList.Clear();
        }

        public bool Contains(KeyValuePair<TKey,TValue> item)
        {
            if (InnerList.Count==0) {
                return false;
            } else {
                for (int i=0;i<InnerList.Count;i++) {
                    if (InnerList[i].Equals(item)) {
                        return true;
                    }
                }
                return false;
            }
        }

        public void  CopyTo(KeyValuePair<TKey,TValue>[] array, int arrayIndex)
        {
            int index=0;
            foreach (var kvp in InnerList)
            {
                array[arrayIndex+index++] = kvp;

            }
        }

        public int  Count
        {
            get { return InnerList.Count;  }
        }

        public bool  IsReadOnly
        {
	        get { return false; }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return InnerList.Remove(item);
        }
        public IEnumerator<KeyValuePair<TKey,TValue>> GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
