//======================================================================
// Project Name    : unity plugin
//
// Copyright © 2017 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Collections.Generic;
using System.IO;

namespace UnityPlugin.Core.IO
{
    public sealed class ArchivePlugin : BasePlugin
    {
        public ArchivePlugin()
        {
            contentsPathList = new List<string>();
        }

        public List<string> contentsPathList { get; set; }

        public List<string> Decompress(string archivePath)
        {
            var plugin = new UnityManagedPlugin.Core.IO.ArchivePlugin();
            contentsPathList = plugin.Decompress(archivePath);
            return contentsPathList;
        }

        public string FindContentsPathByName(string contentsName)
        {
            var ret = string.Empty;
            foreach (var path in contentsPathList)
            {
                var fileName = Path.GetFileName(path);
                if (contentsName.Equals(fileName))
                {
                    ret = path;
                    break;
                }
            }

            return ret;
        }
    }
}