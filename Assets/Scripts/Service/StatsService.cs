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
using Service.Strategy;

namespace Service
{
    public sealed class StatsService : BaseService
    {
        public StatsService()
        {
            strategyDictionary = new Dictionary<string, BaseStrategy>
            {
                { "player", new PlayerStatsStrategy() },
                { "ranking", new RankingStatsStrategy() },
                { "result", new ResultStatsStrategy() }
            };
        }
    }
}