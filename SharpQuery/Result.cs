using System;
using System.Data;
using System.Collections.Generic;

namespace SharpQuery
{
    public class Result
    {
        private DataRowCollection Data;
        public Dictionary<string, string>[] Rows;
        public int Count;

        public Result(DataTable tbl)
        {
            Data = tbl.Rows;

            Count = Data.Count;

            Rows = new Dictionary<string, string>[Count];

            for (int i = 0; i < Count; i++)
            {
                Rows[i] = new Dictionary<string, string>();

                foreach (string key in Keys(i))
                {
                    Rows[i].Add(key, ValueByKey(key, i));
                }
            }
        }

        private List<string> Keys(int index)
        {
            List<string> list = new List<string>();

            DataColumnCollection table = Data[index].Table.Columns;

            for (int i = 0; i < table.Count; i++)
            {
                list.Add(table[i].ColumnName);
            }

            return list;
        }

        private string ValueByKey(string name, int index)
        {
            string data = "";

            try
            {
                data = Data[index].Field<object>(name).ToString();
            }
            catch (Exception)
            {
            }


            return data;
        }

        public int Get(string key, int row)
        {
            return int.Parse(Rows[row][key]);
        }

        public int GetInt(string key, int index)
        {
            return int.Parse(Rows[index][key]);
        }

        public int GetInt(string key)
        {
            return GetInt(key, 0);
        }

        public string GetString(string key, int index)
        {
            return Rows[index][key];
        }

        public string GetString(string key)
        {
            return GetString(key, 0);
        }
    }
}
