using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ELibraryManagement.Dao;

namespace ELibraryManagement.DaoImplements
{
    public class AuthorDAO : DAO<Author>
    {
        public AuthorDAO(SqlConnection conn) : base(conn) { }
        public override bool create(Author obj)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO author_master_tbl(author_id,author_name) " +
                    "values(@author_id,@author_name)", conn);
                cmd.Parameters.AddWithValue("@author_id", obj.author_id);
                cmd.Parameters.AddWithValue("@author_name", obj.author_name);
                int result = cmd.ExecuteNonQuery();
                return result > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override bool delete(Author obj)
        {

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Delete from author_master_tbl WHERE author_id='" + obj.author_id + "'", conn);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override Author find(Object obj)
        {
            Author o = (Author)obj;
            Author a = new Author();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT author_id,author_name from author_master_tbl  where author_id=@author_id",conn);
                cmd.Parameters.AddWithValue("@author_id", o.author_id);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            a.author_id = rdr["author_id"].ToString();
                            a.author_name = rdr["author_name"].ToString();
                        }
                        return a;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public override bool update(Author obj)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("UPDATE author_master_tbl " +
                    "SET author_name=@author_name WHERE author_id=@author_id", conn);
                cmd.Parameters.AddWithValue("@author_name", obj.author_name);
                cmd.Parameters.AddWithValue("@author_id", obj.author_id);
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