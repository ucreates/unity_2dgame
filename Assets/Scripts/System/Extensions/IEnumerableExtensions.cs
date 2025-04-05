using System;
using System.Collections;
using System.Collections.Generic;

namespace Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void For<T>(this IEnumerable<T> enumerable, in Action<int, T> action)
        {
            var i = 0;
            foreach (var item in enumerable)
            {
                action?.Invoke(i, item);
                i++;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, in Action<T> action)
        {
            foreach (var item in enumerable) action?.Invoke(item);
        }

        public static void ForEach<T>(this T enumerable, Action<T> action) where T : IEnumerable
        {
            foreach (T item in enumerable) action?.Invoke(item);
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, in Func<T, bool> callback)
        {
            foreach (var item in enumerable)
                if (!callback?.Invoke(item) ?? false)
                    break;
        }

        public static void ForEach<T>(this T enumerable, in Func<T, bool> callback) where T : IEnumerable
        {
            foreach (T item in enumerable)
                if (!callback?.Invoke(item) ?? false)
                    break;
        }
    }
}