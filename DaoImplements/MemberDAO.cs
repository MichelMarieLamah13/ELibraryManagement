using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using ELibraryManagement.Dao;

namespace ELibraryManagement.DaoImplements
{
    public class MemberDAO : DAO<Member>
    {
        public MemberDAO(SqlConnection conn) : base(conn) { }
        public override bool create(Member obj)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO member_master_tbl(full_name,dob,contact_no,email,state,city,pincode,full_address,member_id,password,account_status) " +
                    "values(@full_name,@dob,@contact_no,@email,@state,@city,@pincode,@full_address,@member_id,@password,@account_status)", conn);
                cmd.Parameters.AddWithValue("@full_name", obj.fullname);
                cmd.Parameters.AddWithValue("@dob", obj.dob);
                cmd.Parameters.AddWithValue("@contact_no", obj.contact_no);
                cmd.Parameters.AddWithValue("@email", obj.email);
                cmd.Parameters.AddWithValue("@state", obj.state);
                cmd.Parameters.AddWithValue("@city", obj.city);
                cmd.Parameters.AddWithValue("@pincode", obj.pincode);
                cmd.Parameters.AddWithValue("@full_address", obj.fulladdress);
                cmd.Parameters.AddWithValue("@member_id", obj.member_id);
                string pwd = getHashValue(obj.password);
                cmd.Parameters.AddWithValue("@password", pwd);
                cmd.Parameters.AddWithValue("@account_status", obj.account_status);
                int result = cmd.ExecuteNonQuery();
                return result > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override bool delete(Member obj)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Delete from member_master_tbl WHERE member_id=@member_id", conn);
                cmd.Parameters.AddWithValue("@member_id", obj.member_id);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override Member find(Object obj)
        {
            Member o = (Member)obj;
            try
            {
                Member m = new Member();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd;
                if (o.password == null)
                {
                    cmd = new SqlCommand("Select full_name,dob,contact_no,email,state,city,pincode,full_address,member_id,password,account_status from member_master_tbl WHERE member_id =@member_id;", conn);
                }
                else {
                    string pwd = getHashValue(o.password);
                    cmd = new SqlCommand("Select full_name,dob,contact_no,email,state,city,pincode,full_address,member_id,password,account_status from member_master_tbl WHERE member_id = @member_id AND password = @password;", conn);
                    cmd.Parameters.AddWithValue("@password", pwd);
                }
                cmd.Parameters.AddWithValue("@member_id", o.member_id);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            m.fullname = rdr["full_name"].ToString();
                            m.dob = rdr["dob"].ToString();
                            m.contact_no = rdr["contact_no"].ToString();
                            m.email = rdr["email"].ToString();
                            m.state = rdr["state"].ToString();
                            m.city = rdr["city"].ToString();
                            m.pincode = rdr["pincode"].ToString();
                            m.fulladdress = rdr["full_address"].ToString();
                            m.member_id = rdr["member_id"].ToString();
                            m.password = rdr["password"].ToString();
                            m.account_status = rdr["account_status"].ToString();
                        }

                        return m;
                    }

                }
                    
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public override bool update(Member obj)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("UPDATE member_master_tbl " +
                    "SET full_name=@full_name,dob=@dob,contact_no=@contact_no,email=@email,state=@state,city=@city,pincode=@pincode,full_address=@full_address,password=@password,account_status=@account_status " +
                    "WHERE member_id=@member_id", conn);
                cmd.Parameters.AddWithValue("@full_name", obj.fullname);
                cmd.Parameters.AddWithValue("@dob", obj.dob);
                cmd.Parameters.AddWithValue("@contact_no", obj.contact_no);
                cmd.Parameters.AddWithValue("@email", obj.email);
                cmd.Parameters.AddWithValue("@state", obj.state);
                cmd.Parameters.AddWithValue("@city", obj.city);
                cmd.Parameters.AddWithValue("@pincode", obj.pincode);
                cmd.Parameters.AddWithValue("@full_address", obj.fulladdress);
                string pwd = getHashValue(obj.password);
                cmd.Parameters.AddWithValue("@password", pwd);
                cmd.Parameters.AddWithValue("@account_status", obj.account_status);
                cmd.Parameters.AddWithValue("@member_id", obj.member_id);

                int result = cmd.ExecuteNonQuery();
                return result > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public string getHashValue(string value,string algorithm="SHA1") {
            byte[] b = System.Text.Encoding.UTF8.GetBytes(value);
            if (algorithm == "SHA1")
            {
                using (SHA256Managed sha1=new SHA256Managed()) {
                    var hash = sha1.ComputeHash(b);
                    return Convert.ToBase64String(hash);
                }
               
            }
            else if(algorithm=="MD5") {
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