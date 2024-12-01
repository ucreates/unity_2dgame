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
            var dao = db.FindBy<T>();
            rollBackId = dao.id - addRecordList.Count;
            var ret = Remove();
            if (false == ret) return false;
            ret = Update();
            if (false == ret) return false;
            ret = Add();
            if (false == ret) return false;
            return true;
        }

        public bool Rollback()
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<T>();
            foreach (var record in postAddRecordList)
            {
                var ret = dao.Remove(record);
                if (false == ret) return false;
            }

            foreach (var record in preUpdateRecordList)
            {
                var ret = dao.Update(record);
                if (false == ret) return false;
            }

            dao.Reset(rollBackId);
            foreach (var record in removeRecordList)
            {
                var ret = dao.Save(record);
                if (false == ret) return false;
            }

            return true;
        }

        private bool Add()
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<T>();
            foreach (var record in addRecordList)
            {
                var ret = dao.Save(record);
                if (false == ret) return false;
                var savedRecord = dao.FindBy(dao.id);
                postAddRecordList.Add(savedRecord);
            }

            return true;
        }

        private bool Update()
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<T>();
            foreach (var record in updateRecordList)
            {
                var oldRecord = dao.FindBy(record.primaryKey);
                preUpdateRecordList.Add(oldRecord.Clone() as T);
                var ret = dao.Update(record);
                if (false == ret) return false;
            }

            return true;
        }

        private bool Remove()
        {
            var db = DataBase.GetInstance();
            var dao = db.FindBy<T>();
            foreach (var record in removeRecordList)
            {
                var ret = dao.Remove(record);
                if (false == ret) return false;
            }

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

        public bool registerAddRecord(T record)
        {
            if (false == IsValid(addRecordList, record)) return false;
            addRecordList.Add(record);
            return true;
        }

        public bool registerUpdateRecord(T record)
        {
            if (false == IsValid(removeRecordList, record)) return false;
            if (false == IsValid(updateRecordList, record)) return false;
            updateRecordList.Add(record);
            return true;
        }

        public bool registerRemoveRecord(T record)
        {
            if (false == IsValid(removeRecordList, record)) return false;
            if (false == IsValid(updateRecordList, record)) return false;
            removeRecordList.Add(record);
            return true;
        }

        private bool IsValid(List<T> recordList, T record)
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