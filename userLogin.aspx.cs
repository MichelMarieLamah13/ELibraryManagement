using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ELibraryManagement.Dao;

namespace ELibraryManagement
{
    public partial class userlogin : System.Web.UI.Page
    {
        public Member m { get; set; }
        public string txtClasse { get; set; }
        public List<string> lstMsg { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            m = new Member();
            txtClasse = "";
            lstMsg = new List<string>();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkFields())
            {
                signInMember();
            }
        }
        public void signInMember()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Member> memberDAO = df.getMemberDAO();
            Member x = memberDAO.find(m);
            if (x != null)
            {
                Session["username"] = x.member_id;
                Session["fullname"] = x.fullname;
                Session["role"] = "user";
                Session["status"] = x.account_status;
                Response.Redirect("homepage.aspx");
            }
            else {
                lstMsg.Add("Invalid credentials");
                txtClasse = "danger";
            }
        }
        public bool checkFields()
        {
            getValues();
            lstMsg.Clear();
            txtClasse = "";

            if (m.password.Length < 6)
            {
                lstMsg.Add("Password must be at least 6 characters");
            }

            if (m.member_id.Length == 0)
            {
                lstMsg.Add("MemberId field is empty");
            }

            if (lstMsg.Count() > 0)
            {
                txtClasse = "danger";
            }
            return lstMsg.Count == 0;
        }
        public void getValues()
        {          
            m.member_id = TextBox1.Text.Trim();
            m.password = TextBox2.Text.Trim();
        }
    }
}