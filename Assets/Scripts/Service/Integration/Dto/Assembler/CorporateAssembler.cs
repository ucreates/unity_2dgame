﻿//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Core.Entity;
using Service;
using Service.Integration;
using Service.Integration.Table;
namespace Service.Integration.Dto.Assembler {
public sealed class CorporateAssembler : XmlAssembler<MCorporateTable> {
    public CorporateAssembler() : base("Config/corporate") {}
    public override List<MCorporateTable> WriteToTableList() {
        List<MCorporateTable> ret = new List<MCorporateTable>();
        IEnumerable<XElement> elementList = this.GetElementList();
        MCorporateTable record = new MCorporateTable();
        foreach (XElement element in elementList) {
            if (element.Attribute("type").Value.ToLower().Equals("name")) {
                record.campanyName = element.Value;
            } else if (element.Attribute("type").Value.ToLower().Equals("business")) {
                record.buisiness = element.Value;
            } else if (element.Attribute("type").Value.ToLower().Equals("copyright")) {
                record.copyright = element.Value;
            }
        }
        ret.Add(record);
        return ret;
    }
}
}