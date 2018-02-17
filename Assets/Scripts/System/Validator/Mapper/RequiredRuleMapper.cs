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
using System.Xml;
using System.Collections.Generic;
using Core.Validator.Message;
using Core.Validator.Builder;
namespace Core.Validator.Mapper {
public sealed class RequiredRuleMapper : BaseRuleMapper {
    public override Dictionary<string, object> Map(XmlNodeList ruleNodeList) {
        Dictionary<string, object> ret = new Dictionary<string, object>();
        RequiredValidatorUnitBuilder builder = new RequiredValidatorUnitBuilder();
        foreach (XmlNode ruleNode in ruleNodeList) {
            foreach (XmlAttribute ruleAttr in ruleNode.Attributes) {
                if (ruleAttr.Value.ToLower().Equals("type")) {
                    string type = ruleNode.InnerText.ToLower();
                    builder.AddType(type);
                } else if (ruleAttr.Value.ToLower().Equals("summary")) {
                    string summary = ruleNode.InnerText.ToLower();
                    builder.AddMessage(new ErrorValidateMessage(summary));
                }
            }
        }
        ret.Add(builder.type, builder.Build());
        return ret;
    }
}
}
