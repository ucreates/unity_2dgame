//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Threading;
using Core.Device;
using Service.Integration.Communication.Client;
using Service.Integration.Communication.Entity;

namespace Service.Integration.Communication
{
    public sealed class CommunicationGateway
    {
        private CommunicationGateway()
        {
            userAgent = new UserAgent();
        }

        private static CommunicationGateway instance { get; set; }

        private UserAgent userAgent { get; }

        public static CommunicationGateway GetInstance()
        {
            if (null == instance) instance = new CommunicationGateway();
            return instance;
        }

        public void Request(CommunicationRequest parameter)
        {
            var t = new Thread(AsyncRequest);
            parameter.threadId = t.ManagedThreadId;
            t.Start(parameter);
        }

        private void AsyncRequest(object parameter)
        {
            var reqparam = parameter as CommunicationRequest;
            var client = CommunicationClientFactory.FactoryMethod(reqparam.clientType);
            client.userAgent = userAgent;
            if (null == client) return;
            var response = client.Request(reqparam);
            lock (reqparam)
            {
                if (response.status == CommunicationResponse.ResponseStatus.SUCCESS)
                    reqparam.callback.OnSuccessCallback(response);
                else
                    reqparam.callback.OnFaildCallback(response);
            }
        }
    }
}