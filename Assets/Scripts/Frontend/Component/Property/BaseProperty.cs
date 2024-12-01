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

namespace Frontend.Component.Property
{
    public class BaseProperty
    {
        public BaseProperty(MonoBehaviour behaviour)
        {
            type = behaviour.tag;
            name = behaviour.name;
            id = behaviour.GetInstanceID();
        }

        public virtual string category => string.Empty;

        public string type { get; protected set; }

        public string name { get; protected set; }

        public int id { get; protected set; }
    }
}