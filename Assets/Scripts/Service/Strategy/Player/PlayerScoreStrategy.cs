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
using Core.Extensions;
using Service.BizLogic;

namespace Service.Strategy
{
    public sealed class PlayerScoreStrategy : BaseStrategy
    {
        public override Response Update(in object parameter)
        {
            var clearCount = parameter.ToInt32();
            var ubl = new UserBizLogic();
            var userMaster = ubl.GetPlayer();
            var sbl = new ScoreBizLogic();
            sbl?.AddClearCount(userMaster.id, clearCount);
            var response = new Response();
            response.resultStatus = Response.ServiceStatus.SUCCESS;
            return response;
        }
    }
}