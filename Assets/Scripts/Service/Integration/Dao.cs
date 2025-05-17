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
using System.Linq;
using Core.Extensions.Array;
using Service.Integration.Dto.Assembler;
using Service.Integration.Query.Expression;
using Service.Integration.Schema;
using Service.Integration.Table;

namespace Service.Integration
{
    public sealed class Dao<T> : BaseDao where T : BaseTable, new()
    {
        public Dao() : this(false)
        {
        }

        public Dao(bool persistent)
        {
            id = 0;
            name = GetType()?.FullName?.ToLower();
            if (persistent)
            {
                assembler = new PersistenceAssembler<T>(name);
                recordList = assembler?.WriteToTableList();
                recordList ??= new List<T>();
            }
            else
            {
                recordList = new List<T>();
            }
        }

        private PersistenceAssembler<T> assembler { get; }

        public List<T> recordList { get; }

        public string name { get; }

        public int id { get; private set; }

        public (T record, int index) FindBy(int recordId)
        {
            var record = recordList.FirstOrDefault(record => record.id == recordId);
            var index = recordList.IndexOf(record);
            return (record, index);
        }

        public T FindBy(KeySchema primaryKey)
        {
            if (0 == recordList.Count) return null;
            var recordId = primaryKey.GetId();
            var result = FindBy(recordId);
            return null != result.record && result.record?.primaryKey == primaryKey ? result.record : null;
        }

        public List<T> FindBy(in Func<T, bool> predicate)
        {
            return recordList.Where(predicate).ToList();
        }

        public List<T> FindAll(params BaseExpression[] expressions)
        {
            var result = new List<T>(recordList);
            expressions.ForEach(expression =>
            {
                if (expression is OrderByExpression orderByExpression) result = OrderBy(orderByExpression.orderFieldName, orderByExpression.orderType, result);

                if (expression is LimitExpression limitExpression) result = Limit(limitExpression.limit, result);
            });
            return result.ToList();
        }

        public T FindFirst()
        {
            return recordList.FirstOrDefault();
        }

        public T FindLast()
        {
            return recordList.LastOrDefault();
        }

        public T FindPrevious(int recordId)
        {
            return FindBy(recordId).record?.previous as T ?? null;
        }

        public T FindNext(int recordId)
        {
            return FindBy(recordId).record?.next as T ?? null;
        }

        public bool Save()
        {
            if (0 == recordList.Count)
            {
                var table = new T();
                return Save(table);
            }

            return false;
        }

        public bool Save(T record)
        {
            var result = FindBy(record.id);
            if (null != result.record) return false;
            var getPreviousAndNextRecords = new Func<int, (T previous, T next)>(delegate(int index)
            {
                var previousIdx = index - 1;
                var nextIdx = index + 1;
                var previous = 0 <= previousIdx ? recordList.ElementAt(previousIdx) : null;
                var next = recordList.Count - 1 > nextIdx ? recordList.ElementAt(nextIdx) : null;
                return (previous, next);
            });
            id++;
            record.id = id;
            record?.Build();
            recordList.Add(record);
            var index = recordList.IndexOf(record);
            var previousAndNextRecords = getPreviousAndNextRecords(index);
            record.previous = previousAndNextRecords.previous;
            record.next = previousAndNextRecords.next;
            if (null != assembler) assembler.WriteToPlayerPrefs(recordList);
            return true;
        }

        public bool Update(T record)
        {
            var result = FindBy(record.id);
            if (null == result.record) return false;
            recordList[result.index] = record;
            if (null != assembler) assembler.WriteToPlayerPrefs(recordList);
            return true;
        }

        public bool Remove(int recordId)
        {
            var result = FindBy(recordId);
            return Remove(result.record);
        }

        public bool Remove(T record)
        {
            var result = FindBy(record.id);
            if (null == result.record) return false;
            recordList.RemoveAt(result.index);
            if (null != assembler) assembler.WriteToPlayerPrefs(recordList);
            return true;
        }

        private List<T> OrderBy(string orderFieldName, OrderByExpression.OrderType orderType, List<T> recordListParams = null)
        {
            var result = null == recordListParams ? recordList : new List<T>(recordListParams);
            var propertyInfo = typeof(T).GetProperty(orderFieldName) ?? typeof(T).GetProperty("");
            if (orderType == OrderByExpression.OrderType.Asc)
                result = result?.OrderBy(record => propertyInfo?.GetValue(record)).ToList();
            else if (orderType == OrderByExpression.OrderType.Desc)
                result = result?.OrderByDescending(record => propertyInfo?.GetValue(record)).ToList();
            return result;
        }

        public List<T> Limit(int limit, List<T> recordListParams = null)
        {
            var result = null == recordListParams ? recordList : new List<T>(recordListParams);
            return result?.Take(limit)?.ToList();
        }

        public bool HasRecord(in Func<T, bool> predicate)
        {
            return recordList.Any(predicate);
        }

        public bool Empty()
        {
            return !recordList.Any();
        }

        public override void Clear()
        {
            recordList.Clear();
            if (null != assembler) assembler.WriteToPlayerPrefs(recordList);
            id = 0;
        }

        public void Reset(int rollBackId)
        {
            id = rollBackId;
        }
    }
}