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
    public sealed class PlayerStatsStrategy : BaseStrategy
    {
        public override Response Get(Parameter parameter = null)
        {
            var ret = new Response();
            var ubl = new UserBizLogic();
            var mut = ubl.GetPlayer();
            var sbl = new ScoreBizLogic();
            var clearCount = sbl.GetClearCount(mut.id);
            var cbl = new CorporateBizLogic();
            var copyright = cbl.GetCopyright();
            ret.Set("clearcount", clearCount);
            ret.Set("nickname", mut.nickName);
            ret.Set("copyright", copyright);
            ret.resultStatus = Response.ServiceStatus.SUCCESS;
            return ret;
        }

        public override Response Clear()
        {
            var sret = new Response();
            var sbl = new ScoreBizLogic();
            sbl.Clear();
            sret.resultStatus = Response.ServiceStatus.SUCCESS;
            return sret;
        }
    }
}