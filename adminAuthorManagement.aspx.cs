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
    public partial class adminAuthorManagement : System.Web.UI.Page
    {
        public Author a { get; set; }
        public string txtClasse { get; set; }
        public List<string> lstMsg { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
            a = new Author();
            txtClasse = "";
            lstMsg = new List<string>();
        }
        // user defined function
        void getAuthorByID()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Author> authorDAO = df.getAuthorDAO();
            Author x = authorDAO.find(a);
            if (x != null) {
                TextBox2.Text = x.author_name;
            }
            else
            {
                lstMsg.Add("This author doesn't exist");
                txtClasse = "danger";
            }

        }


        void deleteAuthor()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Author> authorDAO = df.getAuthorDAO();
            Author x = authorDAO.find(a);
            if (x != null)
            {
                if (authorDAO.delete(a))
                {
                    lstMsg.Add("Author deleted successfully ");
                    txtClasse = "success";
                    clearForm();
                    GridView1.DataBind();
                }
                else
                {
                    lstMsg.Add("Delete operation in database failed");
                    txtClasse = "warning";
                }
            }
            else
            {
                lstMsg.Add("This member doesn't exist");
                txtClasse = "danger";
            }
        }

        void updateAuthor()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Author> authorDAO = df.getAuthorDAO();
            Author x = authorDAO.find(a);
            if (x != null)
            {
                if (authorDAO.update(a))
                {
                    lstMsg.Add("Author updated successfully ");
                    txtClasse = "success";
                    clearForm();
                    GridView1.DataBind();
                }
                else
                {
                    lstMsg.Add("Update operation in database failed");
                    txtClasse = "warning";
                }
            }
            else
            {
                lstMsg.Add("This member doesn't exist");
                txtClasse = "danger";
            }
           
        }


        void addNewAuthor()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Author> authorDAO = df.getAuthorDAO();
            Author x = authorDAO.find(a);
            if (x == null)
            {
                if (authorDAO.create(a))
                {
                    lstMsg.Add("Author added successfully ");
                    txtClasse = "success";
                    clearForm();
                    GridView1.DataBind();
                }
                else
                {
                    lstMsg.Add("Add operation in database failed");
                    txtClasse = "warning";
                }
            }
            else
            {
                lstMsg.Add("This member already exist");
                txtClasse = "danger";
            }
        }



       

        void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
        }

        // add button click
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkFields()) {
                addNewAuthor();
            }
        }
        // update button click
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkFields())
            {
                updateAuthor();

            }
           
        }

        // delete button click
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkFields())
            {
                deleteAuthor();
            }
        }

        // GO button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkFieldId()) {
                getAuthorByID();
            }
          
        }
        public bool checkFieldId()
        {
            getValues();
            lstMsg.Clear();
            txtClasse = "";
            if (a.author_id.Length == 0)
            {
                lstMsg.Add("Empty field author id");
            }
            if (lstMsg.Count > 0)
            {
                txtClasse = "danger";
            }
            return lstMsg.Count == 0;
        }
        public bool checkFields()
        {
            getValues();
            lstMsg.Clear();
            txtClasse = "";
            if (a.author_id.Length == 0)
            {
                lstMsg.Add("Empty field author id");
            }
            if (a.author_name.Length == 0)
            {
                lstMsg.Add("Empty field author name");
            }
            if (lstMsg.Count > 0)
            {
                txtClasse = "danger";
            }
            return lstMsg.Count == 0;
        }
        public void getValues()
        {
            a.author_id = TextBox1.Text.Trim();
            a.author_name = TextBox2.Text.Trim();
        }
    }
}