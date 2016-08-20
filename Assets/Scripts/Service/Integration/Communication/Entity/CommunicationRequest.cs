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
using System;
using System.Collections;
using System.Collections.Generic;
using Service.Integration.Communication.Callback;
using Service.Integration.Communication.Client;
namespace Service.Integration.Communication.Entity {
public sealed class CommunicationRequest {
    public List<CommunicationRequest> nextRequestParameterList {
        get;
        set;
    }
    public string clientType {
        get;
        set;
    }
    public int threadId {
        get;
        set;
    }
    public string type {
        get;
        set;
    }
    public DownLoadClientSetting setting {
        get;
        set;
    }
    public BaseResponseCallback callback {
        get;
        set;
    }
    public string methodName {
        get;
        set;
    }
    public Dictionary<string, object> requestDataList {
        get;
        set;
    }
    public DownLoadAsset downLoadFile {
        get;
        set;
    }
    public int dataLength {
        get {
            int length = 0;
            foreach (string key in this.requestDataList.Keys) {
                object data = this.requestDataList[key];
                length += data.ToString().Length;
            }
            return length;
        }
    }
    public CommunicationRequest() {
        this.requestDataList = new Dictionary<string, object>();
        this.callback = new BaseResponseCallback();
        this.clientType = string.Empty;
        this.type = string.Empty;
        this.threadId = 0;
        this.methodName = "GET";
        this.downLoadFile = new DownLoadAsset(string.Empty);
    }
    public string GetURL() {
        return this.setting.uri.AbsoluteUri + this.downLoadFile.uri;
    }
    public string GetQueryString() {
        return string.Empty;
    }
}
}