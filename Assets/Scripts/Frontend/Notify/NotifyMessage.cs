//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System;

namespace Frontend.Notify
{
    public class NotifyMessage : EventArgs
    {
        public enum Title
        {
            GameTitle = 0,
            GameReady = 1,
            GameStart = 2,
            GameOver = 3,
            GameRestart = 4,
            FlappyBirdDead = 5,
            InputProfileError = 6,
            InputProfile = 7,
            RegulationShow = 8,
            RankingShow = 10,
            RankingHide = 11,
            NoticeShow = 13,
            NoticeHide = 14,
            ShopShow = 15,
            ShopHide = 16,
            ShopConfirmShow = 17,
            ShopCommitShow = 18
        }

        public object parameter;
        public Title title;
    }
}