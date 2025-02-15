using System;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static int ToInt32(this string source)
        {
            return Convert.ToInt32(source);
        }
    }
}