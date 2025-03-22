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
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entity;
using Frontend.Component.Asset.Sound;
using Service.BizLogic;
using Service.Integration;
using Service.Integration.Table;
using UnityEngine;
using UnityEngine.Networking;
using Random = System.Random;

namespace Service.Strategy
{
    public sealed class MasterInitStrategy : BaseStrategy
    {
        public const int DUMMY_USER_NUMBER = 10;
        public const int FIRST_DUMMY_USER_ID = 1;

        public override Response Update(object parameter)
        {
            var response = new Response();
            response.resultStatus = Response.ServiceStatus.SUCCESS;
            Task.WhenAll(LoadUserData(), LoadItemMaster(), LoadSoundData()).Wait();
            return response;
        }

        public override IEnumerator Request(object parameter = null)
        {
            var mbl = new MasterBizLogic();
            yield return mbl.DownloadRequest(parameter as Action<float>);

            var request = new CommunicationRequest();
            request.url = new Uri("https://httpbin.org/get");
            request.paramter = new Dictionary<string, object>
            {
                { "num", 1 },
                { "str", "test1" }
            };
            request.method = CommunicationGateway.HttpMethod.Get;
            request.locale = "ja-JP";
            request.bearer = "bearer";
            request.onSuccess = response => { Debug.Log(response.downloadHandler.text); };
            request.onFaild = response => { Debug.Log(response.downloadHandler.text); };
            var client = CommunicationGateway.GetInstance();
            yield return client.SyncRequest(request);
            if (client.result != UnityWebRequest.Result.Success)
            {
                client.Dump();
                yield break;
            }

            request = new CommunicationRequest();
            request.url = new Uri("https://httpbin.org/post");
            request.paramter = new Dictionary<string, object>
            {
                { "num", 2 },
                { "str", "test2" }
            };
            request.method = CommunicationGateway.HttpMethod.Post;
            request.locale = "ja-JP";
            request.bearer = "bearer";
            request.onSuccess = response => { Debug.Log(response.downloadHandler.text); };
            request.onFaild = response => { Debug.Log(response.downloadHandler.text); };
            client = CommunicationGateway.GetInstance();
            yield return client.SyncRequest(request);
            if (client.result != UnityWebRequest.Result.Success)
            {
                client.Dump();
                yield break;
            }

            Debug.Log("request success!");
        }

        private async Task LoadUserData()
        {
            var ubl = new UserBizLogic();
            var sbl = new ScoreBizLogic();
            var random = new Random();
            var dummyUserList = new List<MUserTable>();
            for (var i = FIRST_DUMMY_USER_ID; i <= DUMMY_USER_NUMBER; i++)
            {
                var id = i.ToString();
                dummyUserList.Add(new MUserTable($"User{id}", $"password{id}", i, "09098765432", random.Next(300, 1000),
                    false));
            }

            ubl?.AddNewUser(dummyUserList);
            var dummyScoreList = new List<TScoreTable>();
            for (var i = FIRST_DUMMY_USER_ID; i <= DUMMY_USER_NUMBER; i++)
                dummyScoreList.Add(new TScoreTable(i, random.Next(1, 20)));
            sbl?.AddNewUserScore(dummyScoreList);
        }

        private async Task LoadItemMaster()
        {
            var itemMasterDictionary = new Dictionary<string, int>
            {
                { "TYPE_A", 100 },
                { "TYPE_B", 200 },
                { "TYPE_C", 300 },
                { "TYPE_D", 400 }
            };
            var ibl = new ItemBizLogic();
            ibl?.InitializeMaster(itemMasterDictionary);
        }

        private async Task LoadSoundData()
        {
            var collection = SoundAssetCollection.GetInstance();
            collection?.SetBGMAsset("athletic", new BGMAsset("Audio/BGM/athletic"));
            collection?.SetBGMAsset("player_down", new BGMAsset("Audio/BGM/player_down", true));
            collection?.SetSeAsset("bird_wing", new SoundEffectAsset("Audio/SE/sfx_wing"));
            collection?.SetSeAsset("bird_hit", new SoundEffectAsset("Audio/SE/sfx_hit"));
            collection?.SetSeAsset("bird_die", new SoundEffectAsset("Audio/SE/sfx_die"));
            collection?.SetSeAsset("point", new SoundEffectAsset("Audio/SE/sfx_point"));
        }
    }
}