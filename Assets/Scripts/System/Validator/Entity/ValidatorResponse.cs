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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Validator.Message;
namespace Core.Validator.Entity {
public sealed class ValidatorResponse {
    public List<ValidatorResponseEntity> responseList {
        get;
        set;
    }
    public ValidatorResponse() {
        this.responseList = new List<ValidatorResponseEntity>();
    }
    public bool isSuccess() {
        return (this.responseList.Count == 0);
    }
    public List<bool> GetResultList() {
        List<bool> list = new List<bool>();
        foreach (ValidatorResponseEntity entity in this.responseList) {
            list.Add(entity.result);
        }
        return list;
    }
    public List<BaseValidateMessage> GetMessageList() {
        List<BaseValidateMessage> list = new List<BaseValidateMessage>();
        foreach (ValidatorResponseEntity entity in this.responseList) {
            list.Add(entity.message);
        }
        return list;
    }
    public void Dump() {
        foreach (ValidatorResponseEntity entity in this.responseList) {
            Debug.Log("result:" + entity.result.ToString() + ", message:" + entity.message.message);
        }
    }
}
}
