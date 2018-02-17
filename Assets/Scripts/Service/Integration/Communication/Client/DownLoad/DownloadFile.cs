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
namespace Service.Integration.Communication.Client {
public sealed class DownLoadAsset {
    public string contentsId {
        get;
        set;
    }
    public string uri {
        get;
        set;
    }
    public string directory {
        get {
            return Path.GetDirectoryName(this.uri);
        }
    }
    public string extension {
        get {
            return Path.GetExtension(this.uri);
        }
    }
    public string fileName {
        get {
            return Path.GetFileName(this.uri);
        }
    }
    public DownLoadAsset(string uri) : this(uri, string.Empty) {
    }
    public DownLoadAsset(string uri, string contentsId) {
        this.uri = uri;
        this.contentsId = contentsId;
    }
}
}
