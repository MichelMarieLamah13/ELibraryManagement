using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ELibraryManagement.Beans;
using ELibraryManagement.Dao;

namespace ELibraryManagement
{
    public partial class adminBookIssuing : System.Web.UI.Page
    {
        public Issue i { get; set; }
        public string txtClasse { get; set; }
        public List<string> lstMsg { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
            lstMsg = new List<string>();
            txtClasse = "";
            i = new Issue();
        }

        // issue book
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkIssue()) {
                if (checkIfBookExist() && checkIfMemberExist())
                {
                    issueBook();
                }
                else
                {
                    lstMsg.Add("Wrong book id or member id");
                    txtClasse = "danger";
                }
            }
            
        }
        // return book
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkFieldsIds()) {
                if (checkIfBookExist() && checkIfMemberExist())
                {
                    returnBook();
                }
                else
                {
                    lstMsg.Add("Wrong book id or member id");
                    txtClasse = "danger";
                }
            }         
        }

        // go button click event
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkFieldsIds()) {
                getNames();
            }
            
        }




        // user defined function

        public void returnBook()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Issue> issueDAO = df.getIssueDAO();
            Issue x = issueDAO.find(i);
            if (x != null)
            {
                if (issueDAO.delete(i))
                {
                    DAO<Book> bookDAO = df.getBookDAO();
                    Book b = new Book();
                    b.book_id = i.book_id;
                    Book x1 = bookDAO.find(b);
                    if (x1 != null)
                    {
                        x1.current_stock += 1;
                        if (bookDAO.update(x1))
                        {
                            lstMsg.Add("Book updated successfully");
                            txtClasse = "success";
                        }
                        else
                        {
                            lstMsg.Add("Book not updated successfully");
                            txtClasse = "danger";
                            return;
                        }
                    }
                    else
                    {
                        lstMsg.Add("Book able to issue the book");
                        txtClasse = "danger";
                        return;
                    }
                    lstMsg.Add("Book Returned Successfully");
                    txtClasse = "success";
                    clearForm();
                    GridView1.DataBind();
                }
            }
            else
            {
                lstMsg.Add("This issue doesn't exist");
                txtClasse = "danger";
            }

          
        }

        public void issueBook()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Issue> issueDAO = df.getIssueDAO();
            Issue x = issueDAO.find(i);
            if (x == null)
            {
                if (issueDAO.create(i)) {
                    DAO<Book> bookDAO = df.getBookDAO();
                    Book b = new Book();
                    b.book_id = i.book_id;
                    Book x1 = bookDAO.find(b);
                    if (x1 != null)
                    {
                        x1.current_stock -= 1;
                        if (bookDAO.update(x1))
                        {
                            lstMsg.Add("Book updated successfully");
                            txtClasse = "success";
                        }
                        else {
                            lstMsg.Add("Book not updated successfully");
                            txtClasse = "danger";
                            return;
                        }
                    }
                    else {
                        lstMsg.Add("Book able to issue the book");
                        txtClasse = "danger";
                        return;
                    }
                    lstMsg.Add("Book Issued Successfully");
                    txtClasse = "success";
                    clearForm();
                    GridView1.DataBind();
                }
            }
            else {
                lstMsg.Add("This issue already exist");
                txtClasse = "danger";
            }
            
        }

        public bool checkIfBookExist()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Book> bookDAO = df.getBookDAO();
            Book b = new Book();
            b.book_id = i.book_id;
            Book x = bookDAO.find(b);
            return x != null;
        }

        public bool checkIfMemberExist()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Member> memberDAO = df.getMemberDAO();
            Member m = new Member();
            m.member_id = i.member_id;
            Member x = memberDAO.find(m);
            return x != null;

        }

       


        public void getNames()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Book> bookDAO = df.getBookDAO();
            Book b=new Book();
            b.book_id = i.book_id;
            Book x = bookDAO.find(b);
            if (x != null)
            {
                TextBox4.Text = x.book_name;
            }
            else {
                lstMsg.Add("This book doesn't exist");
                txtClasse = "danger";
            }

            DAO<Member> memberDAO = df.getMemberDAO();
            Member m = new Member();
            m.member_id = i.member_id;
            Member x1 = memberDAO.find(m);
            if (x1 != null)
            {
                TextBox3.Text = x1.fullname;
            }
            else
            {
                lstMsg.Add("This member doesn't exist");
                txtClasse = "danger";
            }

            
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Check your condition here
                    DateTime dt = Convert.ToDateTime(e.Row.Cells[5].Text);
                    DateTime today = DateTime.Today;
                    if (today > dt)
                    {
                        e.Row.BackColor = System.Drawing.Color.PaleVioletRed;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        public bool checkIssue() {
            checkFields();
            if (i.issue_date.Length != 0 && i.due_date.Length != 0)
            {
                DateTime td = DateTime.Now;
                String[] id = i.issue_date.Split('-');
                String[] dd = i.due_date.Split('-');
                DateTime d1 = new DateTime(Int32.Parse(id[0]), Int32.Parse(id[1]), Int32.Parse(id[2]));
                DateTime d2 = new DateTime(Int32.Parse(dd[0]), Int32.Parse(dd[1]), Int32.Parse(dd[2]));
                int d = d1.CompareTo(td);
                if (d < 0)
                {
                    lstMsg.Add("The issue date must be >= than today");
                }
                d = d2.CompareTo(td);
                if (d < 0)
                {
                    lstMsg.Add("The due date must be >= than today");
                }
            }
            if (lstMsg.Count > 0)
            {
                txtClasse = "danger";
            }
            return lstMsg.Count == 0;
        }
        public bool checkFieldsIds() {
            getValues();
            lstMsg.Clear();
            txtClasse = "";
            if (i.member_id.Length == 0)
            {
                lstMsg.Add("Empty field member id");
            }
            if (i.book_id.Length == 0)
            {
                lstMsg.Add("Empty field book id");
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
            if (i.member_id.Length == 0)
            {
                lstMsg.Add("Empty field member id");
            }
            if (i.member_name.Length == 0)
            {
                lstMsg.Add("Empty field member name");
            }
            if (i.book_id.Length == 0)
            {
                lstMsg.Add("Empty field book id");
            }
            if (i.book_name.Length == 0)
            {
                lstMsg.Add("Empty field book name");
            }
           
            if (i.issue_date.Length == 0)
            {
                lstMsg.Add("Empty field publish date");
            }

            if (i.due_date.Length == 0)
            {
                lstMsg.Add("Empty field due date");
            }

            
           
            if (i.issue_date.Length != 0 && i.due_date.Length != 0) {
                String[] id = i.issue_date.Split('-');
                String[] dd = i.due_date.Split('-');
                DateTime d1 = new DateTime(Int32.Parse(id[0]), Int32.Parse(id[1]), Int32.Parse(id[2]));
                DateTime d2 = new DateTime(Int32.Parse(dd[0]), Int32.Parse(dd[1]), Int32.Parse(dd[2]));
                int d = d2.CompareTo(d1);
                if (d < 0)
                {
                    lstMsg.Add("The issue date must be < due date");
                }
            }
          
            if (lstMsg.Count > 0) {
                txtClasse = "danger";
            }

            return lstMsg.Count == 0;
        }
        public void getValues()
        {
            i.member_id = TextBox2.Text.Trim();
            i.member_name = TextBox3.Text.Trim();
            i.book_id = TextBox1.Text.Trim();
            i.book_name = TextBox4.Text.Trim();
            i.issue_date = TextBox5.Text.Trim();
            i.due_date = TextBox6.Text.Trim();

        }

        public void clearForm() {
            TextBox2.Text="";
            TextBox3.Text="";
            TextBox1.Text="";
            TextBox4.Text="";
            TextBox5.Text="";
            TextBox6.Text="";
        }
    }
}