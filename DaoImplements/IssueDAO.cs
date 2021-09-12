using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ELibraryManagement.Beans;
using ELibraryManagement.Dao;

namespace ELibraryManagement.DaoImplements
{
    public class IssueDAO : DAO<Issue>
    {
        public IssueDAO(SqlConnection conn):base(conn) {
           
        }
        public override bool create(Issue obj)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO book_issue_tbl(member_id,member_name,book_id,book_name,issue_date,due_date) " +
                    "values(@member_id,@member_name,@book_id,@book_name,@issue_date,@due_date)", conn);
                cmd.Parameters.AddWithValue("@member_id", obj.member_id);
                cmd.Parameters.AddWithValue("@member_name", obj.member_name);
                cmd.Parameters.AddWithValue("@book_id", obj.book_id);
                cmd.Parameters.AddWithValue("@book_name", obj.book_name);
                cmd.Parameters.AddWithValue("@issue_date", obj.issue_date);
                cmd.Parameters.AddWithValue("@due_date", obj.due_date);
                int result = cmd.ExecuteNonQuery();
                return result > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override bool delete(Issue obj)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("DELETE FROM book_issue_tbl " +
                    "WHERE member_id=@member_id AND book_id=@book_id", conn);
                cmd.Parameters.AddWithValue("@member_id", obj.member_id);
                cmd.Parameters.AddWithValue("@book_id", obj.book_id);
                int result = cmd.ExecuteNonQuery();
                return result > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override Issue find(Object obj)
        {
            Issue o = (Issue)obj;
            Issue i = new Issue();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT member_id,member_name,book_id,book_name,issue_date,due_date FROM book_issue_tbl " +
                    "WHERE member_id=@member_id,book_id=@book_id", conn);
                cmd.Parameters.AddWithValue("@member_id", o.member_id);
                cmd.Parameters.AddWithValue("@book_id", o.book_id);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            i.member_id = rdr["member_id"].ToString();
                            i.member_name = rdr["member_name"].ToString();
                            i.book_id = rdr["book_id"].ToString();
                            i.book_name = rdr["book_name"].ToString();
                            i.issue_date = rdr["issue_date"].ToString();
                            i.due_date = rdr["due_date"].ToString();
                        }
                        return i;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               
            }
            return null;
        }

        public override bool update(Issue obj)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("UPDATE book_issue_tbl " +
                    "SET member_id=@member_id,member_name=@member_name,book_id=@book_id,book_name=@book_name,issue_date=@issue_date,due_date=@due_date" +
                    "WHERE member_id=@member_id,book_id=@book_id", conn);
                cmd.Parameters.AddWithValue("@member_id", obj.member_id);
                cmd.Parameters.AddWithValue("@member_name", obj.member_name);
                cmd.Parameters.AddWithValue("@book_id", obj.book_id);
                cmd.Parameters.AddWithValue("@book_name", obj.book_name);
                cmd.Parameters.AddWithValue("@issue_date", obj.issue_date);
                cmd.Parameters.AddWithValue("@due_date", obj.due_date);
                int result = cmd.ExecuteNonQuery();
                return result > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}