//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Collections.Generic;
using Core.Extensions;
using Service.Integration.Communication.Callback;
using Service.Integration.Communication.Client;

namespace Service.Integration.Communication.Entity
{
    public sealed class CommunicationRequest
    {
        public CommunicationRequest()
        {
            requestDataDictionary = new Dictionary<string, object>();
            callback = new BaseResponseCallback();
            clientType = string.Empty;
            type = string.Empty;
            threadId = 0;
            methodName = "GET";
            downLoadFile = new DownLoadAsset(string.Empty);
        }

        public List<CommunicationRequest> nextRequestParameterList { get; set; }

        public string clientType { get; set; }

        public int threadId { get; set; }

        public string type { get; set; }

        public DownLoadClientSetting setting { get; set; }

        public BaseResponseCallback callback { get; set; }

        public string methodName { get; set; }

        public Dictionary<string, object> requestDataDictionary { get; set; }

        public DownLoadAsset downLoadFile { get; set; }

        public int dataLength
        {
            get
            {
                var length = 0;
                requestDataDictionary.ForEach(pair => { length += pair.Value.ToString().Length; });
                return length;
            }
        }

        public string GetURL()
        {
            return setting.uri.AbsoluteUri + downLoadFile.uri;
        }

        public string GetQueryString()
        {
            return string.Empty;
        }
    }
}