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
using Service.Integration.Schema;
using Service.Integration.Table;
namespace Service.Integration {
public sealed class UnitOfWork<T> where T : BaseTable, new() {
    private int rollBackId {
        get;
        set;
    }
    public List<T> addRecordList {
        get;
        set;
    }
    public List<T> updateRecordList {
        get;
        set;
    }
    public List<T> removeRecordList {
        get;
        set;
    }
    public List<T> preUpdateRecordList {
        get;
        set;
    }
    public List<T> postAddRecordList {
        get;
        set;
    }
    public UnitOfWork() {
        this.addRecordList = new List<T>();
        this.updateRecordList = new List<T>();
        this.removeRecordList = new List<T>();
        this.preUpdateRecordList = new List<T>();
        this.postAddRecordList = new List<T>();
    }
    public bool Commit() {
        DataBase db = DataBase.GetInstance();
        Dao<T> dao = db.FindBy<T>();
        this.rollBackId = dao.id - this.addRecordList.Count;
        bool ret = this.Remove();
        if (false == ret) {
            return false;
        }
        ret = this.Update();
        if (false == ret) {
            return false;
        }
        ret = this.Add();
        if (false == ret) {
            return false;
        }
        return true;
    }
    public bool Rollback() {
        DataBase db = DataBase.GetInstance();
        Dao<T> dao = db.FindBy<T>();
        foreach (T record in this.postAddRecordList) {
            bool ret = dao.Remove(record);
            if (false == ret) {
                return false;
            }
        }
        foreach (T record in this.preUpdateRecordList) {
            bool ret = dao.Update(record);
            if (false == ret) {
                return false;
            }
        }
        dao.Reset(this.rollBackId);
        foreach (T record in this.removeRecordList) {
            bool ret = dao.Save(record);
            if (false == ret) {
                return false;
            }
        }
        return true;
    }
    private bool Add() {
        DataBase db = DataBase.GetInstance();
        Dao<T> dao = db.FindBy<T>();
        foreach (T record in this.addRecordList) {
            bool ret = dao.Save(record);
            if (false == ret) {
                return false;
            }
            T savedRecord = dao.FindBy(dao.id);
            this.postAddRecordList.Add(savedRecord);
        }
        return true;
    }
    private bool Update() {
        DataBase db = DataBase.GetInstance();
        Dao<T> dao = db.FindBy<T>();
        foreach (T record in this.updateRecordList) {
            T oldRecord = dao.FindBy(record.primaryKey);
            this.preUpdateRecordList.Add(oldRecord.Clone() as T);
            bool ret = dao.Update(record);
            if (false == ret) {
                return false;
            }
        }
        return true;
    }
    private bool Remove() {
        DataBase db = DataBase.GetInstance();
        Dao<T> dao = db.FindBy<T>();
        foreach (T record in this.removeRecordList) {
            bool ret = dao.Remove(record);
            if (false == ret) {
                return false;
            }
        }
        return true;
    }
    public void Clear() {
        this.addRecordList.Clear();
        this.updateRecordList.Clear();
        this.removeRecordList.Clear();
        this.preUpdateRecordList.Clear();
        this.postAddRecordList.Clear();
    }
    public bool registerAddRecord(T record) {
        if (false == this.IsValid(this.addRecordList, record)) {
            return false;
        }
        this.addRecordList.Add(record);
        return true;
    }
    public bool registerUpdateRecord(T record) {
        if (false == this.IsValid(this.removeRecordList, record)) {
            return false;
        }
        if (false == this.IsValid(this.updateRecordList, record)) {
            return false;
        }
        this.updateRecordList.Add(record);
        return true;
    }
    public bool registerRemoveRecord(T record) {
        if (false == this.IsValid(this.removeRecordList, record)) {
            return false;
        }
        if (false == this.IsValid(this.updateRecordList, record)) {
            return false;
        }
        this.removeRecordList.Add(record);
        return true;
    }
    private bool IsValid(List<T> recordList, T record) {
        if (0 == record.id) {
            return false;
        }
        int left = 0;
        int right = recordList.Count;
        while (left <= right) {
            int mid = left + (right - left) / 2;
            T registeredRecord = recordList[mid];
            if (record.id < registeredRecord.id) {
                right = mid - 1;
            } else if (record.id > registeredRecord.id) {
                left = mid + 1;
            } else {
                return false;
            }
        }
        return true;
    }
}
}
