using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ELibraryManagement.Connexion;

namespace ELibraryManagement.Beans
{
    public class City
    {
        public String city_id { get; set; }
        public String city_name { get; set; }
        public String state_name { get; set; }

        public City(string city_id, string city_name, string state_name)
        {
            this.city_id = city_id;
            this.city_name = city_name;
            this.state_name = state_name;
        }

        public City()
        {

        }

        public static List<City> getByState(String state) {
            List<City> lst=new List<City>();
            try
            {
                SqlConnection con = SQLServer.getConnection();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from city_tbl WHERE state_name=@state_name;", con);
                cmd.Parameters.AddWithValue("@state_name", state);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            City c = new City();
                            c.city_id = rdr["city_id"].ToString();
                            c.city_name = rdr["city_name"].ToString();
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