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
    public sealed class PlayerCommitStrategy : BaseStrategy
    {
        public override Response Request(Parameter parameter)
        {
            var nickName = parameter.Get<string>("nickname");
            var password = parameter.Get<string>("password");
            var gender = parameter.Get<int>("gender");
            var coin = parameter.Get<int>("coin");
            var mailOrPhone = parameter.Get<string>("mailphone");
            var ubl = new UserBizLogic();
            var ret = ubl.AddNewUser(nickName, password, gender, mailOrPhone, coin, true);
            var mut = ubl.GetPlayer();
            var sbl = new ScoreBizLogic();
            ret = sbl.AddNewUserScore(mut.id);
            var response = new Response();
            if (ret)
                response.resultStatus = Response.ServiceStatus.SUCCESS;
            else
                response.resultStatus = Response.ServiceStatus.FAILED;
            return response;
        }
    }
}