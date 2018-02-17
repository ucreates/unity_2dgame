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
using System.Collections.Generic;
namespace Frontend.Component.State {
public sealed class FiniteStateEntity<T> where T : MonoBehaviour {
    public bool isNewState {
        get;
        set;
    }
    public FiniteState<T> state {
        get;
        set;
    }
    public string currentStateName {
        get;
        set;
    }
    public string previousStateName {
        get;
        set;
    }
    public string nextStateName {
        get;
        set;
    }
    public FiniteStateEntity() {
        this.state = null;
        this.currentStateName = string.Empty;
        this.previousStateName = string.Empty;
        this.nextStateName = string.Empty;
        this.isNewState = false;
    }
    public void Update(string nextStateName, FiniteState<T> nextState) {
        this.previousStateName = this.currentStateName;
        this.currentStateName = nextStateName;
        this.state = nextState;
        this.isNewState =  true;
    }
}
}
