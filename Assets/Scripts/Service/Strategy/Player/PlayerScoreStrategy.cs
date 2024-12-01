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
    public sealed class PlayerScoreStrategy : BaseStrategy
    {
        public override Response Update(Parameter parameter)
        {
            var clearCount = parameter.Get<int>("clearcount");
            var ubl = new UserBizLogic();
            var mut = ubl.GetPlayer();
            var sbl = new ScoreBizLogic();
            sbl.AddClearCount(mut.id, clearCount);
            var ret = new Response();
            ret.resultStatus = Response.ServiceStatus.SUCCESS;
            return ret;
        }
    }
}