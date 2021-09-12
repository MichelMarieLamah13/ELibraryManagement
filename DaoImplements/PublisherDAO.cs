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
    public class PublisherDAO : DAO<Publisher>
    {
        public PublisherDAO(SqlConnection conn) : base(conn) { }
        public override bool create(Publisher obj)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO publisher_master_tbl(publisher_id,publisher_name) " +
                    "values(@publisher_id,@publisher_name)", conn);
                cmd.Parameters.AddWithValue("@publisher_id", obj.publisher_id);
                cmd.Parameters.AddWithValue("@publisher_name", obj.publisher_name);
                int result = cmd.ExecuteNonQuery();
                return result > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override bool delete(Publisher obj)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Delete from publisher_master_tbl WHERE publisher_id='" + obj.publisher_id + "'", conn);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override Publisher find(Object obj)
        {
            Publisher o = (Publisher)obj;
            Publisher p = new Publisher();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT publisher_id,publisher_name from publisher_master_tbl  where publisher_id=@publisher_id",conn);
                cmd.Parameters.AddWithValue("@publisher_id", o.publisher_id);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        if (rdr.Read())
                        {
                            p.publisher_id = rdr["publisher_id"].ToString();
                            p.publisher_name = rdr["publisher_name"].ToString();
                        }
                        return p;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public override bool update(Publisher obj)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("UPDATE publisher_master_tbl " +
                    "SET publisher_name=@publisher_name WHERE publisher_id=@publisher_id", conn);
                cmd.Parameters.AddWithValue("@publisher_name", obj.publisher_name);
                cmd.Parameters.AddWithValue("@publisher_id", obj.publisher_id);
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