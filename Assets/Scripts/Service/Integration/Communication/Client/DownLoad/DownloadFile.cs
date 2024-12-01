//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.IO;

namespace Service.Integration.Communication.Client
{
    public sealed class DownLoadAsset
    {
        public DownLoadAsset(string uri) : this(uri, string.Empty)
        {
        }

        public DownLoadAsset(string uri, string contentsId)
        {
            this.uri = uri;
            this.contentsId = contentsId;
        }

        public string contentsId { get; set; }

        public string uri { get; set; }

        public string directory => Path.GetDirectoryName(uri);

        public string extension => Path.GetExtension(uri);

        public string fileName => Path.GetFileName(uri);
    }
}