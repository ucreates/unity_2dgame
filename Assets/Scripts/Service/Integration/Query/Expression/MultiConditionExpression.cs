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
using System.Collections;
using System.Collections.Generic;
namespace Service.Integration.Query.Expression {
public class MultiConditionExpression : BaseExpression {
    public List<ConditionExpression> conditionList {
        get;
        set;
    }
    public MultiConditionExpression() {
        this.conditionList = new List<ConditionExpression>();
    }
}
}
