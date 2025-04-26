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
        public override Response Get(in object parameter = null)
        {
            var response = new Response();
            var ubl = new UserBizLogic();
            var userMaster = ubl.GetPlayer();
            var sbl = new ScoreBizLogic();
            var clearCount = sbl?.GetClearCount(userMaster.id) ?? 0;
            var cbl = new CorporateBizLogic();
            var copyright = cbl?.GetCopyright();
            response.data = (clearCount, userMaster.nickName, copyright);
            response.resultStatus = Response.ServiceStatus.SUCCESS;
            return response;
        }

        public override Response Clear()
        {
            var response = new Response();
            var sbl = new ScoreBizLogic();
            sbl?.Clear();
            response.resultStatus = Response.ServiceStatus.SUCCESS;
            return response;
        }
    }
}