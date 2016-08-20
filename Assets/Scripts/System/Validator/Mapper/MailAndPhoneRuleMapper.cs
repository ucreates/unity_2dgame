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
using Core.Validator;
using Core.Validator.Unit;
using Core.Validator.Builder;
using Core.Validator.Message;
namespace Core.Validator.Mapper {
public sealed class MailAndPhoneRuleMapper : BaseRuleMapper {
    private const string EMAIL_ADRRESS_REGEX = "^[a-z|A-Z|0-9\\.\\-]+@[a-z|A-Z|0-9\\-]+\\.[a-z|A-Z|0-9\\.\\-]+$";
    private const string PHONE_NUMBER_REGEX = "^\\d{2,6}[\\-]?\\d{0,4}[\\-]?\\d{4}$";
    public override Dictionary<string, object> Map(XmlNodeList ruleNodeList) {
        Dictionary<string, object> ret = new Dictionary<string, object>();
        for (int i = 0; i < 2; i++) {
            RegexValidatorUnitBuilder builder = new RegexValidatorUnitBuilder();
            if (i == 0) {
                builder.AddPattern(MailAndPhoneRuleMapper.EMAIL_ADRRESS_REGEX);
            } else {
                builder.AddPattern(MailAndPhoneRuleMapper.PHONE_NUMBER_REGEX);
            }
            foreach (XmlNode ruleNode in ruleNodeList) {
                foreach (XmlAttribute ruleAttr in ruleNode.Attributes) {
                    string attrValue = ruleAttr.Value.ToLower();
                    if (i == 0 && attrValue.Equals("mailerrorsummary")) {
                        string summary = ruleNode.InnerText;
                        builder.AddMessage(new ErrorValidateMessage(summary));
                        break;
                    } else if (i == 1 && attrValue.Equals("phoneerrorsummary")) {
                        string summary = ruleNode.InnerText;
                        builder.AddMessage(new ErrorValidateMessage(summary));
                        break;
                    }
                }
            }
            string builderType = string.Empty;
            if (i == 0) {
                builderType = builder.type + "::mail";
            } else {
                builderType = builder.type + "::phone";
            }
            ret.Add(builderType, builder.Build());
        }
        return ret;
    }
}
}
