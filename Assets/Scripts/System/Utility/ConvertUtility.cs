//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Reflection;
using Core.Extensions;

namespace Core.Utility
{
    public sealed class ConvertUtility
    {
        public static T ToGenerics<T>(object value)
        {
            if (null == value || value.ToString().IsNullOrEmpty()) return default;
            var type = typeof(T);
            if (type.Name.ToLower().Equals("string")) return (T)value;
            var flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod;
            var Parse = type.GetMethod("Parse", flags, null, new[] { typeof(string) }, null);
            return (T)Parse.Invoke(type, new object[] { value.ToString() });
        }
    }
}