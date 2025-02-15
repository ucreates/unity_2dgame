using System;

namespace Core.Extensions.Array
{
    public static class ArrayExtensions
    {
        public static void For<T>(this T[] array, Action<T> action)
        {
            for (var i = 0; i < array.Length; i++) action?.Invoke(array[i]);
        }

        public static void For<T>(this T[] array, Action<T, int> action)
        {
            for (var i = 0; i < array.Length; i++) action?.Invoke(array[i], i);
        }

        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            foreach (var item in array) action?.Invoke(item);
        }

        public static void ForEach<T>(this T[] array, Func<T, bool> action)
        {
            foreach (var item in array)
                if (!action?.Invoke(item) ?? false)
                    break;
        }

        public static void Sort<T>(this T[] array, bool desc = false) where T : IComparable<T>
        {
            System.Array.Sort(array);
            if (desc) System.Array.Reverse(array);
        }
    }
}