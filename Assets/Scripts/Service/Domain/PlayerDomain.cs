//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using Service.Strategy;
namespace Service.Domain
{
    public sealed class PlayerDomain : BaseDomain {
    public PlayerDomain() {
        this.strategyDictionary.Add("score", new PlayerScoreStrategy());
        this.strategyDictionary.Add("clear", new PlayerClearStrategy());
        this.strategyDictionary.Add("commit", new PlayerCommitStrategy());
    }
}
}
