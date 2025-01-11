//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

namespace Service.Integration.Communication.Entity
{
    public sealed class CommunicationResponse
    {
        public enum ResponseStatus
        {
            SUCCESS = 0,
            FAILD = 1
        }

        public CommunicationResponse()
        {
            status = ResponseStatus.SUCCESS;
            request = new CommunicationRequest();
        }

        public ResponseStatus status { get; set; }

        public object data { get; set; }

        public CommunicationRequest request { get; set; }

        public string message { get; set; }

        public string GetDownLoadPath()
        {
            return $"{request.setting.localPath}{request.downLoadFile.uri}";
        }
    }
}