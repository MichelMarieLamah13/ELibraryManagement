using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ELibraryManagement.Beans;
using ELibraryManagement.Dao;

namespace ELibraryManagement
{
    public partial class adminlogin : System.Web.UI.Page
    {
        public Admin a { get; set; }
        public string txtClasse { get; set; }
        public List<string> lstMsg { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            a = new Admin();
            txtClasse = "";
            lstMsg = new List<string>();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkFields())
            {
                signInAdmin();
            }

        }
        public void signInAdmin()
        {

            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Admin> adminDAO = df.getAdminDAO();
            Admin x = adminDAO.find(a);
            if (x != null)
            {
                Session["username"] = x.username;
                Session["fullname"] = x.fullname;
                Session["role"] = "admin";
                Response.Redirect("homepage.aspx");
            }
            else
            {
                lstMsg.Add("Invalid credentials");
                txtClasse = "danger";
            }
        }
        public bool checkFields()
        {
            getValues();
            lstMsg.Clear();
            txtClasse = "";

            if (a.password.Length < 6)
            {
                lstMsg.Add("Password must be at least 6 characters");
            }

            if (a.username.Length == 0)
            {
                lstMsg.Add("Username field is empty");
            }

            if (lstMsg.Count() > 0)
            {
                txtClasse = "danger";
            }
            return lstMsg.Count == 0;
        }
        public void getValues()
        {
            a.username = TextBox1.Text.Trim();
            a.password = TextBox2.Text.Trim();
        }
    }
}