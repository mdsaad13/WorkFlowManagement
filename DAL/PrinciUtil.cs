using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WorkFlowManagement.Models;

namespace WorkFlowManagement.DAL
{
    public class PrinciUtil
    {
        private readonly SqlConnection Conn = new SqlConnection($"Data Source=localhost;Initial Catalog={AppInfo.DbName};Integrated Security=True");

        /* Departments operations starts here */
        internal bool AddDept(Departments model)
        {
            bool result = false;
            try
            {
                string query = "INSERT INTO departments (name) VALUES(@name)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("name", model.Name));

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

        internal List<Departments> AllDepts()
        {
            DataTable td = new DataTable();
            List<Departments> list = new List<Departments>();
            try
            {
                string sqlquery = "SELECT * FROM departments ORDER BY name ASC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    Departments obj = new Departments
                    {
                        ID = Convert.ToInt32(row["deptid"]),
                        Name = Convert.ToString(row["name"])
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

        internal Departments GetDeptByID(int id)
        {
            DataTable td = new DataTable();
            Departments obj = new Departments();
            try
            {
                string sqlquery = "SELECT * FROM departments where deptid = @deptid";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("deptid", id));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                Conn.Open();

                adp.Fill(td);

                obj.ID = Convert.ToInt32(td.Rows[0]["deptid"]);
                obj.Name = Convert.ToString(td.Rows[0]["name"]);

            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return obj;

        }

        internal bool UpdateDept(Departments model)
        {
            bool result = false;
            try
            {
                string query = "UPDATE departments SET name= @name WHERE deptid = @deptid";

                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("name", model.Name));
                cmd.Parameters.Add(new SqlParameter("deptid", model.ID));

                Conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Conn.Close();
            }
            return result;
        }
        /* Departments operations ends here */

        /* HOD operations starts here */
        internal bool AddHod(Hod model)
        {
            bool result = false;
            try
            {
                string query = "INSERT INTO hod (name, email, mobile, deptid, subject, education, experience, imgurl, password) VALUES(@name, @email, @mobile, @deptid, @subject, @education, @experience, @imgurl, @password)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("name", model.Name));
                cmd.Parameters.Add(new SqlParameter("email", model.Email));
                cmd.Parameters.Add(new SqlParameter("mobile", model.Mobile));
                cmd.Parameters.Add(new SqlParameter("deptid", model.DeptID));
                cmd.Parameters.Add(new SqlParameter("subject", model.Subject));
                cmd.Parameters.Add(new SqlParameter("education", model.Education));
                cmd.Parameters.Add(new SqlParameter("experience", model.Experience));
                cmd.Parameters.Add(new SqlParameter("imgurl", model.ImgUrl));
                cmd.Parameters.Add(new SqlParameter("password", model.Password));

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

        internal List<Hod> AllHods()
        {
            DataTable td = new DataTable();
            List<Hod> list = new List<Hod>();
            try
            {
                string sqlquery = "SELECT * FROM hod ORDER BY hodid DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                Conn.Close();
                foreach (DataRow row in td.Rows)
                {
                    Hod obj = new Hod
                    {
                        ID = Convert.ToInt32(row["hodid"]),
                        Name = Convert.ToString(row["name"]),
                        Email = Convert.ToString(row["email"]),
                        Mobile = Convert.ToString(row["mobile"]),
                        DeptID = Convert.ToInt32(row["deptid"]),
                        Subject = Convert.ToString(row["subject"]),
                        Education = Convert.ToString(row["education"]),
                        Experience = Convert.ToString(row["experience"]),
                        ImgUrl = Convert.ToString(row["imgurl"]),
                        DeptName = GetDeptByID(Convert.ToInt32(row["deptid"])).Name
                    };

                    list.Add(obj);
                }
            }
            catch (Exception)
            { }
            return list;
        }

        internal Hod GetHodByID(int id)
        {
            DataTable td = new DataTable();
            Hod obj = new Hod();
            try
            {
                string sqlquery = "SELECT * FROM hod where hodid = @hodid";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("hodid", id));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                Conn.Open();

                adp.Fill(td);

                Conn.Close();

                obj.ID = Convert.ToInt32(td.Rows[0]["hodid"]);
                obj.Name = Convert.ToString(td.Rows[0]["name"]);
                obj.Email = Convert.ToString(td.Rows[0]["email"]);
                obj.Mobile = Convert.ToString(td.Rows[0]["mobile"]);
                obj.DeptID = Convert.ToInt32(td.Rows[0]["deptid"]);
                obj.Subject = Convert.ToString(td.Rows[0]["subject"]);
                obj.Education = Convert.ToString(td.Rows[0]["education"]);
                obj.Experience = Convert.ToString(td.Rows[0]["experience"]);
                obj.ImgUrl = Convert.ToString(td.Rows[0]["imgurl"]);
                obj.DeptName = GetDeptByID(Convert.ToInt32(td.Rows[0]["deptid"])).Name;

            }
            catch (Exception)
            { }
            return obj;

        }

        internal bool UpdateHod(Hod model)
        {
            bool result = false;
            try
            {
                string query;

                if (model.ImgUrl == "noupdate")
                {
                    query = "UPDATE hod SET name = @name, email = @email, mobile = @mobile, deptid = @deptid, subject = @subject, education = @education, experience = @experience WHERE hodid = @id";
                }
                else
                {
                    query = "UPDATE hod SET name = @name, email = @email, mobile = @mobile, deptid = @deptid, subject = @subject, education = @education, experience = @experience, imgurl = @imgurl WHERE hodid = @id";
                }

                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("name", model.Name));
                cmd.Parameters.Add(new SqlParameter("email", model.Email));
                cmd.Parameters.Add(new SqlParameter("mobile", model.Mobile));
                cmd.Parameters.Add(new SqlParameter("deptid", model.DeptID));
                cmd.Parameters.Add(new SqlParameter("subject", model.Subject));
                cmd.Parameters.Add(new SqlParameter("education", model.Education));
                cmd.Parameters.Add(new SqlParameter("experience", model.Experience));
                cmd.Parameters.Add(new SqlParameter("imgurl", model.ImgUrl));

                cmd.Parameters.Add(new SqlParameter("id", model.ID));

                Conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Conn.Close();
            }
            return result;
        }

        internal void DeleteHod(int ID)
        {
            try
            {
                string query = "DELETE from hod where hodid = @hodid";
                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("hodid", ID));
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
        /* HOD operations ends here */

        /* Faculty operations starts here */
        internal bool AddFaculty(Faculty model)
        {
            bool result = false;
            try
            {
                string query = "INSERT INTO faculty (name, email, mobile, deptid, subject, education, experience, imgurl, password) VALUES(@name, @email, @mobile, @deptid, @subject, @education, @experience, @imgurl, @password)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("name", model.Name));
                cmd.Parameters.Add(new SqlParameter("email", model.Email));
                cmd.Parameters.Add(new SqlParameter("mobile", model.Mobile));
                cmd.Parameters.Add(new SqlParameter("deptid", model.DeptID));
                cmd.Parameters.Add(new SqlParameter("subject", model.Subject));
                cmd.Parameters.Add(new SqlParameter("education", model.Education));
                cmd.Parameters.Add(new SqlParameter("experience", model.Experience));
                cmd.Parameters.Add(new SqlParameter("imgurl", model.ImgUrl));
                cmd.Parameters.Add(new SqlParameter("password", model.Password));

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

        internal List<Faculty> AllFaculty()
        {
            DataTable td = new DataTable();
            List<Faculty> list = new List<Faculty>();
            try
            {
                string sqlquery = "SELECT * FROM faculty ORDER BY facultyid DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                Conn.Close();
                foreach (DataRow row in td.Rows)
                {
                    Faculty obj = new Faculty
                    {
                        ID = Convert.ToInt32(row["facultyid"]),
                        Name = Convert.ToString(row["name"]),
                        Email = Convert.ToString(row["email"]),
                        Mobile = Convert.ToString(row["mobile"]),
                        DeptID = Convert.ToInt32(row["deptid"]),
                        Subject = Convert.ToString(row["subject"]),
                        Education = Convert.ToString(row["education"]),
                        Experience = Convert.ToString(row["experience"]),
                        ImgUrl = Convert.ToString(row["imgurl"]),
                        DeptName = GetDeptByID(Convert.ToInt32(row["deptid"])).Name
                    };

                    list.Add(obj);
                }
            }
            catch (Exception)
            { }
            return list;
        }

        internal Faculty GetFacultyByID(int id)
        {
            DataTable td = new DataTable();
            Faculty obj = new Faculty();
            try
            {
                string sqlquery = "SELECT * FROM faculty where facultyid = @facultyid";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("facultyid", id));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                Conn.Open();

                adp.Fill(td);

                Conn.Close();

                obj.ID = Convert.ToInt32(td.Rows[0]["facultyid"]);
                obj.Name = Convert.ToString(td.Rows[0]["name"]);
                obj.Email = Convert.ToString(td.Rows[0]["email"]);
                obj.Mobile = Convert.ToString(td.Rows[0]["mobile"]);
                obj.DeptID = Convert.ToInt32(td.Rows[0]["deptid"]);
                obj.Subject = Convert.ToString(td.Rows[0]["subject"]);
                obj.Education = Convert.ToString(td.Rows[0]["education"]);
                obj.Experience = Convert.ToString(td.Rows[0]["experience"]);
                obj.ImgUrl = Convert.ToString(td.Rows[0]["imgurl"]);
                obj.DeptName = GetDeptByID(Convert.ToInt32(td.Rows[0]["deptid"])).Name;

            }
            catch (Exception)
            { }
            return obj;

        }

        internal bool UpdateFaculty(Faculty model)
        {
            bool result = false;
            try
            {
                string query;

                if (model.ImgUrl == "noupdate")
                {
                    query = "UPDATE faculty SET name = @name, email = @email, mobile = @mobile, deptid = @deptid, subject = @subject, education = @education, experience = @experience WHERE facultyid = @id";
                }
                else
                {
                    query = "UPDATE faculty SET name = @name, email = @email, mobile = @mobile, deptid = @deptid, subject = @subject, education = @education, experience = @experience, imgurl = @imgurl WHERE facultyid = @id";
                }

                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("name", model.Name));
                cmd.Parameters.Add(new SqlParameter("email", model.Email));
                cmd.Parameters.Add(new SqlParameter("mobile", model.Mobile));
                cmd.Parameters.Add(new SqlParameter("deptid", model.DeptID));
                cmd.Parameters.Add(new SqlParameter("subject", model.Subject));
                cmd.Parameters.Add(new SqlParameter("education", model.Education));
                cmd.Parameters.Add(new SqlParameter("experience", model.Experience));
                cmd.Parameters.Add(new SqlParameter("imgurl", model.ImgUrl));

                cmd.Parameters.Add(new SqlParameter("id", model.ID));

                Conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Conn.Close();
            }
            return result;
        }

        internal void DeleteFaculty(int ID)
        {
            try
            {
                string query = "DELETE from faculty where facultyid = @facultyid";
                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("facultyid", ID));
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
        /* Faculty operations ends here */

    }
}