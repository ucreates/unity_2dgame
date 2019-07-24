//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using Core.Entity;
using Service.BizLogic;
using Service.Integration.Table;
namespace Service.Strategy
{
    public sealed class ResultStatsStrategy : BaseStrategy {
    public override Response Get(Parameter parameter = null) {
        Response sret = new Response();
        UserBizLogic ubl = new UserBizLogic();
        MUserTable mut = ubl.GetPlayer();
        ScoreBizLogic sbl = new ScoreBizLogic();
        int clearCount = sbl.GetClearCount(mut.id);
        SummaryBizLogic psbl = new SummaryBizLogic();
        psbl.UpdateBestClearCount(clearCount);
        int bestClearCount = psbl.GetBestClearCount();
        sret.Set<int>("clearcount", clearCount);
        sret.Set<int>("bestclearcount", bestClearCount);
        sret.resultStatus = Response.ServiceStatus.SUCCESS;
        return sret;
    }
}
}
