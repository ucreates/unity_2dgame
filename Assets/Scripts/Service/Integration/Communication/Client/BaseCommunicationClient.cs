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
using System.Collections;
using System.Threading;
using Core.Device;
using Service.Integration.Communication;
using Service.Integration.Communication.Entity;
namespace Service.Integration.Communication.Client {
public abstract class BaseCommunicationClient {
    public UserAgent userAgent {
        get;
        set;
    }
    public virtual CommunicationResponse Request(CommunicationRequest parameter) {
        return null;
    }
}
}