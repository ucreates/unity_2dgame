//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Service.Integration;
using Service.Integration.Table;

namespace Service.BizLogic
{
    public sealed class SummaryBizLogic : BaseBizLogic
    {
        public SummaryBizLogic()
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<TSummaryTable>();
            dao.Save();
        }

        public int GetBestClearCount()
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<TSummaryTable>();
            var tt = dao.FindBy(UNIQUE_RECORD_ID);
            return tt.record.bestClearCount;
        }

        public bool UpdateBestClearCount(int clearCount)
        {
            var bestClearCount = GetBestClearCount();
            if (bestClearCount < clearCount)
            {
                var db = DataBase.GetInstance();
                var dao = db.FindBy<TSummaryTable>();
                var tt = dao.FindBy(UNIQUE_RECORD_ID);
                tt.record.bestClearCount = clearCount;
                dao.Save(tt.record);
                return true;
            }

            return false;
        }

        public bool Clear()
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<TScoreTable>();
            var tt = dao.FindBy(UNIQUE_RECORD_ID);
            tt.record.clearCount = 0;
            return dao.Update(tt.record);
        }
    }
}