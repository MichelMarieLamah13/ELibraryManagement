using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ELibraryManagement.Beans;
using ELibraryManagement.Dao;

namespace ELibraryManagement
{
    public partial class adminBookInventory : System.Web.UI.Page
    {
        public Book b { get; set; }
        public string txtClasse { get; set; }
        public List<string> lstMsg { get; set; }
        static string global_filepath;
        static int global_actual_stock, global_current_stock, global_issued_books;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillAuthorPublisherValues();
            }
            b = new Book();
            txtClasse = "";
            lstMsg = new List<string>();
            GridView1.DataBind();
        }

        // go button click
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkFieldId()) {
                getBookByID();
            }
        }


        // update button click
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkFields()) {
                updateBookByID();
            }
            
        }
        // delete button click
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkFields()) {
                deleteBookByID();
            }
           
        }
        // add button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkFields()) { 
                addNewBook();
            }
        }



        // user defined functions

        public void deleteBookByID()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Book> bookDAO = df.getBookDAO();
            Book x = bookDAO.find(b);
            if (x != null)
            {
                if (bookDAO.delete(x))
                {
                    lstMsg.Add("Book deleted successfully");
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
                lstMsg.Add("This book doesn't exists");
                txtClasse = "warning";
            }
        }

        public void updateBookByID()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Book> bookDAO = df.getBookDAO();
            Book x = bookDAO.find(b);
            if (x != null)
            {
                int actual_stock = b.actual_stock;
                int current_stock = b.current_stock;
                if (global_actual_stock == actual_stock)
                {

                }
                else {
                    if (actual_stock < global_issued_books)
                    {
                        lstMsg.Add("Actual Stock value cannot be less than the Issued books");
                        return;
                    }
                    else
                    {
                        current_stock = actual_stock - global_issued_books;
                        TextBox5.Text = current_stock.ToString();
                    }
                }
 
                if (bookDAO.update(b))
                {
                    lstMsg.Add("Book updated successfully");
                    txtClasse = "success";
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
                lstMsg.Add("This book doesn't exists");
                txtClasse = "warning";
            }

        }


        public void getBookByID()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Book> bookDAO = df.getBookDAO();
            Book x = bookDAO.find(b);
            if (x != null)
            {
                TextBox2.Text = x.book_name;
                TextBox3.Text = x.publish_date;
                TextBox9.Text =x.edition;
                TextBox10.Text = x.book_cost.ToString();
                TextBox11.Text = x.no_of_page.ToString();
                TextBox4.Text = x.actual_stock.ToString();
                TextBox5.Text = x.current_stock.ToString();
                TextBox6.Text = x.book_description;
                TextBox7.Text = (x.actual_stock - x.current_stock).ToString();

                DropDownList1.SelectedValue = x.language;
                DropDownList2.SelectedValue = x.publisher_name;
                DropDownList3.SelectedValue = x.author_name;

                ListBox1.ClearSelection();
                string[] genre = x.genre.Split(',');
                for (int i = 0; i < genre.Length; i++)
                {
                    for (int j = 0; j < ListBox1.Items.Count; j++)
                    {
                        if (ListBox1.Items[j].ToString() == genre[i])
                        {
                            ListBox1.Items[j].Selected = true;

                        }
                    }
                }

                global_actual_stock = x.actual_stock;
                global_current_stock = x.current_stock;
                global_issued_books = global_actual_stock - global_current_stock;
                global_filepath = x.book_img_link;
                b = x;
            }
            else
            {
                lstMsg.Add("This book doesn't exists");
                txtClasse = "warning";
            }
           
        }

        public void fillAuthorPublisherValues()
        {
            DropDownList3.Items.Clear();
            ListItem item = new ListItem("Select the author", "Select");
            DropDownList3.Items.Add(item);
            List<Author> lstAuthor = Author.getAll();
            if (lstAuthor != null)
            {
                foreach (Author s in lstAuthor)
                {
                    item = new ListItem(s.author_name, s.author_name);
                    DropDownList3.Items.Add(item);
                }
            }
            DropDownList2.Items.Clear();
            item = new ListItem("Select the publisher", "Select");
            DropDownList2.Items.Add(item);
            List<Publisher> lstPublisher = Publisher.getAll();
            if (lstPublisher != null)
            {
                foreach (Publisher s in lstPublisher)
                {
                    item = new ListItem(s.publisher_name, s.publisher_name);
                    DropDownList2.Items.Add(item);
                }
            }

            
        }


        public void addNewBook()
        {
            DAOFactory df = DAOFactory.getDAOFactory(DAOFactory.MSERVER_DAO_FACTORY);
            DAO<Book> bookDAO = df.getBookDAO();
            Book x = bookDAO.find(b);
            if (x == null)
            {
                if (bookDAO.create(b))
                {
                    lstMsg.Add("Book added successfully");
                    txtClasse = "success";
                    clearForm();
                    GridView1.DataBind();
                }
                else {
                    lstMsg.Add("Add operation in database failed");
                    txtClasse = "warning";
                }
            }
            else {
                lstMsg.Add("This book already exists");
                txtClasse = "warning";
            }
            
        }
        public bool checkFieldId() {
            getValues();
            lstMsg.Clear();
            txtClasse = "";
            if (b.book_id.Length == 0)
            {
                lstMsg.Add("Empty field book id");
            }
            if (lstMsg.Count > 0) {
                txtClasse = "danger";
            }
            return lstMsg.Count() == 0;
        }
        public bool checkFields()
        {
            getValues();

            txtClasse = "";
            if (b.book_id.Length == 0)
            {
                lstMsg.Add("Empty field book id");
            }
            if (b.book_name.Length == 0)
            {
                lstMsg.Add("Empty field book name");
            }
            if (b.genre.Length == 0)
            {
                lstMsg.Add("Empty field genre");
            }
            if (b.author_name == "Select")
            {
                lstMsg.Add("Empty field author name");
            }

            if (b.publisher_name == "Select")
            {
                lstMsg.Add("Empty field publisher name");
            }
            if (b.publish_date.Length == 0)
            {
                lstMsg.Add("Empty field publish date");
            }
            else
            {
                String[] v = b.publish_date.Split('-');
                DateTime pd = new DateTime(Int32.Parse(v[0]), Int32.Parse(v[1]), Int32.Parse(v[2]));
                DateTime today = DateTime.Now;
                int d = today.CompareTo(pd);
                if (d < 0)
                {
                    lstMsg.Add("The publish date must be less than today: " + today.ToString("dddd,dd MMMM yyyy"));
                }
            }
            if (b.language=="Select")
            {
                lstMsg.Add("Empty field language");
            }
            if (b.edition.Length == 0)
            {
                lstMsg.Add("Empty field edition");
            }
            if (b.book_description.Length == 0)
            {
                lstMsg.Add("Empty field description");
            }
            if (b.book_img_link== "~/book_inventory/")
            {
                lstMsg.Add("Empty field img link");
            }
            if (lstMsg.Count() > 0)
            {
                txtClasse = "danger";
            }
            return lstMsg.Count == 0;
        }
        public void getValues()
        {
            lstMsg.Clear();
            b.book_id = TextBox1.Text.Trim();
            b.book_name = TextBox2.Text.Trim();
            string genres = "";
            foreach (int i in ListBox1.GetSelectedIndices())
            {
                genres = genres + ListBox1.Items[i] + ",";
            }
            // genres = Adventure,Self Help,
            if (genres.Length != 0) {
                genres = genres.Remove(genres.Length - 1);
            }
            b.genre = genres;
            b.author_name = DropDownList3.SelectedItem.Value;
            b.publisher_name = DropDownList2.SelectedItem.Value;
            b.publish_date = TextBox3.Text.Trim();
            b.language = DropDownList1.SelectedItem.Value;
            b.edition = TextBox9.Text.Trim();
            try
            {
                b.book_cost = Double.Parse(TextBox10.Text.Trim());
                if (b.book_cost < 0) {
                    lstMsg.Add("Book cost must be > 0");
                }
            }
            catch (Exception ex)
            {
                lstMsg.Add("Book cost: " + ex.Message);
            }
            try
            {
                b.no_of_page = int.Parse(TextBox11.Text.Trim());
                if (b.no_of_page <=0)
                {
                    lstMsg.Add("No of page must be > 0");
                }
            }
            catch (Exception ex)
            {
                lstMsg.Add("No of page: " + ex.Message);
            }
            b.book_description = TextBox6.Text.Trim();
            try
            {
                b.actual_stock = int.Parse(TextBox4.Text.Trim());
                if (b.actual_stock < 0)
                {
                    lstMsg.Add("Actual stock must be >= 0");
                }
            }
            catch (Exception ex)
            {
                lstMsg.Add("Actual stock: " + ex.Message);
            }
            try
            {
                b.current_stock = int.Parse(TextBox4.Text.Trim());
                if (b.current_stock < 0)
                {
                    lstMsg.Add("Current stock must be >= 0");
                }
            }
            catch (Exception ex)
            {
                lstMsg.Add("Actual stock: " + ex.Message);
            }
            string filepath = "~/book_inventory/books1.png";
            if (FileUpload1.PostedFile.FileName.Length != 0)
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(Server.MapPath("book_inventory/" + filename));
                filepath = "~/book_inventory/" + filename;
            }
            else {
                filepath = global_filepath;
            }
            
            b.book_img_link = filepath;
            global_filepath = b.book_img_link;
           

        }
        public string imageLink() {
            if (b.book_img_link==null||b.book_img_link== "~/book_inventory/")
            {
                return "book_inventory/books1.png";
            }
            return b.book_img_link.Remove(0,2);
        }
        public void clearForm()
        {
                TextBox1.Text = "";  
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox9.Text = "";
                TextBox10.Text = "";
                TextBox11.Text = "";
                TextBox4.Text ="";
                TextBox5.Text = "";
                TextBox6.Text ="";
                TextBox7.Text = "";

                DropDownList1.SelectedValue = "Select";
                DropDownList2.SelectedValue = "Select";
                DropDownList3.SelectedValue = "Select";

                ListBox1.ClearSelection();
              
                b.book_img_link= "~/book_inventory/";


        }

    }
}