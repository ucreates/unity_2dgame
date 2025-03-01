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
    public sealed class FlappyBIrdGameOverState : FiniteState<FlappyBirdBehaviour>
    {
        public override void Create()
        {
            timeLine = new TimeLine();
        }

        public override void Update()
        {
            if (timeLine?.currentTime < 0.5f)
            {
                timeLine?.Next();
                return;
            }

            var notifier = Notifier.GetInstance();
            if (notifier?.currentMessage?.title != NotifyMessage.Title.GameOver &&
                notifier?.currentMessage?.title != NotifyMessage.Title.RankingShow)
            {
                notifier?.Notify(NotifyMessage.Title.GameOver);
                complete = true;
            }
        }
    }
}