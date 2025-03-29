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
using System.Collections;
using System.IO;
using Service.Integration;
using UnityEngine;
using UnityEngine.Networking;
using Console = Core.IO.Console;
using File = UnityEngine.Windows.File;

namespace Service.BizLogic
{
    public sealed class MasterBizLogic : BaseBizLogic
    {
        public IEnumerator DownloadRequest(Action<float> callback)
        {
            var request = new CommunicationRequest();
            request.url = new Uri("https://github.com/ucreates/unity_2dgame/archive/refs/heads/master.zip");
            request.method = CommunicationGateway.HttpMethod.Get;
            request.onSuccess = response =>
            {
                var path = Path.Combine(Application.temporaryCachePath, "master.zip");
                if (File.Exists(path)) File.Delete(path);
                File.WriteAllBytes(path, response.downloadHandler.data);
            };
            request.onFaild = response => { Console.Error(values: response.downloadHandler.text); };
            request.onDownloadProgress = (progress, size) => { callback?.Invoke(progress); };
            var client = CommunicationGateway.GetInstance();
            yield return client.DownloadRequest(request);
            if (UnityWebRequest.Result.Success != client.result)
            {
                client.Dump();
                yield break;
            }

            yield break;
        }
    }
}