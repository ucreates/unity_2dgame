//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using Frontend.Behaviour.State;
using Frontend.Component.Property;
using Frontend.Component.State;

public sealed class BackGroundBehaviour : BaseBehaviour, IStateMachine<BackGroundBehaviour>
{
    public void Start()
    {
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<BackGroundBehaviour>(this);
        stateMachine?.Add("show", new BackGroundShowState());
        stateMachine?.Change("show");
        stateMachine?.Play();
    }

    public void Update()
    {
        stateMachine?.Update();
    }

    public FiniteStateMachine<BackGroundBehaviour> stateMachine { get; set; }
}