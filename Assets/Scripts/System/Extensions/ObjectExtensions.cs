using System;

namespace Core.Extensions
{
    public static class ObjectExtensions
    {
        public static int ToInt32(this object source)
        {
            return Convert.ToInt32(source);
        }
    }
}