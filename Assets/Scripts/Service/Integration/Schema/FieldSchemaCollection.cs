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
using System.Collections.Generic;
namespace Service.Integration.Schema 
{
[Serializable]
public sealed class FieldSchemaCollection {
    public Dictionary<string, BaseFieldSchema> fieldSchemaDictionary {
        get;
        set;
    }
    public FieldSchemaCollection() {
        this.fieldSchemaDictionary = new Dictionary<string, BaseFieldSchema>();
    }
    public bool Set(string fieldSchemaName, BaseFieldSchema field) {
        if (false != this.fieldSchemaDictionary.ContainsKey(fieldSchemaName)) {
            return false;
        }
        this.fieldSchemaDictionary.Add(fieldSchemaName, field);
        return true;
    }
    public BaseFieldSchema Get(string fieldSchemaName) {
        if (false == this.fieldSchemaDictionary.ContainsKey(fieldSchemaName)) {
            return null;
        }
        return this.fieldSchemaDictionary[fieldSchemaName];
    }
}
}
