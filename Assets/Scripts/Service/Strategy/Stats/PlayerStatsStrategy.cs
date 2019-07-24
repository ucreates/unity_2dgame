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
    public sealed class PlayerStatsStrategy : BaseStrategy {
    public override Response Get(Parameter parameter = null) {
        Response ret = new Response();
        UserBizLogic ubl = new UserBizLogic();
        MUserTable mut = ubl.GetPlayer();
        ScoreBizLogic sbl = new ScoreBizLogic();
        int clearCount = sbl.GetClearCount(mut.id);
        CorporateBizLogic cbl = new CorporateBizLogic();
        string copyright = cbl.GetCopyright();
        ret.Set<int>("clearcount", clearCount);
        ret.Set<string>("nickname", mut.nickName);
        ret.Set<string>("copyright", copyright);
        ret.resultStatus = Response.ServiceStatus.SUCCESS;
        return ret;
    }
    public override Response Clear() {
        Response sret = new Response();
        ScoreBizLogic sbl = new ScoreBizLogic();
        sbl.Clear();
        sret.resultStatus = Response.ServiceStatus.SUCCESS;
        return sret;
    }
}
}
