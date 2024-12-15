using System;

namespace Foundation
{
    //C#:3.0
    public static class ArrayExtension
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
            Array.Sort(array);
            if (desc) Array.Reverse(array);
        }
    }
}