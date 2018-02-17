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
using Service.Integration.Schema;
namespace Service.Integration.Table {
public sealed class TScoreTable : BaseTable {
    [PrimaryKey]
    public int userId {
        get;
        set;
    }
    public int clearCount {
        get;
        set;
    }
    public TScoreTable() : this(0, 0) {}
    public TScoreTable(int userId, int clearCount) {
        this.userId = userId;
        this.clearCount = clearCount;
    }
    public override BaseTable Clone() {
        return base.MemberwiseClone() as TScoreTable;
    }
}
}
