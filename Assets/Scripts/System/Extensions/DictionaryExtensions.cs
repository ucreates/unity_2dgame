using System.Collections.Generic;
using Core.Extensions;
using UnityEngine;

namespace System.Extensions
{
    public static class DictionaryExtensions
    {
        public static void Dump(this Dictionary<string, object> dictionary)
        {
            dictionary.ForEach(pair => { Debug.Log($"{pair.Key}/{pair.Value}"); });
        }
    }
}