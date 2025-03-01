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
        public override Response Get(object parameter = null)
        {
            var ret = new Response();
            var ubl = new UserBizLogic();
            var mut = ubl.GetPlayer();
            var sbl = new ScoreBizLogic();
            var clearCount = sbl?.GetClearCount(mut.id) ?? 0;
            var cbl = new CorporateBizLogic();
            var copyright = cbl?.GetCopyright();
            ret.Set<int>("clearcount", clearCount);
            ret.Set<string>("nickname", mut.nickName);
            ret.Set<string>("copyright", copyright);
            ret.resultStatus = Response.ServiceStatus.SUCCESS;
            return ret;
        }

        public override Response Clear()
        {
            var sret = new Response();
            var sbl = new ScoreBizLogic();
            sbl?.Clear();
            sret.resultStatus = Response.ServiceStatus.SUCCESS;
            return sret;
        }
    }
}