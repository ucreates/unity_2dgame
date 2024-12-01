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
using Core.Entity;
using UnityEngine;

namespace Frontend.Component.State
{
    public sealed class FiniteStateMachine<T> where T : MonoBehaviour
    {
        public FiniteStateMachine(T owner)
        {
            finiteStateEntity = new FiniteStateEntity<T>();
            this.owner = owner;
            persistentStateList = new Dictionary<string, FiniteState<T>>();
            stateList = new Dictionary<string, FiniteState<T>>();
        }

        public Dictionary<string, FiniteState<T>> stateList { get; set; }

        public Dictionary<string, FiniteState<T>> persistentStateList { get; set; }

        public FiniteStateEntity<T> finiteStateEntity { get; set; }

        private Parameter paramter { get; set; }

        public T owner { get; }

        public void Change(string newStateName)
        {
            Change(newStateName, null);
        }

        public void Change(string newStateName, bool update)
        {
            Change(newStateName, null, update);
        }

        public void Change(string newStateName, Parameter paramter, bool update = false)
        {
            this.paramter = paramter;
            var nextState = Get(newStateName);
            finiteStateEntity.Update(newStateName, nextState);
            if (update) Update();
        }

        public void Update()
        {
            if (false == finiteStateEntity.state.complete)
            {
                if (finiteStateEntity.isNewState)
                {
                    if (null != paramter)
                        finiteStateEntity.state.Create(paramter);
                    else
                        finiteStateEntity.state.Create();
                    finiteStateEntity.isNewState = false;
                }

                if (null != finiteStateEntity.state && false == finiteStateEntity.state.complete &&
                    false == finiteStateEntity.state.wait) finiteStateEntity.state.Update();
            }

            foreach (var key in persistentStateList.Keys)
            {
                var state = persistentStateList[key];
                if (state.complete) state.Update();
            }
        }

        public FiniteState<T> Get(string stateName)
        {
            if (stateList.ContainsKey(stateName)) return stateList[stateName];
            return null;
        }

        public bool Add(string stateName, FiniteState<T> state, bool isFirstState = false)
        {
            if (false == state.persistent)
            {
                if (false == stateList.ContainsKey(stateName))
                {
                    state.owner = owner;
                    stateList.Add(stateName, state);
                    return true;
                }
            }
            else
            {
                if (false == persistentStateList.ContainsKey(stateName))
                {
                    state.owner = owner;
                    persistentStateList.Add(stateName, state);
                    return true;
                }
            }

            return false;
        }

        public void Play()
        {
            foreach (var key in stateList.Keys)
            {
                var state = stateList[key];
                state.wait = false;
                state.complete = false;
            }

            foreach (var key in persistentStateList.Keys)
            {
                var state = persistentStateList[key];
                state.wait = false;
                state.complete = false;
                state.Create();
            }
        }

        public void Pause()
        {
            foreach (var key in stateList.Keys) stateList[key].wait = true;
        }

        public void Stop()
        {
            foreach (var key in stateList.Keys) stateList[key].complete = true;
            foreach (var key in persistentStateList.Keys) persistentStateList[key].complete = true;
        }
    }
}