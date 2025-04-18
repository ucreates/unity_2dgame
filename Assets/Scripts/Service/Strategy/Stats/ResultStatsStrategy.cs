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

namespace Service.Strategy
{
    public sealed class ResultStatsStrategy : BaseStrategy
    {
        public override Response Get(in object parameter = null)
        {
            var sret = new Response();
            var ubl = new UserBizLogic();
            var mut = ubl?.GetPlayer();
            var sbl = new ScoreBizLogic();
            var clearCount = sbl?.GetClearCount(mut.id) ?? 0;
            var psbl = new SummaryBizLogic();
            psbl?.UpdateBestClearCount(clearCount);
            var bestClearCount = psbl?.GetBestClearCount() ?? 0;
            sret.data = (clearCount, bestClearCount);
            sret.resultStatus = Response.ServiceStatus.SUCCESS;
            return sret;
        }
    }
}