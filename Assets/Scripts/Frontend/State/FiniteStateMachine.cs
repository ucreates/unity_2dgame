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
using Frontend.Notify;
using Core.Entity;
namespace Frontend.Component.State {
public sealed class FiniteStateMachine <T> where T : MonoBehaviour {
    public Dictionary<string, FiniteState<T>> stateList {
        get;
        set;
    }
    public Dictionary<string, FiniteState<T>> persistentStateList {
        get;
        set;
    }
    public FiniteStateEntity<T> finiteStateEntity {
        get;
        set;
    }
    private Parameter paramter {
        get;
        set;
    }
    public T owner {
        get;
        private set;
    }
    public FiniteStateMachine(T owner) {
        this.finiteStateEntity = new FiniteStateEntity<T>();
        this.owner = owner;
        this.persistentStateList = new Dictionary<string, FiniteState<T>>();
        this.stateList = new Dictionary<string, FiniteState<T>>();
    }
    public void Change(string newStateName) {
        this.Change(newStateName, null, false);
        return;
    }
    public void Change(string newStateName, bool update) {
        this.Change(newStateName, null, update);
        return;
    }
    public void Change(string newStateName, Parameter paramter, bool update = false) {
        this.paramter = paramter;
        FiniteState<T> nextState = this.Get(newStateName);
        this.finiteStateEntity.Update(newStateName, nextState);
        if (false != update) {
            this.Update();
        }
        return;
    }
    public void Update() {
        if (false == this.finiteStateEntity.state.complete) {
            if (this.finiteStateEntity.isNewState) {
                if (null != this.paramter) {
                    this.finiteStateEntity.state.Create(this.paramter);
                } else {
                    this.finiteStateEntity.state.Create();
                }
                this.finiteStateEntity.isNewState = false;
            }
            if (null != this.finiteStateEntity.state && false == this.finiteStateEntity.state.complete && false == this.finiteStateEntity.state.wait) {
                this.finiteStateEntity.state.Update();
            }
        }
        foreach (string key in this.persistentStateList.Keys) {
            FiniteState<T> state = this.persistentStateList [key];
            if (false != state.complete) {
                state.Update();
            }
        }
    }
    public FiniteState<T> Get(string stateName) {
        if (this.stateList.ContainsKey(stateName)) {
            return this.stateList[stateName];
        }
        return null;
    }
    public bool Add(string stateName, FiniteState<T> state, bool isFirstState = false) {
        if (false == state.persistent) {
            if (false == this.stateList.ContainsKey(stateName)) {
                state.owner = this.owner;
                this.stateList.Add(stateName, state);
                return true;
            }
        } else {
            if (false == this.persistentStateList.ContainsKey(stateName)) {
                state.owner = this.owner;
                this.persistentStateList.Add(stateName, state);
                return true;
            }
        }
        return false;
    }
    public void Play() {
        foreach (string key in this.stateList.Keys) {
            FiniteState<T> state = this.stateList [key];
            state.wait = false;
            state.complete = false;
        }
        foreach (string key in this.persistentStateList.Keys) {
            FiniteState<T> state = this.persistentStateList [key];
            state.wait = false;
            state.complete = false;
            state.Create();
        }
    }
    public void Pause() {
        foreach (string key in this.stateList.Keys) {
            this.stateList [key].wait = true;
        }
    }
    public void Stop() {
        foreach (string key in this.stateList.Keys) {
            this.stateList [key].complete = true;
        }
        foreach (string key in this.persistentStateList.Keys) {
            this.persistentStateList [key].complete = true;
        }
    }
}
}
