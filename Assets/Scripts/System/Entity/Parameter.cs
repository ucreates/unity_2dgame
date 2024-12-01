//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Collections.Generic;

namespace Core.Entity
{
    public sealed class Parameter
    {
        public Parameter()
        {
            parameterList = new Dictionary<string, BaseValue>();
        }

        public Dictionary<string, BaseValue> parameterList { get; }

        public T Get<T>(string parameterName)
        {
            if (parameterList.ContainsKey(parameterName))
            {
                var ret = parameterList[parameterName] as Value<T>;
                return ret.value;
            }

            return default;
        }

        public bool Set<T>(string parameterName, T notifyValue)
        {
            if (false == parameterList.ContainsKey(parameterName))
            {
                var value = new Value<T>();
                value.value = notifyValue;
                parameterList.Add(parameterName, value);
                return true;
            }

            return false;
        }
    }
}