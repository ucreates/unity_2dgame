﻿//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

namespace Core.Entity
{
    public sealed class Response
    {
        public enum ServiceStatus
        {
            SUCCESS = 0,
            FAILED = 0
        }

        public string errorMessage { get; set; }

        public object data { get; set; }

        public ServiceStatus resultStatus { get; set; }
    }
}