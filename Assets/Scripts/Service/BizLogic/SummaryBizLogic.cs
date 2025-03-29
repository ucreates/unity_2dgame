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
using Service.Integration;
using Service.Integration.Table;
using Console = Core.IO.Console;

namespace Service.BizLogic
{
    public sealed class SummaryBizLogic : BaseBizLogic
    {
        public SummaryBizLogic()
        {
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<TSummaryTable>();
            dao?.Save();
        }

        public int GetBestClearCount()
        {
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<TSummaryTable>();
            var tt = dao?.FindBy(UNIQUE_RECORD_ID) ?? null;
            return tt?.record?.bestClearCount ?? 0;
        }

        public bool UpdateBestClearCount(int clearCount)
        {
            var ubl = new UserBizLogic();
            var userId = ubl.GetPlayer()?.id ?? -1;
            var bestClearCount = GetBestClearCount();
            if (bestClearCount < clearCount)
            {
                var db = DataBase.GetInstance();
                var dao = db?.FindBy<TSummaryTable>();
                var tt = dao.FindBy(UNIQUE_RECORD_ID);
                tt.record.bestClearCount = clearCount;
                dao?.Save(tt.record);
                UpdateStats(userId, bestClearCount);
                return true;
            }

            return false;
        }

        public bool Clear()
        {
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<TScoreTable>();
            var tt = dao.FindBy(UNIQUE_RECORD_ID);
            tt.record.clearCount = 0;
            return dao?.Update(tt.record) ?? false;
        }

        public async void UpdateStats(int usreId, int bestClearCount)
        {
            var request = new CommunicationRequest();
            request.url = new Uri("https://httpbin.org/get");
            request.paramter = new Dictionary<string, object>
            {
                { "userId", usreId },
                { "clearCount", bestClearCount }
            };
            request.method = CommunicationGateway.HttpMethod.Get;
            request.locale = "ja-JP";
            request.bearer = "bearer";
            request.onSuccess = response => { Console.Info(values: response.downloadHandler.text); };
            request.onFaild = response => { Console.Error(values: response.downloadHandler.text); };
            var client = CommunicationGateway.GetInstance();
            await client.AsyncRequest(request);
        }
    }
}