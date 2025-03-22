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
    public sealed class PlayerService : BaseService
    {
        public PlayerService()
        {
            strategyDictionary = new Dictionary<string, BaseStrategy>
            {
                { "score", new PlayerScoreStrategy() },
                { "clear", new PlayerClearStrategy() },
                { "commit", new PlayerCommitStrategy() }
            };
        }
    }
}