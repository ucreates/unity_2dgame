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
using Frontend.Behaviour.State;
using Frontend.Component.Asset.Render;
using Frontend.Component.Asset.Sound;
using Frontend.Component.Property;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using Frontend.Notify;
using Service;
using UnityEngine;

public sealed class FlappyBirdBehaviour : BaseBehaviour, IStateMachine<FlappyBirdBehaviour>, INotify
{
    public Vector2 defaultPosition;

    public TimeLine deadTimeLine { get; set; }

    public void Start()
    {
        assetCollection.Set("anime", new AnimatorAsset(this));
        property = new BaseProperty(this);
        stateMachine = new FiniteStateMachine<FlappyBirdBehaviour>(this);
        stateMachine.Add("ready", new FlappyBirdReadyState());
        stateMachine.Add("go", new FlappyBirdGoState());
        stateMachine.Add("fall", new FlappyBirdFallState());
        stateMachine.Add("dead", new FlappyBirdDeadState());
        stateMachine.Add("hide", new FlappyBIrdHideState());
        stateMachine.Add("gameover", new FlappyBIrdGameOverState());
        stateMachine.Change("hide");
        stateMachine.Play();
        var notifier = Notifier.GetInstance();
        notifier.Add(this, property);
        deadTimeLine = new TimeLine();
    }

    public void Update()
    {
        if (stateMachine.finiteStateEntity.currentStateName.Equals("ready")) stateMachine.Update();
    }

    private void FixedUpdate()
    {
        if (!stateMachine.finiteStateEntity.currentStateName.Equals("ready")) stateMachine.Update();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Barrier") && !stateMachine.finiteStateEntity.currentStateName.Equals("dead"))
        {
            var soundAsset = SoundAssetCollection.GetInstance().GetSEAsset("bird_hit") as SoundEffectAsset;
            soundAsset.Play();
            stateMachine.Change("dead");
        }
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.name.Equals("LandCollision")) stateMachine.Change("gameover");
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("HitArea") &&
            (stateMachine.finiteStateEntity.currentStateName.Equals("go") ||
             stateMachine.finiteStateEntity.currentStateName.Equals("fall")))
        {
            var soundAsset = SoundAssetCollection.GetInstance().GetSEAsset("point") as SoundEffectAsset;
            soundAsset.Play();
            var parameter = new Parameter();
            parameter.Set("clearcount", 1);
            ServiceGateway.GetInstance()
                .Request("service://player/score")
                .Update(parameter);
        }
    }

    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null)
    {
        if (notifyMessage == NotifyMessage.GameStart)
        {
            stateMachine.Change("go");
        }
        else if (notifyMessage == NotifyMessage.RegulationShow || notifyMessage == NotifyMessage.RankingShow)
        {
            stateMachine.Change("hide");
        }
        else if (notifyMessage == NotifyMessage.GameRestart || notifyMessage == NotifyMessage.GameTitle)
        {
            stateMachine.Change("ready");
            stateMachine.Play();
        }
        else if (notifyMessage == NotifyMessage.GameOver)
        {
            stateMachine.Stop();
        }
    }

    public FiniteStateMachine<FlappyBirdBehaviour> stateMachine { get; set; }
}