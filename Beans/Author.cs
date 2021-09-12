using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ELibraryManagement.Connexion;

namespace ELibraryManagement
{
    public class Author
    {
        public String author_id { get; set; }
        public String author_name { get; set; }

        public Author(string author_id, string author_name)
        {
            this.author_id = author_id;
            this.author_name = author_name;
        }

        public Author()
        {
        }

        public static List<Author> getAll()
        {
            List<Author> lst = new List<Author>();
            try
            {
                SqlConnection con = SQLServer.getConnection();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from author_master_tbl;", con);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            Author p = new Author();
                            p.author_id = rdr["author_id"].ToString();
                            p.author_name = rdr["author_name"].ToString();
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