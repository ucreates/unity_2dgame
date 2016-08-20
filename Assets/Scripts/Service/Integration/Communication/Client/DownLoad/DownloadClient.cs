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
using System.IO;
using System.Collections.Generic;
using System.Net;
using Service.Integration.Communication;
using Service.Integration.Communication.Callback;
using Service.Integration.Communication.Entity;
namespace Service.Integration.Communication.Client {
public sealed class DownloadClient {
    public DownLoadClientSetting setting {
        get;
        private set;
    }
    public List<DownLoadAsset> downLoadFileList {
        get;
        private set;
    }
    public DownloadClient(DownLoadClientSetting setting) {
        this.setting = setting;
        this.downLoadFileList = new List<DownLoadAsset>();
    }
    public void Request() {
        try {
            for (int i = 0; i < this.downLoadFileList.Count; i++) {
                DownLoadAsset downloadFile = this.downLoadFileList[i];
                BaseResponseCallback callback = new BaseResponseCallback();
                CommunicationRequest request = new CommunicationRequest();
                request.clientType = "http";
                request.setting = this.setting;
                request.callback.OnSuccessCallback = callback.OnSuccess;
                request.callback.OnFaildCallback = callback.OnFaild;
                request.downLoadFile = downloadFile;
                CommunicationGateway.GetInstance().Request(request);
            }
        } catch (WebException ex) {
            Console.WriteLine(ex.Message);
        }
    }
}
}
