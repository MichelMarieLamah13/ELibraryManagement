using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ELibraryManagement.Connexion
{
    public class SQLServer
    {
        private static SqlConnection conn;
        private SQLServer() {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            conn = new SqlConnection(strcon);
        }
        public static SqlConnection getConnection() {
            if (conn == null) {
                new SQLServer();
            }
            return conn;
        }

    }
}