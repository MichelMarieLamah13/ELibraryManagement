using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ELibraryManagement.Beans;
using ELibraryManagement.Dao;

namespace ELibraryManagement
{
    public partial class adminSignUp : System.Web.UI.Page
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
                signUpNewAdmin();
            }
        }
        public void signUpNewAdmin()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Admin> adminDAO = df.getAdminDAO();
            Admin x = adminDAO.find(a);
            if (x == null)
            {
                if (adminDAO.create(a))
                {
                    lstMsg.Add("Welcome Admin " + a.username + ". You can now login");
                    txtClasse = "success";
                }
                else
                {
                    lstMsg.Add("Add operation in database failed");
                    txtClasse = "warning";
                }
            }
            else
            {
                lstMsg.Add("This admin already exists");
                txtClasse = "warning";
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
            if (a.fullname.Length == 0)
            {
                lstMsg.Add("fullname field is empty");
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
            a.fullname = TextBox3.Text.Trim();
        }
    }
}