//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using UnityEngine;
using System.Collections;
using Core.Entity;
using Service;
using Service.BizLogic;
using Service.Integration.Schema;
namespace Service.Strategy {
public  class BaseStrategy {
    public BaseStrategy() {
    }
    public virtual Response Get(Parameter parameter = null) {
        return null;
    }
    public virtual Response Load(Parameter parameter = null) {
        return null;
    }
    public virtual Response Request(Parameter parameter = null) {
        return null;
    }
    public virtual Response Update(Parameter parameter = null) {
        return null;
    }
    public virtual Response Clear() {
        return null;
    }
}
}
