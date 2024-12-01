﻿//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Service.Strategy;

namespace Service
{
    public sealed class MasterService : BaseService
    {
        public MasterService()
        {
            strategyDictionary.Add("init", new MasterInitStrategy());
        }
    }
}