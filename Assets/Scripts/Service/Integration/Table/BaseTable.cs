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
using Service.Integration.Schema;
using UnityEngine;

namespace Service.Integration.Table
{
    [Serializable]
    public abstract class BaseTable
    {
        public BaseTable()
        {
            id = 0;
            primaryKey = new KeySchema();
            fieldSchemaCollection = new FieldSchemaCollection();
        }

        public KeySchema primaryKey { get; set; }

        public FieldSchemaCollection fieldSchemaCollection { get; set; }

        [PrimaryKeyAttribute] public int id { get; set; }

        public void Build()
        {
            var PropertyInfoList = GetType().GetProperties();
            foreach (var pinfo in PropertyInfoList)
            {
                foreach (var attribute in Attribute.GetCustomAttributes(pinfo))
                    if (attribute.GetType() == typeof(PrimaryKeyAttribute))
                    {
                        var fieldValue = pinfo.GetValue(this, null).ToString();
                        primaryKey.Set(pinfo.Name, fieldValue);
                    }

                var propertyType = pinfo.PropertyType.Name.ToLower();
                var propertyValue = pinfo.GetValue(this, null);
                if (propertyType.Equals("int32"))
                    fieldSchemaCollection.Set(pinfo.Name, new FieldSchema<int>(Convert.ToInt32(propertyValue)));
                else if (propertyType.Equals("long"))
                    fieldSchemaCollection.Set(pinfo.Name, new FieldSchema<long>(Convert.ToInt64(propertyValue)));
                else if (propertyType.Equals("float"))
                    fieldSchemaCollection.Set(pinfo.Name, new FieldSchema<float>(Convert.ToSingle(propertyValue)));
                else if (propertyType.Equals("double"))
                    fieldSchemaCollection.Set(pinfo.Name, new FieldSchema<double>(Convert.ToDouble(propertyValue)));
                else if (propertyType.Equals("boolean"))
                    fieldSchemaCollection.Set(pinfo.Name, new FieldSchema<bool>(Convert.ToBoolean(propertyValue)));
                else if (propertyType.Equals("string"))
                    fieldSchemaCollection.Set(pinfo.Name, new FieldSchema<string>(propertyValue.ToString()));
            }
        }

        public virtual BaseTable Clone()
        {
            return MemberwiseClone() as BaseTable;
        }

        public void Dump()
        {
            var PropertyInfoList = GetType().GetProperties();
            foreach (var pinfo in PropertyInfoList)
            {
                var propertyType = pinfo.GetType().Name;
                var propertyValue = pinfo.GetValue(this, null);
                Debug.Log(pinfo.Name + ":" + propertyValue + "<" + propertyType + ">");
            }
        }
    }
}