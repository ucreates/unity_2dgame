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
using Core.Extensions.Array;
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
        }

        public BaseTable previous { get; set; }

        public BaseTable next { get; set; }

        public KeySchema primaryKey { get; set; }

        [PrimaryKeyAttribute] public int id { get; set; }

        public void Build()
        {
            var propertyInfoList = GetType().GetProperties();
            propertyInfoList.ForEach(pinfo =>
            {
                Attribute.GetCustomAttributes(pinfo).ForEach(attribute =>
                {
                    if (attribute.GetType() is PrimaryKeyAttribute)
                    {
                        var fieldValue = pinfo.GetValue(this, null).ToString();
                        primaryKey.Set(pinfo.Name, fieldValue);
                    }
                });
            });
        }

        public virtual BaseTable Clone()
        {
            return MemberwiseClone() as BaseTable;
        }

        public void Dump()
        {
            var propertyInfoList = GetType().GetProperties();
            propertyInfoList.ForEach(pinfo =>
            {
                if (pinfo.GetType() is PrimaryKeyAttribute)
                {
                    var propertyType = pinfo.GetType().Name;
                    var propertyValue = pinfo.GetValue(this, null);
                    Debug.Log($"{pinfo.Name}:{propertyValue} <{propertyType}");
                }
            });
        }
    }
}