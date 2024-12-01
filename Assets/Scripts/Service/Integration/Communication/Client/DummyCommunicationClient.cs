//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System;
using System.Threading;
using Service.Integration.Communication.Entity;

namespace Service.Integration.Communication.Client
{
    public sealed class DummyCommunicationClient : BaseCommunicationClient
    {
        public override CommunicationResponse Request(CommunicationRequest parameter)
        {
            var response = new CommunicationResponse();
            var random = new Random();
            var sleepTime = random.Next(500, 4000);
            Thread.Sleep(sleepTime);
            response.request = parameter;
            return response;
        }
    }
}