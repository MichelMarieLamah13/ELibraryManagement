using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ELibraryManagement.Connexion;

namespace ELibraryManagement.Beans
{
    public class State
    {
        public String state_id { get; set; }
        public String state_name { get; set; }

        public State(string state_id, string state_name)
        {
            this.state_id = state_id;
            this.state_name = state_name;
        }

        public State()
        {
        }

        public static List<State> getAll()
        {
            List<State> lst = new List<State>();
            try
            {
                SqlConnection con = SQLServer.getConnection();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from state_tbl;", con);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            State c = new State();
                            c.state_id = rdr["state_id"].ToString();
                            c.state_name = rdr["state_name"].ToString();
                            lst.Add(c);
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