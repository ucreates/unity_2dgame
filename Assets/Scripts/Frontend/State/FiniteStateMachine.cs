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
using System.Linq;
using Core.Extensions;
using UnityEngine;

namespace Frontend.Component.State
{
    public sealed class FiniteStateMachine<T> where T : MonoBehaviour
    {
        public FiniteStateMachine(T owner)
        {
            finiteStateEntity = new FiniteStateEntity<T>();
            this.owner = owner;
            persistentStateDictionary = new Dictionary<string, FiniteState<T>>();
            stateDictionary = new Dictionary<string, FiniteState<T>>();
        }

        public Dictionary<string, FiniteState<T>> stateDictionary { get; set; }

        public Dictionary<string, FiniteState<T>> persistentStateDictionary { get; set; }

        public FiniteStateEntity<T> finiteStateEntity { get; set; }

        private object paramter { get; set; }

        public T owner { get; }

        public void Change(string newStateName)
        {
            Change(newStateName, null);
        }

        public void Change(string newStateName, bool update)
        {
            Change(newStateName, null, update);
        }

        public void Change(string newStateName, object paramter, bool update = false)
        {
            this.paramter = paramter;
            var nextState = Get(newStateName);
            finiteStateEntity.Update(newStateName, nextState);
            if (update) Update();
        }

        public void Update()
        {
            if (!finiteStateEntity.state.complete)
            {
                if (finiteStateEntity.isNewState)
                {
                    if (null != paramter)
                        finiteStateEntity.state.Create(paramter);
                    else
                        finiteStateEntity.state.Create();
                    finiteStateEntity.isNewState = false;
                }

                if (null != finiteStateEntity.state && !
                        finiteStateEntity.state.complete &&
                    !finiteStateEntity.state.wait)
                    finiteStateEntity.state.Update();
            }

            persistentStateDictionary.ForEach(pair =>
            {
                if (pair.Value.complete) pair.Value.Update();
            });
        }

        public FiniteState<T> Get(string stateName)
        {
            return stateDictionary.FirstOrDefault(pair => pair.Key.Equals(stateName)).Value;
        }

        public bool Add(string stateName, in FiniteState<T> state)
        {
            if (!state.persistent)
            {
                if (!stateDictionary.ContainsKey(stateName))
                {
                    state.owner = owner;
                    stateDictionary.Add(stateName, state);
                    return true;
                }
            }
            else
            {
                if (!persistentStateDictionary.ContainsKey(stateName))
                {
                    state.owner = owner;
                    persistentStateDictionary.Add(stateName, state);
                    return true;
                }
            }

            return false;
        }

        public bool Add(in Dictionary<string, FiniteState<T>> stateDictionary)
        {
            var result = false;
            stateDictionary.ForEach(pair =>
            {
                result = Add(pair.Key, pair.Value);
                return result;
            });
            return result;
        }

        public void Play()
        {
            stateDictionary.ForEach(pair =>
            {
                pair.Value.wait = false;
                pair.Value.complete = false;
            });

            persistentStateDictionary.ForEach(pair =>
            {
                pair.Value.wait = false;
                pair.Value.complete = false;
                pair.Value.Create();
            });
        }

        public void Pause()
        {
            stateDictionary.ForEach(pair => { pair.Value.wait = true; });
        }

        public void Stop()
        {
            stateDictionary.ForEach(pair => { pair.Value.complete = true; });
            persistentStateDictionary.ForEach(pair => { pair.Value.complete = true; });
        }
    }
}