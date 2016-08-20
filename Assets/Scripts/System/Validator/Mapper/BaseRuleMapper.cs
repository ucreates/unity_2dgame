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
using Core.Validator.Builder;
using Core.Validator.Message;
namespace Core.Validator.Mapper {
public abstract class BaseRuleMapper {
    public virtual Dictionary<string, object> Map(XmlNodeList ruleNodeList) {
        return null;
    }
}
}
