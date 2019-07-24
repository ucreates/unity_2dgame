//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
namespace Service.Integration.Table
{
    public sealed class MUserTable : BaseTable {
    public string nickName {
        get;
        set;
    }
    public string password {
        get;
        set;
    }
    public int gender {
        get;
        set;
    }
    public string mailOrPhone {
        get;
        set;
    }
    public bool isPlayer {
        get;
        set;
    }
    public int coin {
        get;
        set;
    }
    public MUserTable() : this(string.Empty, string.Empty, 0, string.Empty, 0, false) {}
    public MUserTable(string nickName, string password, int gender, string mailOrPhone, int coin, bool isPlayer) {
        this.nickName = nickName;
        this.password = password;
        this.gender = gender;
        this.mailOrPhone = mailOrPhone;
        this.coin = coin;
        this.isPlayer = isPlayer;
    }
    public override BaseTable Clone() {
        return base.MemberwiseClone() as MUserTable;
    }
}
}
