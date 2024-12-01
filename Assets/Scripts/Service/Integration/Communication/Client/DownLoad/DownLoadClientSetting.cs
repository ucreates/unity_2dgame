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
using UnityEngine;

namespace Service.Integration.Communication.Client
{
    public sealed class DownLoadClientSetting
    {
        public DownLoadClientSetting(Uri uri, string localRootPath)
        {
            this.uri = uri;
            localPath = localRootPath + uri.LocalPath;
        }

        public Uri uri { get; set; }

        public string localPath { get; set; }

        public void Dump()
        {
            Debug.Log("uri:" + uri);
            Debug.Log("localPath:" + localPath);
        }
    }
}