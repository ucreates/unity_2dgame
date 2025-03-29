using System.Collections.Generic;
using Core.Extensions;

namespace System.Extensions
{
    public static class DictionaryExtensions
    {
        public static void Dump(this Dictionary<string, object> dictionary)
        {
            dictionary.ForEach(pair => { Core.IO.Console.Info(values: $"{pair.Key}/{pair.Value}"); });
        }
    }
}