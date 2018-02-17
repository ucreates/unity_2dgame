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
using Service.BizLogic;
using Core.Entity;
namespace Service.Strategy {
public sealed class RankingStatsStrategy : BaseStrategy {
    public override Response Get(Parameter parameter = null) {
        Response sret = new Response();
        ScoreBizLogic sbl = new ScoreBizLogic();
        List<TScoreTable> rankingList = sbl.GetRankingList();
        List<MUserTable> userList = new List<MUserTable>();
        UserBizLogic ubl = new UserBizLogic();
        foreach (TScoreTable score in rankingList) {
            MUserTable user = ubl.GetUser(score.userId);
            userList.Add(user);
        }
        sret.Set<List<TScoreTable>>("rankinglist", rankingList);
        sret.Set<List<MUserTable>>("userlist", userList);
        sret.resultStatus = Response.ServiceStatus.SUCCESS;
        return sret;
    }
}
}
