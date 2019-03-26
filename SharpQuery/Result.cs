using System;
using System.Data;
using System.Collections.Generic;

namespace SharpQuery
{
    public class Result
    {
        private DataRowCollection Rows;
        public Dictionary<string, string>[] Data;
        public int Count;

        public Result(DataTable tbl)
        {
            Rows = tbl.Rows;

            Count = Rows.Count;

            Data = new Dictionary<string, string>[Count];

            for (int i = 0; i < Count; i++)
            {
                Data[i] = new Dictionary<string, string>();

                foreach (string key in Keys(i))
                {
                    Data[i].Add(key, ValueByKey(key, i));
                }
            }
        }

        private List<string> Keys(int index)
        {
            List<string> list = new List<string>();

            DataColumnCollection table = Rows[index].Table.Columns;

            for (int i = 0; i < table.Count; i++)
            {
                list.Add(table[i].ColumnName);
            }

            return list;
        }

        public string ValueByKey(string name, int index)
        {
            string data = "";

            try
            {
                data = Rows[index].Field<object>(name).ToString();
            }
            catch (Exception)
            {
            }


            return data;
        }

        public object Get(string key, int index = 0)
        {
            try
            {
                return Data[index][key];
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
