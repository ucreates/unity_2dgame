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
    public sealed class MasterDomain : BaseDomain {
    public MasterDomain() {
        this.strategyDictionary.Add("init", new MasterInitStrategy());
    }
}
}
