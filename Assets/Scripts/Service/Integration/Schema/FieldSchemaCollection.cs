//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Integration.Schema
{
    [Serializable]
    public sealed class FieldSchemaCollection
    {
        public FieldSchemaCollection()
        {
            fieldSchemaDictionary = new Dictionary<string, BaseFieldSchema>();
        }

        public Dictionary<string, BaseFieldSchema> fieldSchemaDictionary { get; set; }

        public bool Set(string fieldSchemaName, BaseFieldSchema field)
        {
            if (fieldSchemaDictionary.ContainsKey(fieldSchemaName)) return false;
            fieldSchemaDictionary.Add(fieldSchemaName, field);
            return true;
        }

        public BaseFieldSchema Get(string fieldSchemaName)
        {
            return fieldSchemaDictionary.FirstOrDefault(pair => pair.Key.Equals(fieldSchemaName)).Value;
        }
    }
}