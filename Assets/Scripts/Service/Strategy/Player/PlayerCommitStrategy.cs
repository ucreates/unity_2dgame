//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Collections.Generic;
using Core.Entity;
using Core.Extensions;
using Service.BizLogic;

namespace Service.Strategy
{
    public sealed class PlayerCommitStrategy : BaseStrategy
    {
        public override Response Update(in object parameter)
        {
            var paramBody = (Dictionary<string, object>)parameter;
            var ubl = new UserBizLogic();
            var ret = ubl.AddNewUser(paramBody["nickName"].ToString(), paramBody["password"].ToString(), paramBody["gender"].ToInt32(), paramBody["mailPhone"].ToString(), paramBody["coin"].ToInt32(), true);
            var mut = ubl?.GetPlayer() ?? null;
            var sbl = new ScoreBizLogic();
            ret = sbl?.AddNewUserScore(mut.id) ?? false;
            var response = new Response();
            response.resultStatus = ret ? Response.ServiceStatus.SUCCESS : Response.ServiceStatus.FAILED;
            return response;
        }
    }
}