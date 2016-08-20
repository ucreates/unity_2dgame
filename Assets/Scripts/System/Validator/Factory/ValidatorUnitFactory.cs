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
using System.Xml;
using System.Collections.Generic;
using UnityEngine;
using Core.Validator.Unit;
using Core.Validator.Mapper;
namespace Core.Validator.Factory {
public sealed class ValidatorUnitFactory {
    public static ValidatorUnitEntity FactoryMethod(XmlNodeList ruleXmlNodeList) {
        ValidatorUnitEntity vfr = new ValidatorUnitEntity();
        foreach (XmlNode ruleNode in ruleXmlNodeList) {
            foreach (XmlAttribute ruleAttr in ruleNode.Attributes) {
                if (ruleAttr.Name.ToLower().Equals("type")) {
                    string attrValue = ruleAttr.Value.ToLower();
                    BaseRuleMapper ruleMapper =  RuleMapperFactory.FactoryMethod(attrValue);
                    vfr.validatorUnitList[attrValue] = ruleMapper.Map(ruleNode.ChildNodes);
                }
            }
        }
        return vfr;
    }
}
}
