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
            var serviceParams = (Dictionary<string, object>)parameter;
            var ubl = new UserBizLogic();
            var addNewUser = ubl.AddNewUser(serviceParams["nickName"].ToString(), serviceParams["password"].ToString(), serviceParams["gender"].ToInt32(), serviceParams["mailPhone"].ToString(), serviceParams["coin"].ToInt32(), true);
            var userMaster = ubl?.GetPlayer() ?? null;
            var sbl = new ScoreBizLogic();
            addNewUser = sbl?.AddNewUserScore(userMaster.id) ?? false;
            var response = new Response();
            response.resultStatus = addNewUser ? Response.ServiceStatus.SUCCESS : Response.ServiceStatus.FAILED;
            return response;
        }
    }
}