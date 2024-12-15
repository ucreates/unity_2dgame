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
using System.Collections.Generic;
using Core.Entity;
using Frontend.Component.Asset.Sound;
using Service.BizLogic;
using Service.Integration.Table;

namespace Service.Strategy
{
    public sealed class MasterInitStrategy : BaseStrategy
    {
        public const int DUMMY_USER_NUMBER = 10;
        public const int FIRST_DUMMY_USER_ID = 1;

        public override Response Update(Parameter parameter)
        {
            var response = new Response();
            response.resultStatus = Response.ServiceStatus.SUCCESS;
            LoadUserData();
            LoadItemMaster();
            LoadSoundData();
            return response;
        }

        private void LoadUserData()
        {
            var ubl = new UserBizLogic();
            var sbl = new ScoreBizLogic();
            var random = new Random();
            var dummyUserList = new List<MUserTable>();
            for (var i = FIRST_DUMMY_USER_ID; i <= DUMMY_USER_NUMBER; i++)
            {
                var id = i.ToString();
                dummyUserList.Add(new MUserTable("User" + id, "password" + id, i, "09098765432", random.Next(300, 1000),
                    false));
            }

            ubl.AddNewUser(dummyUserList);
            var dummyScoreList = new List<TScoreTable>();
            for (var i = FIRST_DUMMY_USER_ID; i <= DUMMY_USER_NUMBER; i++)
                dummyScoreList.Add(new TScoreTable(i, random.Next(1, 20)));
            sbl.AddNewUserScore(dummyScoreList);
        }

        private void LoadItemMaster()
        {
            var itemMasterDictionary = new Dictionary<string, int>();
            itemMasterDictionary.Add("TYPE_A", 100);
            itemMasterDictionary.Add("TYPE_B", 200);
            itemMasterDictionary.Add("TYPE_C", 300);
            itemMasterDictionary.Add("TYPE_D", 400);
            var ibl = new ItemBizLogic();
            ibl.InitializeMaster(itemMasterDictionary);
        }

        private void LoadSoundData()
        {
            var collection = SoundAssetCollection.GetInstance();
            collection.SetBGMAsset("athletic", new BGMAsset("Audio/BGM/athletic"));
            collection.SetBGMAsset("player_down", new BGMAsset("Audio/BGM/player_down", true));
            collection.SetSEAsset("bird_wing", new SoundEffectAsset("Audio/SE/sfx_wing"));
            collection.SetSEAsset("bird_hit", new SoundEffectAsset("Audio/SE/sfx_hit"));
            collection.SetSEAsset("bird_die", new SoundEffectAsset("Audio/SE/sfx_die"));
            collection.SetSEAsset("point", new SoundEffectAsset("Audio/SE/sfx_point"));
        }
    }
}