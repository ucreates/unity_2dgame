//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Frontend.Component.Vfx;
using UnityEngine;

namespace Frontend.Component.State
{
    public class FiniteState<T> where T : MonoBehaviour
    {
        public FiniteState()
        {
            timeLine = new TimeLine();
            persistent = false;
            complete = false;
            wait = false;
            owner = null;
        }

        public bool persistent { get; protected set; }

        public bool complete { get; set; }

        public bool wait { get; set; }

        protected TimeLine timeLine { get; set; }

        public T owner { get; set; }

        public object notifyParameter { protected get; set; }

        public virtual void Create()
        {
        }

        public virtual void Create(object paramter)
        {
        }

        public virtual void Update()
        {
        }
    }
}