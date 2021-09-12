using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ELibraryManagement.Connexion;

namespace ELibraryManagement.Beans
{
    public class Publisher
    {
        public String publisher_id { get; set; }
        public String publisher_name { get; set; }

        public Publisher(string publisher_id, string publisher_name)
        {
            this.publisher_id = publisher_id;
            this.publisher_name = publisher_name;
        }

        public Publisher()
        {
        }

        public static List<Publisher> getAll()
        {
            List<Publisher> lst = new List<Publisher>();
            try
            {
                SqlConnection con = SQLServer.getConnection();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from publisher_master_tbl;", con);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            Publisher p = new Publisher();
                            p.publisher_id = rdr["publisher_id"].ToString();
                            p.publisher_name = rdr["publisher_name"].ToString();
                            lst.Add(p);
                        }
                        return lst;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return null;
        }
    }
}