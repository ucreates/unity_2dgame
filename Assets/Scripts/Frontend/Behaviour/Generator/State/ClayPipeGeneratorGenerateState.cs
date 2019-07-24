//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using Frontend.Component.State;
using Frontend.Component.Vfx;
using UnityEngine;
namespace Frontend.Behaviour.State
{
    public sealed class ClayPipeGeneratorGenerateState : FiniteState<ClayPipeGeneratorBehaviour> {
    private TimeLine generateTimeLine {
        get;
        set;
    }
    public override void Create() {
        this.generateTimeLine = new TimeLine();
        float gy = Random.Range(-2f, 2f);
        Core.Object.Resource.Instanciate("Prefabs/ClayPipe", new UnityEngine.Vector3(5f, gy, 0f), UnityEngine.Quaternion.identity);
    }
    public override void Update() {
        float ct = this.generateTimeLine.currentTime;
        if (ct >= 2f) {
            float gy = Random.Range(-2f, 2f);
            Core.Object.Resource.Instanciate("Prefabs/ClayPipe", new UnityEngine.Vector3(5f, gy, 0f), UnityEngine.Quaternion.identity);
            this.generateTimeLine.Restore();
        }
        this.generateTimeLine.Next();
    }
}
}
