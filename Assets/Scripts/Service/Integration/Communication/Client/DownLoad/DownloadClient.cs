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
using System.Collections.Generic;
using System.Net;
using Service.Integration.Communication.Callback;
using Service.Integration.Communication.Entity;

namespace Service.Integration.Communication.Client
{
    public sealed class DownloadClient
    {
        public DownloadClient(DownLoadClientSetting setting)
        {
            this.setting = setting;
            downLoadFileList = new List<DownLoadAsset>();
        }

        public DownLoadClientSetting setting { get; }

        public List<DownLoadAsset> downLoadFileList { get; }

        public void Request()
        {
            try
            {
                for (var i = 0; i < downLoadFileList.Count; i++)
                {
                    var downloadFile = downLoadFileList[i];
                    var callback = new BaseResponseCallback();
                    var request = new CommunicationRequest();
                    request.clientType = "http";
                    request.setting = setting;
                    request.callback.OnSuccessCallback = callback.OnSuccess;
                    request.callback.OnFaildCallback = callback.OnFaild;
                    request.downLoadFile = downloadFile;
                    CommunicationGateway.GetInstance().Request(request);
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}