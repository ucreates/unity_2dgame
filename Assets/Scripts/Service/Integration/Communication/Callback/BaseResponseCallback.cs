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
using Service.Integration.Communication.Entity;
namespace Service.Integration.Communication.Callback {
public class BaseResponseCallback {
    public delegate void OnCallback(CommunicationResponse response);
    public OnCallback OnSuccessCallback {
        get;
        set;
    }
    public OnCallback OnFaildCallback {
        get;
        set;
    }
    public virtual void OnSuccess(CommunicationResponse response) {
        return;
    }
    public virtual void  OnFaild(CommunicationResponse response) {
        return;
    }
    protected virtual void NextRequest(CommunicationRequest parameter) {
        return;
    }
}
}
