//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using UnityEngine;
using System;
using System.Collections;
namespace Core.Utility {
public sealed class ConvertUtility {
    public static T ToGenerics<T>(object value) {
        if (null == value || string.IsNullOrEmpty(value.ToString())) {
            return default(T);
        }
        Type type = typeof(T);
        if (type.Name.ToLower().Equals("string")) {
            return (T)value;
        }
        System.Reflection.BindingFlags flags = (System.Reflection.BindingFlags) System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.InvokeMethod;
        System.Reflection.MethodInfo Parse = type.GetMethod("Parse", flags, null, new Type[] {typeof(string)}, null);
        return (T)Parse.Invoke(type, new object[] {value.ToString()});
    }
}
}
