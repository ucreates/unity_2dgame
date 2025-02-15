//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Entity;
using Service.BizLogic;

namespace Service.Strategy
{
    public sealed class PlayerClearStrategy : BaseStrategy
    {
        public override Response Update(object parameter)
        {
            var ret = new Response();
            var sbl = new ScoreBizLogic();
            sbl.Clear();
            ret.resultStatus = Response.ServiceStatus.SUCCESS;
            return ret;
        }
    }
}