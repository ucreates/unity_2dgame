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
using System.Linq;
using Core.Extensions;

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
            if (!keyCache.IsNullOrEmpty()) return keyCache;
            keyHolder.ForEach(pair => { keyCache += pair.Value; });
            return keyCache;
        }

        public string Get(string fieldName)
        {
            return keyHolder.FirstOrDefault(pair => pair.Key.Equals(fieldName)).Value;
        }

        public int GetId()
        {
            var id = Get("id");
            if (string.Empty == id) return -1;
            return id.ToInt32();
        }

        public bool Set(string fieldName, object fieldValue)
        {
            var ret = false;
            if (!keyHolder.ContainsKey(fieldName))
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

            keyHolder.ForEach(leftPair =>
            {
                right.keyHolder.ForEach(rightPair =>
                {
                    if (leftPair.Key == rightPair.Key && leftPair.Value == rightPair.Value)
                    {
                        matchCount++;
                        return false;
                    }

                    return true;
                });
            });

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
            left.keyHolder.ForEach(leftPair =>
            {
                right.keyHolder.ForEach(rightPair =>
                {
                    if (leftPair.Key == rightPair.Key && leftPair.Value == rightPair.Value)
                    {
                        matchCount++;
                        return false;
                    }

                    return true;
                });
            });

            if (matchCount != left.keyHolder.Count) return false;
            return true;
        }

        public static bool operator !=(KeySchema left, KeySchema right)
        {
            var matchCount = 0;
            left.keyHolder.ForEach(leftPair =>
            {
                right.keyHolder.ForEach(rightPair =>
                {
                    if (leftPair.Key == rightPair.Key && leftPair.Value == rightPair.Value)
                    {
                        matchCount++;
                        return false;
                    }

                    return true;
                });
            });

            if (matchCount != left.keyHolder.Count) return true;
            return false;
        }
    }
}