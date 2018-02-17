//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Service;
using Service.Integration;
using Service.Integration.Table;
using Service.Integration.Communication;
using Service.BizLogic;
using Core.Entity;
namespace Service.Strategy {
public sealed class PlayerCommitStrategy : BaseStrategy {
    public override Response Request(Parameter parameter) {
        string nickName = parameter.Get<string>("nickname");
        string password = parameter.Get<string>("password");
        int gender = parameter.Get<int>("gender");
        int coin = parameter.Get<int>("coin");
        string mailOrPhone = parameter.Get<string>("mailphone");
        UserBizLogic ubl = new UserBizLogic();
        bool ret = ubl.AddNewUser(nickName, password, gender, mailOrPhone, coin, true);
        MUserTable mut = ubl.GetPlayer();
        ScoreBizLogic sbl = new ScoreBizLogic();
        ret = sbl.AddNewUserScore(mut.id);
        Response response = new Response();
        if (ret) {
            response.resultStatus = Response.ServiceStatus.SUCCESS;
        } else {
            response.resultStatus = Response.ServiceStatus.FAILED;
        }
        return response;
    }
}
}
