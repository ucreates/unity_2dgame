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
using Core.Validator.Message;
using UnityEngine;

namespace Core.Validator.Entity
{
    public sealed class ValidatorResponse
    {
        public ValidatorResponse()
        {
            responseList = new List<ValidatorResponseEntity>();
        }

        public List<ValidatorResponseEntity> responseList { get; set; }

        public bool isSuccess()
        {
            return responseList.Count == 0;
        }

        public List<bool> GetResultList()
        {
            var list = new List<bool>();
            foreach (var entity in responseList) list.Add(entity.result);
            return list;
        }

        public List<BaseValidateMessage> GetMessageList()
        {
            var list = new List<BaseValidateMessage>();
            foreach (var entity in responseList) list.Add(entity.message);
            return list;
        }

        public void Dump()
        {
            foreach (var entity in responseList)
                Debug.Log("result:" + entity.result + ", message:" + entity.message.message);
        }
    }
}