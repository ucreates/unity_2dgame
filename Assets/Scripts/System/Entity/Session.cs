//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Collections.Generic;
using Core.Utility;

namespace Core.Entity
{
    public sealed class Session
    {
        private Session()
        {
            valueDictionary = new Dictionary<string, object>();
        }

        private static Session instance { get; set; }

        private Dictionary<string, object> valueDictionary { get; }

        public static Session GetInstance()
        {
            if (null == instance) instance = new Session();
            return instance;
        }

        public T Get<T>(string key, bool delete = false)
        {
            if (valueDictionary.ContainsKey(key))
            {
                var type = typeof(T);
                if (type == typeof(string) || type is object)
                {
                    var ret = (T)valueDictionary[key];
                    if (delete) valueDictionary.Remove(key);
                    return ret;
                }
                else
                {
                    var ret = ConvertUtility.ToGenerics<T>(valueDictionary[key]);
                    if (delete) valueDictionary.Remove(key);
                    return ret;
                }
            }

            return default;
        }

        public bool Add(string key, object value)
        {
            if (valueDictionary.ContainsKey(key))
            {
                valueDictionary[key] = value;
                return true;
            }

            valueDictionary.Add(key, value);
            return false;
        }
    }
}