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
            seek = 0;
            name = GetType().FullName.ToLower();
            if (persistent)
            {
                assembler = new PersistenceAssembler<T>(name);
                recordList = assembler.WriteToTableList();
                if (null == recordList) recordList = new List<T>();
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

        public int seek { get; private set; }

        public bool isFOR => seek == 0;

        public bool isEOR => seek == recordList.Count - 1;

        public T FindBy(int id)
        {
            int index;
            return FindBy(id, out index);
        }

        public T FindBy(int id, out int index)
        {
            index = -1;
            if (0 == recordList.Count) return null;
            index = -1;
            var left = 0;
            var right = recordList.Count - 1;
            T ret = null;
            while (left <= right)
            {
                var mid = left + (right - left) / 2;
                var record = recordList[mid];
                if (id < record.id)
                {
                    right = mid - 1;
                }
                else if (id > record.id)
                {
                    left = mid + 1;
                }
                else
                {
                    index = mid;
                    ret = record;
                    break;
                }
            }

            return ret;
        }

        public T FindBy(KeySchema primaryKey)
        {
            int index;
            return FindBy(primaryKey, out index);
        }

        public T FindBy(KeySchema primaryKey, out int index)
        {
            index = -1;
            if (0 == recordList.Count) return null;
            var id = primaryKey.GetId();
            var record = FindBy(id, out index);
            if (null != record && record.primaryKey == primaryKey) return record;
            return null;
        }

        public List<T> FindBy(BaseExpression condition, BaseExpression order = null, BaseExpression limit = null)
        {
            var ret = new List<T>();
            if (0 == recordList.Count) return ret;
            var conditionList = new List<ConditionExpression>();
            if (condition is AndExpression || condition is OrExpression)
            {
                var mcexp = condition as MultiConditionExpression;
                conditionList = mcexp.conditionList;
            }
            else
            {
                var cexp = condition as ConditionExpression;
                conditionList.Add(cexp);
            }

            foreach (var record in recordList)
            {
                var add = false;
                foreach (var cexp in conditionList)
                {
                    var pinfo = record.GetType().GetProperty(cexp.fieldName);
                    var value = pinfo.GetValue(record, null);
                    if (cexp.comparisonOperator.Equals("=="))
                        add = cexp.field.Equal(value);
                    else if (cexp.comparisonOperator.Equals("!="))
                        add = !cexp.field.Equal(value);
                    else if (cexp.comparisonOperator.Equals(">"))
                        add = cexp.field.MoreThan(value);
                    else if (cexp.comparisonOperator.Equals(">="))
                        add = cexp.field.MoreThanEqual(value);
                    else if (cexp.comparisonOperator.Equals("<"))
                        add = cexp.field.LessThan(value);
                    else if (cexp.comparisonOperator.Equals("<=")) add = cexp.field.LessThanEqual(value);
                    if (condition is AndExpression && false == add) break;

                    if (condition is OrExpression && add) break;
                }

                if (add) ret.Add(record);
            }

            if (1 < ret.Count)
            {
                if (null != order && order is OrderByExpression)
                {
                    var orderExpression = order as OrderByExpression;
                    ret = OrderBy(orderExpression.orderFieldName, ret, orderExpression.orderType);
                }

                if (null != limit && limit is LimitExpression)
                {
                    var limitExpression = limit as LimitExpression;
                    ret = Limit(ret, limitExpression.limit);
                }
            }

            return ret;
        }

        public List<T> FindAll()
        {
            return recordList;
        }

        public List<T> FindAll(BaseExpression expression)
        {
            if (null == expression) return recordList;
            var ret = new List<T>(recordList);
            if (expression is OrderByExpression)
            {
                var orderByExpression = expression as OrderByExpression;
                return OrderBy(orderByExpression.orderFieldName, ret, orderByExpression.orderType);
            }

            if (expression is LimitExpression)
            {
                var limitExpression = expression as LimitExpression;
                return Limit(ret, limitExpression.limit);
            }

            return ret;
        }

        public List<T> FindAll(OrderByExpression orderBy, LimitExpression limit)
        {
            var ret = new List<T>(recordList);
            if (null != orderBy) ret = OrderBy(orderBy.orderFieldName, ret, orderBy.orderType);
            if (null != limit) ret = Limit(ret, limit.limit);
            return ret;
        }

        public T FindFirst()
        {
            if (0 == recordList.Count) return null;
            return recordList[0];
        }

        public T FindLast()
        {
            if (0 == recordList.Count) return null;
            var lastIdx = recordList.Count - 1;
            return recordList[lastIdx];
        }

        public T FindNext()
        {
            seek++;
            var lastIdx = recordList.Count;
            if (seek >= lastIdx && 0 == recordList.Count)
            {
                seek = lastIdx - 1;
                return null;
            }

            return recordList[seek];
        }

        public T FindPrevious()
        {
            seek--;
            if (seek < 0 && 0 == recordList.Count)
            {
                seek = 0;
                return null;
            }

            return recordList[seek];
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
            if (null != FindBy(record.primaryKey)) return false;
            id++;
            record.id = id;
            record.Build();
            recordList.Add(record);
            if (null != assembler) assembler.WriteToPlayerPrefs(recordList);
            return true;
        }

        public bool Update(int id, T record)
        {
            int index;
            if (null == FindBy(id, out index)) return false;
            recordList[index] = record;
            if (null != assembler) assembler.WriteToPlayerPrefs(recordList);
            return true;
        }

        public bool Update(T record)
        {
            int index;
            if (null == FindBy(record.primaryKey, out index)) return false;
            recordList[index] = record;
            if (null != assembler) assembler.WriteToPlayerPrefs(recordList);
            return true;
        }

        public bool Remove(int id)
        {
            int index;
            if (null == FindBy(id, out index)) return false;
            recordList.RemoveAt(index);
            if (null != assembler) assembler.WriteToPlayerPrefs(recordList);
            return true;
        }

        public bool Remove(T record)
        {
            int index;
            if (null == FindBy(record.primaryKey, out index)) return false;
            recordList.RemoveAt(index);
            if (null != assembler) assembler.WriteToPlayerPrefs(recordList);
            return true;
        }

        private List<T> OrderBy(string orderFieldName, List<T> recordList, OrderByExpression.OrderType orderType)
        {
            for (var i = 0; i < recordList.Count; i++)
            for (var j = recordList.Count - 1; j > i; j--)
            {
                var previousIndex = j - 1;
                var recordA = recordList[j];
                var recordB = recordList[previousIndex];
                var pinfoA = recordA.GetType().GetProperty(orderFieldName);
                var pinfoB = recordB.GetType().GetProperty(orderFieldName);
                var fieldScheamA = recordA.fieldSchemaCollection.Get(orderFieldName);
                var fieldScheamB = recordB.fieldSchemaCollection.Get(orderFieldName);
                var valueA = pinfoA.GetValue(recordA, null);
                var valueB = pinfoB.GetValue(recordB, null);
                if (orderType == OrderByExpression.OrderType.Asc && fieldScheamA.MoreThan(valueB))
                {
                    var table = recordList[previousIndex];
                    recordList[previousIndex] = recordList[j];
                    recordList[j] = table;
                }
                else if (orderType == OrderByExpression.OrderType.Desc && fieldScheamB.MoreThan(valueA))
                {
                    var table = recordList[previousIndex];
                    recordList[previousIndex] = recordList[j];
                    recordList[j] = table;
                }
            }

            return recordList;
        }

        public List<T> Limit(List<T> originRcordList, int limit)
        {
            if (limit < originRcordList.Count)
            {
                var ret = new List<T>();
                var index = 0;
                foreach (var record in originRcordList)
                {
                    if (limit == index) break;
                    ret.Add(record);
                    index++;
                }

                return ret;
            }

            return originRcordList;
        }

        public override void Clear()
        {
            recordList.Clear();
            if (null != assembler) assembler.WriteToPlayerPrefs(recordList);
            id = 0;
            seek = 0;
        }

        public void Reset()
        {
            seek = 0;
        }

        public void Reset(int rollBackId)
        {
            id = rollBackId;
        }
    }
}