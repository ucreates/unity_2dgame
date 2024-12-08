using System;
using System.Collections.Generic;

namespace Core.Extensions
{
    public static class ICollectionExtensions
    {
        public static void ForEach<T>(this ICollection<T> collection, Func<T, bool> callback)
        {
            foreach (var item in collection)
                if (!callback?.Invoke(item) ?? false)
                    break;
        }
    }
}