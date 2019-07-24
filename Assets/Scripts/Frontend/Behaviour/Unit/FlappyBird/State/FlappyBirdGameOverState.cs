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
using Frontend.Notify;
namespace Frontend.Behaviour.State
{
    public sealed class FlappyBIrdGameOverState : FiniteState<FlappyBirdBehaviour> {
    public override void Create() {
        this.timeLine = new TimeLine();
    }
    public override void Update() {
        if (this.timeLine.currentTime < 0.5f) {
            this.timeLine.Next();
            return;
        }
        Notifier notifier = Notifier.GetInstance();
        if (notifier.currentMessage != NotifyMessage.GameOver && notifier.currentMessage != NotifyMessage.RankingShow) {
            notifier.Notify(NotifyMessage.GameOver);
            this.complete = true;
        }
    }
}
}
