//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Linq;
using Service.Integration;
using Service.Integration.Dto.Assembler;
using Service.Integration.Table;

namespace Service.BizLogic
{
    public sealed class CorporateBizLogic : BaseBizLogic
    {
        public CorporateBizLogic()
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<MCorporateTable>();
            if (0 == dao.recordList.Count)
            {
                BaseAssembler<MCorporateTable> assembler = new CorporateAssembler();
                var ret = assembler.WriteToTableList();
                var master = ret.FirstOrDefault();
                dao.Save(master);
            }
        }

        public MCorporateTable GetCoporateInfo()
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<MCorporateTable>();
            var master = dao.FindBy(UNIQUE_RECORD_ID);
            return master;
        }

        public string GetCompanyName()
        {
            return GetCoporateInfo().campanyName;
        }

        public string GetCopyright()
        {
            return GetCoporateInfo().copyright;
        }
    }
}