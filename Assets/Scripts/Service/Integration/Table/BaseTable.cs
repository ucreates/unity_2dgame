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
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Service.Integration.Schema;
namespace Service.Integration.Table {
[Serializable]
public abstract class BaseTable {
    public KeySchema primaryKey {
        get;
        set;
    }
    public FieldSchemaCollection fieldSchemaCollection {
        get;
        set;
    }
    [PrimaryKeyAttribute]
    public int id {
        get;
        set;
    }
    public BaseTable() {
        this.id = 0;
        this.primaryKey = new KeySchema();
        this.fieldSchemaCollection = new FieldSchemaCollection();
    }
    public void Build() {
        PropertyInfo[] PropertyInfoList = this.GetType().GetProperties();
        foreach (PropertyInfo pinfo in PropertyInfoList) {
            foreach (Attribute attribute in Attribute.GetCustomAttributes(pinfo)) {
                if (attribute.GetType() == typeof(PrimaryKeyAttribute)) {
                    string fieldValue = pinfo.GetValue(this, null).ToString();
                    this.primaryKey.Set(pinfo.Name, fieldValue);
                }
            }
            string propertyType = pinfo.PropertyType.Name.ToLower();
            object propertyValue = pinfo.GetValue(this, null);
            if (false != propertyType.Equals("int32")) {
                this.fieldSchemaCollection.Set(pinfo.Name, new FieldSchema<Int32>(Convert.ToInt32(propertyValue)));
            } else if (false != propertyType.Equals("long")) {
                this.fieldSchemaCollection.Set(pinfo.Name, new FieldSchema<long>(Convert.ToInt64(propertyValue)));
            } else if (false != propertyType.Equals("float")) {
                this.fieldSchemaCollection.Set(pinfo.Name, new FieldSchema<float>(Convert.ToSingle(propertyValue)));
            } else if (false != propertyType.Equals("double")) {
                this.fieldSchemaCollection.Set(pinfo.Name, new FieldSchema<double>(Convert.ToDouble(propertyValue)));
            } else if (false != propertyType.Equals("boolean")) {
                this.fieldSchemaCollection.Set(pinfo.Name, new FieldSchema<bool>(Convert.ToBoolean(propertyValue)));
            } else if (false != propertyType.Equals("string")) {
                this.fieldSchemaCollection.Set(pinfo.Name, new FieldSchema<string>(propertyValue.ToString()));
            }
        }
    }
    public virtual BaseTable Clone() {
        return base.MemberwiseClone() as BaseTable;
    }
    public void Dump() {
        PropertyInfo[] PropertyInfoList = this.GetType().GetProperties();
        foreach (PropertyInfo pinfo in PropertyInfoList) {
            string propertyType = pinfo.GetType().Name;
            object propertyValue = pinfo.GetValue(this, null);
            Debug.Log(pinfo.Name + ":" + propertyValue.ToString() + "<" + propertyType + ">");
        }
        return;
    }
}
}
