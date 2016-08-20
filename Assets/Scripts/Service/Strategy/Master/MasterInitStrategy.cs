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
using System.Collections.Generic;
using Core.Entity;
using Frontend.Component.Asset.Sound;
using Service;
using Service.BizLogic;
using Service.Integration;
using Service.Integration.Schema;
using Service.Integration.Table;
namespace Service.Strategy {
public sealed class MasterInitStrategy : BaseStrategy {
    public const int DUMMY_USER_NUMBER = 10;
    public const int FIRST_DUMMY_USER_ID = 1;
    public override Response Update(Parameter parameter) {
        Response response = new Response();
        response.resultStatus = Response.ServiceStatus.SUCCESS;
        this.LoadUserData();
        this.LoadItemMaster();
        this.LoadSoundData();
        return response;
    }
    private void LoadUserData() {
        UserBizLogic ubl = new UserBizLogic();
        ScoreBizLogic sbl = new ScoreBizLogic();
        System.Random random = new System.Random();
        List<MUserTable > dummyUserList = new List<MUserTable>();
        for (int i = MasterInitStrategy.FIRST_DUMMY_USER_ID; i <= MasterInitStrategy.DUMMY_USER_NUMBER; i++) {
            string id = i.ToString();
            dummyUserList.Add(new MUserTable("User" + id, "password" + id, i, "09098765432", random.Next(300, 1000), false));
        }
        ubl.AddNewUser(dummyUserList);
        List<TScoreTable > dummyScoreList = new List<TScoreTable>();
        for (int i = MasterInitStrategy.FIRST_DUMMY_USER_ID; i <= MasterInitStrategy.DUMMY_USER_NUMBER; i++) {
            dummyScoreList.Add(new TScoreTable(i, random.Next(1, 20)));
        }
        sbl.AddNewUserScore(dummyScoreList);
    }
    private void LoadItemMaster() {
        Dictionary<string, int> itemMasterList = new Dictionary<string, int>();
        itemMasterList.Add("TYPE_A", 100);
        itemMasterList.Add("TYPE_B", 200);
        itemMasterList.Add("TYPE_C", 300);
        itemMasterList.Add("TYPE_D", 400);
        ItemBizLogic ibl = new ItemBizLogic();
        ibl.InitializeMaster(itemMasterList);
    }
    private void LoadSoundData() {
        SoundAssetCollection collection = SoundAssetCollection.GetInstance();
        collection.SetBGMAsset("athletic", new BGMAsset("Audio/BGM/athletic"));
        collection.SetBGMAsset("player_down", new BGMAsset("Audio/BGM/player_down", true));
        collection.SetSEAsset("bird_wing", new SoundEffectAsset("Audio/SE/sfx_wing"));
        collection.SetSEAsset("bird_hit", new SoundEffectAsset("Audio/SE/sfx_hit"));
        collection.SetSEAsset("bird_die", new SoundEffectAsset("Audio/SE/sfx_die"));
        collection.SetSEAsset("point", new SoundEffectAsset("Audio/SE/sfx_point"));
    }
}
}
