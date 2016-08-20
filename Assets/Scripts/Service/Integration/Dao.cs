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
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Core.Validator.Unit;
using Service.Integration.Schema;
using Service.Integration.Table;
using Service.Integration.Dto.Assembler;
using Service.Integration.Query.Expression;
namespace Service.Integration {
public sealed class Dao<T> : BaseDao where T : BaseTable, new() {
    private PersistenceAssembler<T> assembler {
        get;
        set;
    }
    public List<T> recordList {
        get;
        private set;
    }
    public string name {
        get;
        private set;
    }
    public int id {
        get;
        private set;
    }
    public int seek {
        get;
        private set;
    }
    public bool isFOR {
        get {
            return (this.seek == 0);
        }
    }
    public bool isEOR {
        get {
            return (this.seek == this.recordList.Count - 1);
        }
    }
    public Dao() : this(false) {}
    public Dao(bool persistent) {
        this.id = 0;
        this.seek = 0;
        this.name = this.GetType().FullName.ToLower();
        if (false != persistent) {
            this.assembler =  new PersistenceAssembler<T>(this.name);
            this.recordList = this.assembler.WriteToTableList();
            if (null == this.recordList) {
                this.recordList = new List<T>();
            }
        } else {
            this.recordList = new List<T>();
        }
    }
    public T FindBy(int id) {
        int index;
        return this.FindBy(id, out index);
    }
    public T FindBy(int id, out int index) {
        index = -1;
        if (0 == this.recordList.Count) {
            return null;
        }
        index = -1;
        int left = 0;
        int right = this.recordList.Count - 1;
        T ret = null;
        while (left <= right) {
            int mid =  left + (right - left) / 2;
            T record = this.recordList[mid];
            if (id < record.id) {
                right = mid - 1;
            } else if (id > record.id) {
                left = mid + 1;
            } else {
                index = mid;
                ret = record;
                break;
            }
        }
        return ret;
    }
    public T FindBy(KeySchema primaryKey) {
        int index;
        return this.FindBy(primaryKey, out index);
    }
    public T FindBy(KeySchema primaryKey, out int index) {
        index = -1;
        if (0 == this.recordList.Count) {
            return null;
        }
        int id = primaryKey.GetId();
        T record = this.FindBy(id, out index);
        if (null != record && record.primaryKey == primaryKey) {
            return record;
        }
        return null;
    }
    public List<T> FindBy(BaseExpression condition, BaseExpression order = null, BaseExpression limit = null) {
        List<T> ret = new List<T> ();
        if (0 == this.recordList.Count) {
            return ret;
        }
        List<ConditionExpression> conditionList = new List<ConditionExpression>();
        if (condition is AndExpression || condition is OrExpression) {
            MultiConditionExpression mcexp = condition as MultiConditionExpression;
            conditionList = mcexp.conditionList;
        } else {
            ConditionExpression cexp = condition as ConditionExpression;
            conditionList.Add(cexp);
        }
        foreach (T record in this.recordList) {
            bool add = false;
            foreach (ConditionExpression cexp in conditionList) {
                PropertyInfo pinfo = record.GetType().GetProperty(cexp.fieldName);
                object value = pinfo.GetValue(record, null);
                if (cexp.comparisonOperator.Equals("==")) {
                    add = cexp.field.Equal(value);
                } else if (cexp.comparisonOperator.Equals("!=")) {
                    add = !cexp.field.Equal(value);
                } else if (cexp.comparisonOperator.Equals(">")) {
                    add = cexp.field.MoreThan(value);
                } else if (cexp.comparisonOperator.Equals(">=")) {
                    add = cexp.field.MoreThanEqual(value);
                } else if (cexp.comparisonOperator.Equals("<")) {
                    add = cexp.field.LessThan(value);
                } else if (cexp.comparisonOperator.Equals("<=")) {
                    add = cexp.field.LessThanEqual(value);
                }
                if (condition is AndExpression && false == add) {
                    break;
                } else if (condition is OrExpression && false != add) {
                    break;
                }
            }
            if (false != add) {
                ret.Add(record);
            }
        }
        if (1 < ret.Count) {
            if (null != order && order is OrderByExpression) {
                OrderByExpression orderExpression = order as OrderByExpression;
                ret = this.OrderBy(orderExpression.orderFieldName, ret, orderExpression.orderType);
            }
            if (null != limit && limit is LimitExpression) {
                LimitExpression limitExpression = limit as LimitExpression;
                ret = this.Limit(ret, limitExpression.limit);
            }
        }
        return ret;
    }
    public List<T> FindAll() {
        return this.recordList;
    }
    public List<T> FindAll(BaseExpression expression) {
        if (null == expression) {
            return this.recordList;
        }
        List<T> ret = new List<T> (this.recordList);
        if (expression is OrderByExpression) {
            OrderByExpression orderByExpression = expression as OrderByExpression;
            return this.OrderBy(orderByExpression.orderFieldName, ret, orderByExpression.orderType);
        } else if (expression is LimitExpression) {
            LimitExpression limitExpression = expression as LimitExpression;
            return this.Limit(ret, limitExpression.limit);
        }
        return ret;
    }
    public List<T> FindAll(OrderByExpression orderBy, LimitExpression limit) {
        List<T> ret = new List<T> (this.recordList);
        if (null != orderBy) {
            ret = this.OrderBy(orderBy.orderFieldName, ret, orderBy.orderType);
        }
        if (null != limit) {
            ret = this.Limit(ret, limit.limit);
        }
        return ret;
    }
    public T FindFirst() {
        if (0 == this.recordList.Count) {
            return null;
        }
        return this.recordList[0];
    }
    public T FindLast() {
        if (0 == this.recordList.Count) {
            return null;
        }
        int lastIdx = this.recordList.Count - 1;
        return this.recordList[lastIdx];
    }
    public T FindNext() {
        this.seek++;
        int lastIdx = this.recordList.Count;
        if (this.seek >= lastIdx && 0 == this.recordList.Count) {
            this.seek = lastIdx - 1;
            return null;
        }
        return this.recordList[this.seek];
    }
    public T FindPrevious() {
        this.seek--;
        if (this.seek < 0 && 0 == this.recordList.Count) {
            this.seek = 0;
            return null;
        }
        return this.recordList[this.seek];
    }
    public bool Save() {
        if (0 == this.recordList.Count) {
            T table = new T();
            return this.Save(table);
        }
        return false;
    }
    public bool Save(T record) {
        if (null != this.FindBy(record.primaryKey)) {
            return false;
        }
        this.id++;
        record.id = this.id;
        record.Build();
        this.recordList.Add(record);
        if (null != this.assembler) {
            this.assembler.WriteToPlayerPrefs(this.recordList);
        }
        return true;
    }
    public bool Update(int id, T record) {
        int index;
        if (null == this.FindBy(id, out index)) {
            return false;
        }
        this.recordList[index] = record;
        if (null != this.assembler) {
            this.assembler.WriteToPlayerPrefs(this.recordList);
        }
        return true;
    }
    public bool Update(T record) {
        int index;
        if (null == this.FindBy(record.primaryKey, out index)) {
            return false;
        }
        this.recordList[index] = record;
        if (null != this.assembler) {
            this.assembler.WriteToPlayerPrefs(this.recordList);
        }
        return true;
    }
    public bool Remove(int id) {
        int index;
        if (null == this.FindBy(id, out index)) {
            return false;
        }
        this.recordList.RemoveAt(index);
        if (null != this.assembler) {
            this.assembler.WriteToPlayerPrefs(this.recordList);
        }
        return true;
    }
    public bool Remove(T record) {
        int index;
        if (null == this.FindBy(record.primaryKey, out index)) {
            return false;
        }
        this.recordList.RemoveAt(index);
        if (null != this.assembler) {
            this.assembler.WriteToPlayerPrefs(this.recordList);
        }
        return true;
    }
    private List<T> OrderBy(string orderFieldName, List<T> recordList, OrderByExpression.OrderType orderType) {
        for (int i = 0; i < recordList.Count; i++) {
            for (int j = recordList.Count - 1; j > i; j--) {
                int previousIndex = j - 1;
                T recordA = recordList[j];
                T recordB = recordList[previousIndex];
                PropertyInfo pinfoA = recordA.GetType().GetProperty(orderFieldName);
                PropertyInfo pinfoB = recordB.GetType().GetProperty(orderFieldName);
                BaseFieldSchema fieldScheamA = recordA.fieldSchemaCollection.Get(orderFieldName);
                BaseFieldSchema fieldScheamB = recordB.fieldSchemaCollection.Get(orderFieldName);
                object valueA = pinfoA.GetValue(recordA, null);
                object valueB = pinfoB.GetValue(recordB, null);
                if (orderType == OrderByExpression.OrderType.Asc && false != fieldScheamA.MoreThan(valueB)) {
                    T table = recordList[previousIndex];
                    recordList[previousIndex] = recordList[j];
                    recordList[j] = table;
                } else if (orderType == OrderByExpression.OrderType.Desc && false != fieldScheamB.MoreThan(valueA)) {
                    T table = recordList[previousIndex];
                    recordList[previousIndex] = recordList[j];
                    recordList[j] = table;
                }
            }
        }
        return recordList;
    }
    public List<T> Limit(List<T> originRcordList, int limit) {
        if (limit < originRcordList.Count) {
            List<T> ret = new List<T>();
            int index = 0;
            foreach (T record in originRcordList) {
                if (limit == index) {
                    break;
                }
                ret.Add(record);
                index++;
            }
            return ret;
        }
        return originRcordList;
    }
    public override void Clear() {
        this.recordList.Clear();
        if (null != this.assembler) {
            this.assembler.WriteToPlayerPrefs(this.recordList);
        }
        this.id = 0;
        this.seek = 0;
    }
    public void Reset() {
        this.seek = 0;
    }
    public void Reset(int rollBackId) {
        this.id = rollBackId;
    }
}
}