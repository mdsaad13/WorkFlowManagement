using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WorkFlowManagement.Models;

namespace WorkFlowManagement.DAL
{
    public class Common
    {
        private readonly SqlConnection Conn = new SqlConnection($"Data Source=localhost;Initial Catalog={AppInfo.DbName};Integrated Security=True");

        internal List<NoticeBoard> NoticeBoard(bool All = false)
        {
            DataTable td = new DataTable();
            List<NoticeBoard> list = new List<NoticeBoard>();
            try
            {
                string Top5 = string.Empty;
                if (!All)
                {
                    Top5 = "TOP 5";
                }
                string sqlquery = $"SELECT {Top5} * FROM noticeboard ORDER BY datetime DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    NoticeBoard obj = new NoticeBoard
                    {
                        ID = Convert.ToInt32(row["noticeid"]),
                        Title = Convert.ToString(row["title"]),
                        Description = Convert.ToString(row["description"]),
                        Date = Convert.ToDateTime(row["datetime"])
                    };

                    list.Add(obj);
                }
            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return list;
        }

        internal bool AddNoticeBoard(NoticeBoard model)
        {
            bool result = false;
            try
            {
                string query = "INSERT INTO noticeboard (title, description, datetime) VALUES(@title, @description, @datetime)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("title", model.Title));
                cmd.Parameters.Add(new SqlParameter("description", model.Description));
                cmd.Parameters.Add(new SqlParameter("datetime", model.Date));

                Conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    result = true;
                }
            }
            catch (Exception exp)
            {

            }
            finally
            {
                Conn.Close();
            }
            return result;
        }

        internal void DeleteNoticeBoard(int ID)
        {
            try
            {
                string query = "DELETE from noticeboard where noticeid = @noticeid";
                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("noticeid", ID));
                Conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            { }
            finally
            {
                Conn.Close();
            }
        }

        /* Meetings operations starts here */
        internal List<Meetings> Meetings(bool All = false)
        {
            DataTable td = new DataTable();
            List<Meetings> list = new List<Meetings>();
            PrinciUtil princiUtil = new PrinciUtil();
            try
            {
                string Top5 = string.Empty;
                if (!All)
                {
                    Top5 = "TOP 5";
                }
                string sqlquery = $"SELECT {Top5} * FROM meetings ORDER BY date DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                Conn.Close();
                foreach (DataRow row in td.Rows)
                {
                    Meetings obj = new Meetings
                    {
                        ID = Convert.ToInt32(row["meetingid"]),
                        DeptID = Convert.ToInt32(row["deptid"]),
                        AddedBy = Convert.ToString(row["addedby"]),
                        Title = Convert.ToString(row["title"]),
                        DateOfMeeting = Convert.ToDateTime(row["date"]),
                        TimeOfMeeting = Convert.ToString(row["time"]),
                        Place = Convert.ToString(row["place"]),
                        Description = Convert.ToString(row["description"]),
                        DeptName = princiUtil.GetDeptByID(Convert.ToInt32(row["deptid"])).Name
                    };

                    list.Add(obj);
                }
            }
            catch (Exception)
            { }
            return list;
        }

        internal bool AddMeeting(Meetings model)
        {
            bool result = false;
            try
            {
                string query = "INSERT INTO meetings (deptid, addedby, title, date, time, place, description) VALUES(@deptid, @addedby, @title, @date, @time, @place, @description)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("deptid", model.DeptID));
                cmd.Parameters.Add(new SqlParameter("addedby", model.AddedBy));
                cmd.Parameters.Add(new SqlParameter("title", model.Title));
                cmd.Parameters.Add(new SqlParameter("date", model.DateOfMeeting));
                cmd.Parameters.Add(new SqlParameter("time", model.TimeOfMeeting));
                cmd.Parameters.Add(new SqlParameter("place", model.Place));
                cmd.Parameters.Add(new SqlParameter("description", model.Description));

                Conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    result = true;
                }
            }
            catch (Exception exp)
            {

            }
            finally
            {
                Conn.Close();
            }
            return result;
        }

        internal void DeleteMeeting(int ID)
        {
            try
            {
                string query = "DELETE from meetings where meetingid = @meetingid";
                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("meetingid", ID));
                Conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            { }
            finally
            {
                Conn.Close();
            }
        }
    }
}