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
using Frontend.Notify;
using Frontend.Component.Vfx;
using Core.Entity;
namespace Frontend.Component.State {
public class FiniteState<T> where T : MonoBehaviour {
    public bool persistent {
        get;
        protected set;
    }
    public bool complete {
        get;
        set;
    }
    public bool wait {
        get;
        set;
    }
    protected TimeLine timeLine {
        get;
        set;
    }
    public T owner {
        get;
        set;
    }
    public FiniteState() {
        this.timeLine = new TimeLine();
        this.persistent = false;
        this.complete = false;
        this.wait = false;
        this.owner = null;
    }
    public virtual void Create() {
        return;
    }
    public virtual void Create(Parameter paramter) {
        return;
    }
    public virtual void Update() {
        return;
    }
}
}
