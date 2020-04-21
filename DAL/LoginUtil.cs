using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WorkFlowManagement.DAL
{
    public class LoginUtil
    {
        private readonly SqlConnection Conn = new SqlConnection($"Data Source=localhost;Initial Catalog={AppInfo.DbName};Integrated Security=True");


        public bool Login(string UserName, string password, string Table)
        {
            bool result = false;
            try
            {
                string query = $"SELECT * FROM {Table} WHERE username = @username AND Password = @password";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("username", UserName));
                cmd.Parameters.Add(new SqlParameter("password", password));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    result = true;
                    if (Table == "principal")
                    {
                        HttpContext.Current.Session["PrincipalID"] = dt.Rows[0]["id"];
                    }
                    else if (Table == "hod")
                    {
                        HttpContext.Current.Session["HodID"] = dt.Rows[0]["hodid"];
                    }
                    else if (Table == "faculty")
                    {
                        HttpContext.Current.Session["FacultyID"] = dt.Rows[0]["facultyid"];
                    }
                    
                    HttpContext.Current.Session["Name"] = dt.Rows[0]["name"];
                }
            }
            catch (Exception ex)
            {
            }
            return result;

        }
    }
}