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
using System.Collections;
using Service.Integration.Schema;
namespace Service.Integration.Query.Expression {
public sealed class ConditionExpression : BaseExpression {
    public string fieldName {
        get;
        set;
    }
    public string comparisonOperator {
        get;
        set;
    }
    public BaseFieldSchema field {
        get;
        set;
    }
    public ConditionExpression() : this(string.Empty, string.Empty, null) {
    }
    public ConditionExpression(string fieldName, string comparisonOperator, BaseFieldSchema field) {
        this.fieldName = fieldName;
        this.comparisonOperator = comparisonOperator;
        this.field = field;
    }
}
}
