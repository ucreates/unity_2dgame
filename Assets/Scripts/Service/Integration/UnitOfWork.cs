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
using Service.Integration.Table;

namespace Service.Integration
{
    public sealed class UnitOfWork<T> where T : BaseTable, new()
    {
        public UnitOfWork()
        {
            addRecordList = new List<T>();
            updateRecordList = new List<T>();
            removeRecordList = new List<T>();
            preUpdateRecordList = new List<T>();
            postAddRecordList = new List<T>();
        }

        private int rollBackId { get; set; }

        public List<T> addRecordList { get; set; }

        public List<T> updateRecordList { get; set; }

        public List<T> removeRecordList { get; set; }

        public List<T> preUpdateRecordList { get; set; }

        public List<T> postAddRecordList { get; set; }

        public bool Commit()
        {
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<T>();
            rollBackId = dao.id - addRecordList.Count;
            var ret = Remove();
            if (!ret) return false;
            ret = Update();
            if (!ret) return false;
            ret = Add();
            if (!ret) return false;
            return true;
        }

        public bool Rollback()
        {
            var result = true;
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<T>();
            postAddRecordList.ForEach(record =>
            {
                if (!result) return;
                result = dao?.Remove(record) ?? false;
            });
            preUpdateRecordList.ForEach(record =>
            {
                if (!result) return;
                result = dao?.Update(record) ?? false;
            });
            dao?.Reset(rollBackId);
            removeRecordList.ForEach(record =>
            {
                if (!result) return;
                result = dao?.Save(record) ?? false;
            });
            return result;
        }

        private bool Add()
        {
            var result = true;
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<T>();
            addRecordList.ForEach(record =>
            {
                if (!result) return;
                result = dao?.Save(record) ?? false;
                var savedResult = dao?.FindBy(dao.id) ?? null;
                postAddRecordList.Add(savedResult?.record);
            });
            return result;
        }

        private bool Update()
        {
            var result = true;
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<T>();
            updateRecordList.ForEach(record =>
            {
                if (!result) return;
                var oldRecord = dao?.FindBy(record.primaryKey);
                preUpdateRecordList.Add(oldRecord.Clone() as T);
                result = dao?.Update(record) ?? false;
            });
            return true;
        }

        private bool Remove()
        {
            var result = true;
            var db = DataBase.GetInstance();
            var dao = db?.FindBy<T>();
            removeRecordList.ForEach(record =>
            {
                if (!result) return;
                result = dao?.Remove(record) ?? false;
            });
            return true;
        }

        public void Clear()
        {
            addRecordList.Clear();
            updateRecordList.Clear();
            removeRecordList.Clear();
            preUpdateRecordList.Clear();
            postAddRecordList.Clear();
        }

        public bool RegisterAddRecord(in T record)
        {
            if (!IsValid(addRecordList, record)) return false;
            addRecordList.Add(record);
            return true;
        }

        public bool RegisterUpdateRecord(in T record)
        {
            if (!IsValid(removeRecordList, record)) return false;
            if (!IsValid(updateRecordList, record)) return false;
            updateRecordList.Add(record);
            return true;
        }

        public bool RegisterRemoveRecord(in T record)
        {
            if (!IsValid(removeRecordList, record)) return false;
            if (!IsValid(updateRecordList, record)) return false;
            removeRecordList.Add(record);
            return true;
        }

        private bool IsValid(in List<T> recordList, in T record)
        {
            if (0 == record.id) return false;
            var left = 0;
            var right = recordList.Count;
            while (left <= right)
            {
                var mid = left + (right - left) / 2;
                var registeredRecord = recordList[mid];
                if (record.id < registeredRecord.id)
                    right = mid - 1;
                else if (record.id > registeredRecord.id)
                    left = mid + 1;
                else
                    return false;
            }

            return true;
        }
    }
}