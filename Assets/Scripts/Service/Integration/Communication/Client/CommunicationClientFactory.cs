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
using System.Threading;
using System.Collections;
using Service.Integration.Communication;
namespace Service.Integration.Communication.Client {
public sealed class CommunicationClientFactory {
    public static BaseCommunicationClient FactoryMethod(string clientName) {
        BaseCommunicationClient client = null;
        switch (clientName) {
        case "dummy":
            client = new DummyCommunicationClient();
            break;
        case "http":
            client = new HttpCommunicationClient();
            break;
        default:
            break;
        }
        return client;
    }
}
}
