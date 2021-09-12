using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibraryManagement.Beans
{
    public class Issue
    {
        public String member_id { get; set; }
        public String member_name { get; set; }
        public String book_id { get; set; }
        public String book_name { get; set; }
        public String issue_date { get; set; }
        public String due_date { get; set; }

        public Issue(string member_id, string member_name, string book_id, string book_name, string issue_date, string due_date)
        {
            this.member_id = member_id;
            this.member_name = member_name;
            this.book_id = book_id;
            this.book_name = book_name;
            this.issue_date = issue_date;
            this.due_date = due_date;
        }

        public Issue()
        {
        }
    }
}