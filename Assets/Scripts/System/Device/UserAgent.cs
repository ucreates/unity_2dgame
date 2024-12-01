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
using UnityEngine;

namespace Core.Device
{
    public sealed class UserAgent
    {
        public UserAgent()
        {
            deviceInfoList = new List<string>();
            deviceInfoList.Add(SystemInfo.deviceName);
            deviceInfoList.Add(SystemInfo.deviceModel);
            deviceInfoList.Add(SystemInfo.operatingSystem);
        }

        private List<string> deviceInfoList { get; }

        public string Create()
        {
            var uastring = "(";
            for (var i = 0; i < deviceInfoList.Count; i++)
            {
                var data = deviceInfoList[i];
                if (i < deviceInfoList.Count - 1) uastring += data + " ";
            }

            uastring += ")";
            return uastring;
        }
    }
}