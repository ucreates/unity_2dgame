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
using System.Collections.Generic;
using Core.Validator;
using Core.Validator.Mapper;
namespace Core.Validator.Factory {
public sealed class RuleMapperFactory {
    public static BaseRuleMapper FactoryMethod(string type) {
        BaseRuleMapper ruleMapper = null;
        switch (type) {
        case "required":
            ruleMapper = new RequiredRuleMapper();
            break;
        case "range":
            ruleMapper = new RangeRuleMapper();
            break;
        case "compare":
            ruleMapper = new CompareRuleMapper();
            break;
        case "regex":
            ruleMapper = new RegexRuleMapper();
            break;
        case "mailandphone":
            ruleMapper = new MailAndPhoneRuleMapper();
            break;
        default:
            break;
        }
        return ruleMapper;
    }
}
}
