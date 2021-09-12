using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibraryManagement
{
    public class Member
    {
        public String member_id { get; set; }
        public String fullname { get; set; }
        public String dob { get; set; }
        public String contact_no { get; set; }
        public String email { get; set; }
        public String state { get; set; }
        public String city { get; set; }
        public String pincode { get; set; }
        public String fulladdress { get; set; }
        public String password { get; set; }
        public String account_status { get; set; }

        public Member(string member_id, string fullname, string dob, string contact_no, string email, string state, string city, string pincode, string fulladdress, string password, string account_status)
        {
            this.member_id = member_id;
            this.fullname = fullname;
            this.dob = dob;
            this.contact_no = contact_no;
            this.email = email;
            this.state = state;
            this.city = city;
            this.pincode = pincode;
            this.fulladdress = fulladdress;
            this.password = password;
            this.account_status = account_status;
        }

        public Member()
        {
        }
    }
}