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
using System.Xml.Linq;
using Service.Integration.Table;
using UnityEngine;

namespace Service.Integration.Dto.Assembler
{
    public class XmlAssembler<T> : BaseAssembler<T> where T : BaseTable, new()
    {
        public XmlAssembler(string xmlPath)
        {
            this.xmlPath = xmlPath;
        }

        public string xmlPath { get; set; }

        protected XElement GetElement()
        {
            var text = Resources.Load(xmlPath);
            if (null == text) return null;
            var xml = Object.Instantiate(text) as TextAsset;
            var element = XElement.Parse(xml.text);
            return element;
        }

        protected IEnumerable<XElement> GetElementList()
        {
            var text = Resources.Load(xmlPath);
            if (null == text) return null;
            var xml = Object.Instantiate(text) as TextAsset;
            var elementList = XElement.Parse(xml.text);
            var elementChildList =
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