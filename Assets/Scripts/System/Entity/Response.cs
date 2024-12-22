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
using System.Linq;

namespace Core.Entity
{
    public sealed class Response
    {
        public enum ServiceStatus
        {
            SUCCESS = 0,
            FAILED = 0
        }

        public Response()
        {
            responseDictionary = new Dictionary<string, BaseValue>();
        }

        public Dictionary<string, BaseValue> responseDictionary { get; }

        public string errorMessage { get; set; }

        public ServiceStatus resultStatus { get; set; }

        public T Get<T>(string name)
        {
            if (responseDictionary.ContainsKey(name)) return (responseDictionary.FirstOrDefault(pair => pair.Key.Equals(name)).Value as Value<T>).value ?? default(T);

            return default;
        }

        public bool Set<T>(string name, T responseValue)
        {
            if (!responseDictionary.ContainsKey(name))
            {
                var value = new Value<T>();
                value.value = responseValue;
                responseDictionary.Add(name, value);
                return true;
            }

            return false;
        }
    }
}