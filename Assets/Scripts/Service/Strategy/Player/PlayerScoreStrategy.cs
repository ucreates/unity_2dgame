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
using Service;
using Service.Integration;
using Service.Integration.Table;
using Service.BizLogic;
using Core.Entity;
namespace Service.Strategy {
public sealed class PlayerScoreStrategy : BaseStrategy {
    public override Response Update(Parameter parameter) {
        int clearCount = parameter.Get<int>("clearcount");
        UserBizLogic ubl = new UserBizLogic();
        MUserTable mut = ubl.GetPlayer();
        ScoreBizLogic sbl = new ScoreBizLogic();
        sbl.AddClearCount(mut.id, clearCount);
        Response ret = new Response();
        ret.resultStatus = Response.ServiceStatus.SUCCESS;
        return ret;
    }
}
}
