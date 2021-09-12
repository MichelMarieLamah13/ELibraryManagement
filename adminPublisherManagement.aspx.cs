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
    public partial class adminPublisherManagement : System.Web.UI.Page
    {
        public Publisher p { get; set; }
        public string txtClasse { get; set; }
        public List<string> lstMsg { get; set; }
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
            p = new Publisher();
            txtClasse = "";
            lstMsg = new List<string>();
        }

        // add publisher
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkFields())
            {
                addNewPublisher();
            }
        }

        // user defined functions

        public void getPublisherByID()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Publisher> publisherDAO = df.getPublisherDAO();
            Publisher x = publisherDAO.find(p);
            if (x != null)
            {
                TextBox2.Text = x.publisher_name;
            }
            else {
                lstMsg.Add("This publisher doesn't exist");
                txtClasse = "danger";
            }
        }

        
        public void addNewPublisher()
        {

            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Publisher> publisherDAO = df.getPublisherDAO();
            Publisher x = publisherDAO.find(p);
            if (x == null)
            {
                if (publisherDAO.create(p))
                {
                    lstMsg.Add("Publisher added successfully ");
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
                lstMsg.Add("This publisher already exist");
                txtClasse = "danger";
            }

        }

        public void updatePublisherByID()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Publisher> publisherDAO = df.getPublisherDAO();
            Publisher x = publisherDAO.find(p);
            if (x != null)
            {
                if (publisherDAO.update(p))
                {
                    lstMsg.Add("Publisher updated successfully ");
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
                lstMsg.Add("This publisher doesn't exist");
                txtClasse = "danger";
            }
        }

        public void deletePublisherByID()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Publisher> publisherDAO = df.getPublisherDAO();
            Publisher x = publisherDAO.find(p);
            if (x != null)
            {
                if (publisherDAO.delete(p))
                {
                    lstMsg.Add("Publisher deleted successfully ");
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


        // update publisher
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkFields())
            {
                updatePublisherByID();

            }
        }

        // delete publisher
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkFields())
            {
                deletePublisherByID();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (checkFieldId())
            {
                getPublisherByID();
            }
        }
        public bool checkFieldId()
        {
            getValues();
            lstMsg.Clear();
            txtClasse = "";
            if (p.publisher_id.Length == 0)
            {
                lstMsg.Add("Empty field publisher id");
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
            if (p.publisher_id.Length == 0)
            {
                lstMsg.Add("Empty field publisher id");
            }
            if (p.publisher_name.Length == 0)
            {
                lstMsg.Add("Empty field publisher name");
            }
            if (lstMsg.Count > 0)
            {
                txtClasse = "danger";
            }
            return lstMsg.Count == 0;
        }
        public void getValues()
        {
            p.publisher_id = TextBox1.Text.Trim();
            p.publisher_name = TextBox2.Text.Trim();
        }
        void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
        }
    }
}