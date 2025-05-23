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
using Frontend.Behaviour.State;
using Frontend.Component.Asset.Render;
using Frontend.Component.Asset.Sound;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using Frontend.Notify;
using Service;
using UniRx;
using UnityEngine;

public sealed class FlappyBirdBehaviour : BaseBehaviour, IStateMachine<FlappyBirdBehaviour>, INotify
{
    public Vector2 defaultPosition;

    public TimeLine deadTimeLine { get; set; } = new();

    public void Start()
    {
        rx = Notifier.GetInstance()?.OnNotify()?.Where(message => { return message.title == NotifyMessage.Title.GameStart || message.title == NotifyMessage.Title.RegulationShow || message.title == NotifyMessage.Title.RankingShow || message.title == NotifyMessage.Title.GameRestart || message.title == NotifyMessage.Title.GameTitle || message.title == NotifyMessage.Title.GameOver; })?.Subscribe(message => { OnNotify(message); });
        assetCollection?.Set("anime", new AnimatorAsset(this));
        stateMachine = new FiniteStateMachine<FlappyBirdBehaviour>(this);
        stateMachine?.Add(new Dictionary<string, FiniteState<FlappyBirdBehaviour>>
        {
            { "ready", new FlappyBirdReadyState() },
            { "go", new FlappyBirdGoState() },
            { "fall", new FlappyBirdFallState() },
            { "dead", new FlappyBirdDeadState() },
            { "hide", new FlappyBIrdHideState() },
            { "gameover", new FlappyBIrdGameOverState() }
        });
        stateMachine?.Change("hide");
        stateMachine?.Play();
    }

    public void Update()
    {
        if (stateMachine.finiteStateEntity.currentStateName.Equals("ready")) stateMachine?.Update();
    }

    private void FixedUpdate()
    {
        if (!stateMachine.finiteStateEntity.currentStateName.Equals("ready")) stateMachine?.Update();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Barrier") && !stateMachine.finiteStateEntity.currentStateName.Equals("dead"))
        {
            var se = SoundAssetCollection.GetInstance().GetSeAsset("bird_hit");
            se?.Play();
            stateMachine?.Change("dead");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("LandCollision")) stateMachine?.Change("gameover");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HitArea") &&
            (stateMachine.finiteStateEntity.currentStateName.Equals("go") ||
             stateMachine.finiteStateEntity.currentStateName.Equals("fall")))
        {
            var se = SoundAssetCollection.GetInstance().GetSeAsset("point");
            se?.Play();
            ServiceGateway.GetInstance()
                ?.Request("service://player/score")
                ?.Update(1);
        }
    }

    public void OnNotify(NotifyMessage notifyMessage)
    {
        if (notifyMessage?.title == NotifyMessage.Title.GameStart)
        {
            stateMachine?.Change("go");
        }
        else if (notifyMessage?.title == NotifyMessage.Title.RegulationShow || notifyMessage?.title == NotifyMessage.Title.RankingShow)
        {
            stateMachine?.Change("hide");
        }
        else if (notifyMessage?.title == NotifyMessage.Title.GameRestart || notifyMessage?.title == NotifyMessage.Title.GameTitle)
        {
            stateMachine?.Change("ready");
            stateMachine?.Play();
        }
        else if (notifyMessage?.title == NotifyMessage.Title.GameOver)
        {
            stateMachine?.Stop();
        }
    }

    public FiniteStateMachine<FlappyBirdBehaviour> stateMachine { get; set; }
}