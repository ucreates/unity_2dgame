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
using System.Linq;
using UnityEngine;

namespace Core.Device
{
    public sealed class UserAgent
    {
        private List<string> deviceInfoList { get; } = new()
        {
            SystemInfo.deviceName,
            SystemInfo.deviceModel,
            SystemInfo.operatingSystem
        };

        public string Create()
        {
            return $"({deviceInfoList.Aggregate((result, current) => string.Join(result, current))})";
        }
    }
}