using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WorkFlowManagement.DAL
{
    public class GeneralUtil
    {
        /// <summary>
        /// <b>Connection String</b>
        /// </summary>
        private readonly SqlConnection Conn = new SqlConnection($"Data Source=localhost;Initial Catalog={AppInfo.DbName};Integrated Security=True");

        /// <summary>
        /// <b>param</b> <c>TableName</c><br></br>
        /// <b>return number of rows in table</b>
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public int Count(string TableName)
        {
            int count = 0;
            try
            {
                string query = "SELECT COUNT(*) FROM " + TableName;
                SqlCommand cmd = new SqlCommand(query, Conn);
                Conn.Open();
                count = Convert.ToInt32(cmd.ExecuteScalar());
                Conn.Close();
                return count;
            }
            catch
            {
                return count;
            }
        }

        /// <summary>
        /// <b>param</b> <c>TableName</c><br></br>
        /// <b>param</b> <c>args Eg: id = 1 AND name = somename</c><br></br>
        /// <b>return number of rows in table</b>
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public int CountByArgs(string TableName, string args)
        {
            int count = 0;
            try
            {
                string query = "SELECT COUNT(*) FROM " + TableName + " WHERE " + args;
                SqlCommand cmd = new SqlCommand(query, Conn);
                Conn.Open();
                count = Convert.ToInt32(cmd.ExecuteScalar());
                Conn.Close();
                return count;
            }
            catch
            {
                return count;
            }
        }

        public double Sum(string TableName, string Colname)
        {
            double count = 0;
            try
            {
                string query = "SELECT SUM(" + Colname + ") FROM " + TableName;
                SqlCommand cmd = new SqlCommand(query, Conn);
                Conn.Open();
                count = Convert.ToDouble(cmd.ExecuteScalar());
                Conn.Close();
                return count;
            }
            catch
            {
                return count;
            }
        }

        /// <summary>
        /// <b>param</b> <c>TableName</c><br></br>
        /// <b>param</b> <c>Colname</c><br></br>
        /// <b>param</b> <c>args Eg: id = 1 AND name = somename</c><br></br>
        /// <b>return sum</b>
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="Colname"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public double SumByArgs(string TableName, string Colname, string args)
        {
            //string[,] args = new string[,];

            double count = 0;
            try
            {
                string query;
                query = "SELECT SUM(" + Colname + ") FROM " + TableName + " WHERE " + args;

                SqlCommand cmd = new SqlCommand(query, Conn);
                Conn.Open();
                count = Convert.ToDouble(cmd.ExecuteScalar());
                Conn.Close();
                return count;
            }
            catch
            {
                return count;
            }
        }

        /// <summary>
        ///     <b>param</b> <c>table-name</c> <br></br>
        ///     <b>param</b> <c>column-name</c> <br></br>
        ///     <b>param</b> <c>field</c> to validate <br></br><br></br>
        ///     <b>return</b> true if found <br></br>
        ///     <b>return</b> false if not found <br></br>
        /// </summary>
        internal bool Validate(string TableName, string column, string field)
        {
            try
            {
                string query = "SELECT * FROM " + TableName + " WHERE " + column + " = @field";

                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("field", field));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    // Found in database
                    return true;
                }
                else
                {
                    // not found
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}