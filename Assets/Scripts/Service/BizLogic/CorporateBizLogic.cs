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
using Core.Entity;
using Service.Integration;
using Service.Integration.Schema;
using Service.Integration.Table;
using Service.Integration.Dto.Assembler;
namespace Service.BizLogic {
public sealed class CorporateBizLogic : BaseBizLogic {
    public CorporateBizLogic() {
        DataBase db = DataBase.GetInstance();
        Dao<MCorporateTable> dao = db.FindBy<MCorporateTable>();
        if (0 == dao.recordList.Count) {
            BaseAssembler<MCorporateTable> assembler = new CorporateAssembler();
            List<MCorporateTable> ret = assembler.WriteToTableList();
            MCorporateTable master = ret[0];
            dao.Save(master);
        }
    }
    public MCorporateTable GetCoporateInfo() {
        DataBase db = DataBase.GetInstance();
        Dao<MCorporateTable> dao = db.FindBy<MCorporateTable>();
        MCorporateTable master = dao.FindBy(BaseBizLogic.UNIQUE_RECORD_ID);
        return master;
    }
    public string GetCompanyName() {
        return this.GetCoporateInfo().campanyName;
    }
    public string GetCopyright() {
        return this.GetCoporateInfo().copyright;
    }
}
}
