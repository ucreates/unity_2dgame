﻿//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Collections.Generic;
using Core.IO;
using Core.Validator.Message;

namespace Core.Validator.Entity
{
    public sealed class ValidatorResponse
    {
        public List<ValidatorResponseEntity> responseList { get; set; } = new();

        public bool isSuccess()
        {
            return responseList.Count == 0;
        }

        public List<bool> GetResultList()
        {
            var result = new List<bool>();
            responseList.ForEach(response => { result.Add(response.result); });
            return result;
        }

        public List<BaseValidateMessage> GetMessageList()
        {
            var result = new List<BaseValidateMessage>();
            responseList.ForEach(response => { result.Add(response.message); });
            return result;
        }

        public void Dump()
        {
            responseList.ForEach(response => { Console.Info(values: $"result:{response.result},message:{response.message.message}"); });
        }
    }
}