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
public sealed class PlayerClearStrategy : BaseStrategy {
    public override Response Update(Parameter parameter) {
        Response ret = new Response();
        ScoreBizLogic sbl = new ScoreBizLogic();
        sbl.Clear();
        ret.resultStatus = Response.ServiceStatus.SUCCESS;
        return ret;
    }
}
}
