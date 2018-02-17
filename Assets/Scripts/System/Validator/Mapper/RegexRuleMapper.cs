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
using Core.Validator;
using Core.Validator.Unit;
using Core.Validator.Message;
using Core.Validator.Builder;
namespace Core.Validator.Mapper {
public sealed class RegexRuleMapper : BaseRuleMapper {
    public override Dictionary<string, object> Map(XmlNodeList ruleNodeList) {
        Dictionary<string, object> ret = new Dictionary<string, object>();
        RegexValidatorUnitBuilder builder = new RegexValidatorUnitBuilder();
        foreach (XmlNode ruleNode in ruleNodeList) {
            foreach (XmlAttribute ruleAttr in ruleNode.Attributes) {
                string attrValue = ruleAttr.Value.ToLower();
                if (attrValue.Equals("pattern")) {
                    string pattern = ruleNode.InnerText;
                    builder.AddPattern(pattern);
                } else if (attrValue.Equals("summary")) {
                    string summary = ruleNode.InnerText;
                    builder.AddMessage(new ErrorValidateMessage(summary));
                }
            }
        }
        ret.Add(builder.type, builder.Build());
        return ret;
    }
}
}
