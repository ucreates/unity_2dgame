//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using Core.Entity;
using Frontend.Behaviour.Base;
using Frontend.Behaviour.State;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Notify;
public sealed class ClayPipeGeneratorBehaviour : BaseBehaviour, IStateMachine<ClayPipeGeneratorBehaviour>, INotify {
    public FiniteStateMachine<ClayPipeGeneratorBehaviour> stateMachine {
        get;
        set;
    }
    // Use this for initialization
    public void Start() {
        this.property = new BaseProperty(this);
        this.stateMachine = new FiniteStateMachine<ClayPipeGeneratorBehaviour>(this);
        this.stateMachine.Add("generate", new ClayPipeGeneratorGenerateState());
        this.stateMachine.Add("stop", new ClayPipeGeneratorStopState());
        this.stateMachine.Change("stop");
        this.stateMachine.Play();
        Notifier notifier = Notifier.GetInstance();
        notifier.Add(this, this.property);
    }
    // Update is called once per frame
    public void Update() {
        this.stateMachine.Update();
    }
    public void OnNotify(int notifyMessage, Parameter parameter = null) {
        if (notifyMessage == NotifyMessage.FlappyBirdDead) {
            this.stateMachine.Change("stop");
        } else if (notifyMessage == NotifyMessage.GameStart) {
            this.stateMachine.Change("generate");
        }
    }
}
