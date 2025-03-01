//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Core.Generator;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using UnityEngine;

namespace Frontend.Behaviour.State
{
    public sealed class ClayPipeGeneratorGenerateState : FiniteState<ClayPipeGeneratorBehaviour>
    {
        private TimeLine generateTimeLine { get; set; }

        public override void Create()
        {
            generateTimeLine = new TimeLine();
            var gy = Random.Range(-2f, 2f);
            ResourceGenerator.Generate("Prefabs/ClayPipe", new Vector3(5f, gy, 0f), Quaternion.identity);
        }

        public override void Update()
        {
            var ct = generateTimeLine?.currentTime ?? 0f;
            if (ct >= 2f)
            {
                var gy = Random.Range(-2f, 2f);
                ResourceGenerator.Generate("Prefabs/ClayPipe", new Vector3(5f, gy, 0f), Quaternion.identity);
                generateTimeLine.Restore();
            }

            generateTimeLine?.Next();
        }
    }
}