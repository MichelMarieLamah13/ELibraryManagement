using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using ELibraryManagement.Beans;
using ELibraryManagement.Dao;

namespace ELibraryManagement.DaoImplements
{
    public class AdminDAO : DAO<Admin>
    {
        public AdminDAO(SqlConnection conn) : base(conn) { 
        
        }
        public override bool create(Admin obj)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO admin_login_tbl(username,password,full_name) " +
                    "values(@username,@password,@full_name)", conn);
                cmd.Parameters.AddWithValue("@username", obj.username);
                string pwd = getHashValue(obj.password);
                cmd.Parameters.AddWithValue("@password", pwd);
                cmd.Parameters.AddWithValue("@full_name", obj.fullname);
                int result = cmd.ExecuteNonQuery();
                return result > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override bool delete(Admin obj)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Delete from admin_login_tbl WHERE username='" + obj.username + "'", conn);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override Admin find(Object obj)
        {
            Admin o = (Admin)obj;
            Admin a = new Admin();
            try {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT username,password,full_name from admin_login_tbl WHERE username=@username AND password=@password",conn);
                cmd.Parameters.AddWithValue("@username", o.username);
                string pwd = getHashValue(o.password);
                cmd.Parameters.AddWithValue("@password", pwd);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            a.fullname = rdr["full_name"].ToString();
                            a.password = rdr["password"].ToString();
                            a.username = rdr["username"].ToString();
                        }
                        return a;
                    }
                }

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public override bool update(Admin obj)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("UPDATE admin_login_tbl " +
                    "SET username=@username,password=@password WHERE full_name=@full_name", conn);
                cmd.Parameters.AddWithValue("@username", obj.username);
                string pwd = getHashValue(obj.password);
                cmd.Parameters.AddWithValue("@password", pwd);
                cmd.Parameters.AddWithValue("@full_name", obj.fullname);
                int result = cmd.ExecuteNonQuery();
                return result > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public string getHashValue(string value, string algorithm = "SHA1")
        {
            byte[] b = System.Text.Encoding.UTF8.GetBytes(value);
            if (algorithm == "SHA1")
            {
                using (SHA256Managed sha1 = new SHA256Managed())
                {
                    var hash = sha1.ComputeHash(b);
                    return Convert.ToBase64String(hash);
                }

            }
            else if (algorithm == "MD5")
            {
                using (MD5 md5 = MD5.Create())
                {
                    var hash = md5.ComputeHash(b);
                    return Convert.ToBase64String(hash);
                }
            }
            return "";
        }
    }
}