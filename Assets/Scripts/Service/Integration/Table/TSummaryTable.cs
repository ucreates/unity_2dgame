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
namespace Service.Integration.Table {
[Serializable]
public sealed class TSummaryTable : BaseTable {
    public int bestClearCount {
        get;
        set;
    }
    public TSummaryTable() {
        this.bestClearCount = 0;
    }
    public override BaseTable Clone() {
        return base.MemberwiseClone() as TSummaryTable;
    }
}
}
