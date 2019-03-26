using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;

namespace SharpQuery
{
    public class Query
    {
        public string QueryString;
        public Dictionary<string, string> Args = new Dictionary<string, string>();
        public Result Result;

        public Query(string QueryString)
        {
            this.QueryString = QueryString;
        }

        public void Execute(MySqlConnection conn)
        {
            DataTable tbl = new DataTable();

            using (var da = new MySqlDataAdapter(QueryString, conn))
            {
                foreach (string key in Args.Keys)
                {
                    da.SelectCommand.Parameters.AddWithValue(key, Args[key]);
                }

                da.Fill(tbl);
            }

            Result = new Result(tbl);
        }
    }
}
