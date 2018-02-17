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
using System.Collections;
using Service;
using Service.Strategy;
using Frontend.Notify;
using Frontend.Component.Vfx;
using Frontend.Component.Vfx.Sprine;
using Frontend.Component.State;
using Frontend.Component.Property;
using Frontend.Component.Asset.Sound;
using Frontend.Component.Asset.Render;
using Frontend.Behaviour.State;
using Core.Utility;
using Core.Scene;
using Core.Generator;
using Core.Entity;
public sealed class FlappyBirdBehaviour : BaseBehaviour, IStateMachine<FlappyBirdBehaviour>, INotify {
    public Vector2 defaultPosition;
    public FiniteStateMachine<FlappyBirdBehaviour> stateMachine {
        get;
        set;
    }
    public TimeLine deadTimeLine {
        get;
        set;
    }
    public void Start() {
        this.assetCollection.Set("anime", new AnimatorAsset(this));
        this.property = new BaseProperty(this);
        this.stateMachine = new FiniteStateMachine<FlappyBirdBehaviour>(this);
        this.stateMachine.Add("ready", new FlappyBirdReadyState());
        this.stateMachine.Add("go", new FlappyBirdGoState());
        this.stateMachine.Add("fall", new FlappyBirdFallState());
        this.stateMachine.Add("dead", new FlappyBirdDeadState());
        this.stateMachine.Add("hide", new FlappyBIrdHideState());
        this.stateMachine.Add("gameover", new FlappyBIrdGameOverState());
        this.stateMachine.Change("hide");
        this.stateMachine.Play();
        Notifier notifier = Notifier.GetInstance();
        notifier.Add(this, this.property);
        this.deadTimeLine = new TimeLine();
    }
    public void Update() {
        if (this.stateMachine.finiteStateEntity.currentStateName.Equals("ready")) {
            this.stateMachine.Update();
        }
    }
    private void FixedUpdate() {
        if (false == this.stateMachine.finiteStateEntity.currentStateName.Equals("ready")) {
            this.stateMachine.Update();
        }
    }
    private void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.CompareTag("Barrier") && false == this.stateMachine.finiteStateEntity.currentStateName.Equals("dead")) {
            SoundEffectAsset soundAsset = SoundAssetCollection.GetInstance().GetSEAsset("bird_hit") as SoundEffectAsset;
            soundAsset.Play();
            this.stateMachine.Change("dead");
        }
    }
    private void OnCollisionStay2D(Collision2D coll) {
        if (coll.gameObject.name.Equals("LandCollision")) {
            this.stateMachine.Change("gameover");
        }
    }
    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.CompareTag("HitArea") &&
                (this.stateMachine.finiteStateEntity.currentStateName.Equals("go") ||
                 this.stateMachine.finiteStateEntity.currentStateName.Equals("fall"))) {
            SoundEffectAsset soundAsset = SoundAssetCollection.GetInstance().GetSEAsset("point") as SoundEffectAsset;
            soundAsset.Play();
            Parameter parameter = new Parameter();
            parameter.Set<int>("clearcount", 1);
            ServiceGateway.GetInstance()
            .Request("service://player/score")
            .Update(parameter);
        }
    }
    public void OnNotify(NotifyMessage notifyMessage, Parameter parameter = null) {
        if (notifyMessage == NotifyMessage.GameStart) {
            this.stateMachine.Change("go");
        } else if (notifyMessage == NotifyMessage.RegulationShow  || notifyMessage == NotifyMessage.RankingShow) {
            this.stateMachine.Change("hide");
        } else if (notifyMessage == NotifyMessage.GameRestart || notifyMessage == NotifyMessage.GameTitle) {
            this.stateMachine.Change("ready");
            this.stateMachine.Play();
        } else if (notifyMessage == NotifyMessage.GameOver) {
            this.stateMachine.Stop();
        }
    }
}
