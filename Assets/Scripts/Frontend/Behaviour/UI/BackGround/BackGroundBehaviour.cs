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
using Service;
using Frontend.Notify;
using Frontend.Component.State;
using Frontend.Component.Property;
using Frontend.Behaviour.State;
using Core.Entity;
public sealed class BackGroundBehaviour : BaseBehaviour, IStateMachine<BackGroundBehaviour> {
    public FiniteStateMachine<BackGroundBehaviour> stateMachine {
        get;
        set;
    }
    public void Start() {
        this.property = new BaseProperty(this);
        this.stateMachine = new FiniteStateMachine<BackGroundBehaviour>(this);
        this.stateMachine.Add("show", new BackGroundShowState());
        this.stateMachine.Change("show");
        this.stateMachine.Play();
    }
    public void Update() {
        this.stateMachine.Update();
    }
}
