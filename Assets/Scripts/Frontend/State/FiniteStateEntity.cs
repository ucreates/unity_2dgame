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

namespace Frontend.Component.State
{
    public sealed class FiniteStateEntity<T> where T : MonoBehaviour
    {
        public FiniteStateEntity()
        {
            state = null;
            currentStateName = string.Empty;
            previousStateName = string.Empty;
            nextStateName = string.Empty;
            isNewState = false;
        }

        public bool isNewState { get; set; }

        public FiniteState<T> state { get; set; }

        public string currentStateName { get; set; }

        public string previousStateName { get; set; }

        public string nextStateName { get; set; }

        public void Update(string nextStateName, FiniteState<T> nextState)
        {
            previousStateName = currentStateName;
            currentStateName = nextStateName;
            state = nextState;
            isNewState = true;
        }
    }
}