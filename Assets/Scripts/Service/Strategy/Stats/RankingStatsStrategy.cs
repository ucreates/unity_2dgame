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
using Service.BizLogic;
using Service.Integration.Table;

namespace Service.Strategy
{
    public sealed class RankingStatsStrategy : BaseStrategy
    {
        public override Response Get(in object parameter = null)
        {
            var sret = new Response();
            var sbl = new ScoreBizLogic();
            var rankingList = sbl?.GetRankingList();
            var userList = new List<MUserTable>();
            var ubl = new UserBizLogic();
            rankingList.ForEach(score =>
            {
                var user = ubl?.GetUser(score.userId);
                userList.Add(user);
            });
            sret.data = (rankingList, userList);
            sret.resultStatus = Response.ServiceStatus.SUCCESS;
            return sret;
        }
    }
}