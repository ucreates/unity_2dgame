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
using Core.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Service.Integration.Schema;
using Service.Integration.Table;
namespace Service.Integration.Dto.Assembler {
public class XmlAssembler<T> : BaseAssembler<T>  where T : BaseTable, new() {
    public string xmlPath {
        get;
        set;
    }
    public XmlAssembler(string xmlPath) {
        this.xmlPath = xmlPath;
    }
    protected XElement GetElement() {
        UnityEngine.Object text = Resources.Load(this.xmlPath) as UnityEngine.Object;
        if (null == text) {
            return null;
        }
        TextAsset xml = UnityEngine.Object.Instantiate(text) as TextAsset;
        XElement element = XElement.Parse(xml.text);
        return element;
    }
    protected IEnumerable<XElement> GetElementList() {
        UnityEngine.Object text = Resources.Load(this.xmlPath) as UnityEngine.Object;
        if (null == text) {
            return null;
        }
        TextAsset xml = UnityEngine.Object.Instantiate(text) as TextAsset;
        XElement elementList = XElement.Parse(xml.text);
        IEnumerable<XElement> elementChildList =
            from
            element
            in
            elementList.Elements()
            select
            element;
        return elementChildList;
    }
}
}
