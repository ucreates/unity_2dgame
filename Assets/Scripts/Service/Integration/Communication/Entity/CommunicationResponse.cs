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
using System.Collections.Generic;
using Service.Integration.Communication.Entity;
namespace Service.Integration.Communication.Entity {
public sealed class CommunicationResponse {
    public enum ResponseStatus {
        SUCCESS = 0,
        FAILD = 1,
    }
    public ResponseStatus status {
        get;
        set;
    }
    public object data {
        get;
        set;
    }
    public CommunicationRequest request {
        get;
        set;
    }
    public string message {
        get;
        set;
    }
    public CommunicationResponse() {
        this.status = ResponseStatus.SUCCESS;
        this.request = new CommunicationRequest();
    }
    public string GetDownLoadPath() {
        return this.request.setting.localPath + this.request.downLoadFile.uri;
    }
}
}