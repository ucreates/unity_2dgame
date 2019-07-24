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
    public sealed class SummaryBizLogic : BaseBizLogic {
    public SummaryBizLogic() {
        DataBase db = DataBase.GetInstance();
        Dao<TSummaryTable> dao = db.FindBy<TSummaryTable>();
        dao.Save();
    }
    public int GetBestClearCount() {
        DataBase db = DataBase.GetInstance();
        Dao<TSummaryTable> dao = db.FindBy<TSummaryTable>();
        TSummaryTable tt = dao.FindBy(BaseBizLogic.UNIQUE_RECORD_ID);
        return tt.bestClearCount;
    }
    public bool UpdateBestClearCount(int clearCount) {
        int bestClearCount = this.GetBestClearCount();
        if (bestClearCount < clearCount) {
            DataBase db = DataBase.GetInstance();
            Dao<TSummaryTable> dao = db.FindBy<TSummaryTable>();
            TSummaryTable tt = dao.FindBy(BaseBizLogic.UNIQUE_RECORD_ID);
            tt.bestClearCount = clearCount;
            dao.Save(tt);
            return true;
        }
        return false;
    }
    public bool Clear() {
        DataBase db = DataBase.GetInstance();
        Dao<TScoreTable> dao = db.FindBy<TScoreTable>();
        TScoreTable tt = dao.FindBy(BaseBizLogic.UNIQUE_RECORD_ID);
        tt.clearCount = 0;
        return dao.Update(tt);
    }
}
}
