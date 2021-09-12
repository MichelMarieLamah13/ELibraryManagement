using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibraryManagement.Beans
{
    public class Admin
    {
        public String username { get; set; }
        public String password { get; set; }
        public String fullname { get; set; }

        public Admin(string username, string password, string fullname)
        {
            this.username = username;
            this.password = password;
            this.fullname = fullname;
        }

        public Admin()
        {
        }
    }
}