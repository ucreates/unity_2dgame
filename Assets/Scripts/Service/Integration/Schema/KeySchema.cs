//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System;
using System.Collections.Generic;

namespace Service.Integration.Schema
{
    [Serializable]
    public sealed class KeySchema
    {
        public KeySchema()
        {
            keyHolder = new Dictionary<string, string>();
            keyCache = string.Empty;
        }

        public string keyCache { get; set; }

        public Dictionary<string, string> keyHolder { get; set; }

        public string Get()
        {
            if (false == string.IsNullOrEmpty(keyCache)) return keyCache;
            foreach (var fieldName in keyHolder.Keys) keyCache += keyHolder[fieldName];
            return keyCache;
        }

        public string Get(string fieldName)
        {
            if (keyHolder.ContainsKey(fieldName)) return keyHolder[fieldName];
            return string.Empty;
        }

        public int GetId()
        {
            var id = Get("id");
            if (string.Empty == id) return -1;
            return Convert.ToInt32(id);
        }

        public bool Set(string fieldName, object fieldValue)
        {
            var ret = false;
            if (false == keyHolder.ContainsKey(fieldName))
            {
                keyHolder.Add(fieldName, fieldValue.ToString());
                ret = true;
            }

            return ret;
        }

        public override bool Equals(object obj)
        {
            if (GetType() != obj.GetType()) return false;
            var right = obj as KeySchema;
            var matchCount = 0;
            foreach (var leftElement in keyHolder)
            foreach (var rightElement in right.keyHolder)
                if (leftElement.Key == rightElement.Key && leftElement.Value == rightElement.Value)
                {
                    matchCount++;
                    break;
                }

            if (matchCount != keyHolder.Count) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(KeySchema left, KeySchema right)
        {
            var matchCount = 0;
            foreach (var leftElement in left.keyHolder)
            foreach (var rightElement in right.keyHolder)
                if (leftElement.Key == rightElement.Key && leftElement.Value == rightElement.Value)
                {
                    matchCount++;
                    break;
                }

            if (matchCount != left.keyHolder.Count) return false;
            return true;
        }

        public static bool operator !=(KeySchema left, KeySchema right)
        {
            var matchCount = 0;
            foreach (var leftElement in left.keyHolder)
            foreach (var rightElement in right.keyHolder)
                if (leftElement.Key == rightElement.Key && leftElement.Value == rightElement.Value)
                {
                    matchCount++;
                    break;
                }

            if (matchCount != left.keyHolder.Count) return true;
            return false;
        }
    }
}