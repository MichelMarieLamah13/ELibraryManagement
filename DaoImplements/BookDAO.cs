using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ELibraryManagement.Beans;
using ELibraryManagement.Dao;

namespace ELibraryManagement.DaoImplements
{
    public class BookDAO : DAO<Book>
    {
        public BookDAO(SqlConnection conn) : base(conn) { }
        public override bool create(Book obj)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO book_master_tbl(book_id,book_name,genre,author_name,publisher_name,publish_date,language,edition,book_cost,no_of_pages,book_description,actual_stock,current_stock,book_img_link) values(@book_id,@book_name,@genre,@author_name,@publisher_name,@publish_date,@language,@edition,@book_cost,@no_of_pages,@book_description,@actual_stock,@current_stock,@book_img_link)", conn);

                cmd.Parameters.AddWithValue("@book_id", obj.book_id);
                cmd.Parameters.AddWithValue("@book_name", obj.book_name);
                cmd.Parameters.AddWithValue("@genre", obj.genre);
                cmd.Parameters.AddWithValue("@author_name", obj.author_name);
                cmd.Parameters.AddWithValue("@publisher_name", obj.publisher_name);
                cmd.Parameters.AddWithValue("@publish_date", obj.publish_date);
                cmd.Parameters.AddWithValue("@language", obj.language);
                cmd.Parameters.AddWithValue("@edition", obj.edition);
                cmd.Parameters.AddWithValue("@book_cost", obj.book_cost);
                cmd.Parameters.AddWithValue("@no_of_pages", obj.no_of_page);
                cmd.Parameters.AddWithValue("@book_description", obj.book_description);
                cmd.Parameters.AddWithValue("@actual_stock", obj.actual_stock);
                cmd.Parameters.AddWithValue("@current_stock", obj.current_stock);
                cmd.Parameters.AddWithValue("@book_img_link", obj.book_img_link);
                return cmd.ExecuteNonQuery() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override bool delete(Book obj)
        {

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("DELETE from book_master_tbl WHERE book_id=@book_id", conn);
                cmd.Parameters.AddWithValue("@book_id", obj.book_id);
                return cmd.ExecuteNonQuery() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public override Book find(object obj)
        {
            Book o = (Book)obj;
            try
            {
                Book b = new Book();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT book_id,book_name,genre,author_name,publisher_name,publish_date,language,edition,book_cost,no_of_pages,book_description,actual_stock,current_stock,book_img_link" +
                    " from book_master_tbl WHERE book_id=@book_id", conn);
                cmd.Parameters.AddWithValue("@book_id", o.book_id);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            b.book_id = rdr["book_id"].ToString();
                            b.book_name = rdr["book_name"].ToString();
                            b.genre = rdr["genre"].ToString();
                            b.author_name = rdr["author_name"].ToString();
                            b.publisher_name = rdr["publisher_name"].ToString();
                            b.publish_date = rdr["publish_date"].ToString();
                            b.language = rdr["language"].ToString();
                            b.edition = rdr["edition"].ToString();
                            b.book_cost = Double.Parse(rdr["book_cost"].ToString());
                            b.no_of_page = Int32.Parse(rdr["no_of_pages"].ToString());
                            b.book_description = rdr["book_description"].ToString();
                            b.actual_stock = Int32.Parse(rdr["actual_stock"].ToString());
                            b.current_stock = Int32.Parse(rdr["current_stock"].ToString());
                            b.book_img_link = rdr["book_img_link"].ToString();

                        }
                        return b;
                    }

                }
               

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            return null;
        }

        public override bool update(Book obj)
        {
            Book o = (Book)obj;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("UPDATE book_master_tbl set book_name=@book_name, genre=@genre, author_name=@author_name, publisher_name=@publisher_name, publish_date=@publish_date, language=@language, edition=@edition, book_cost=@book_cost, no_of_pages=@no_of_pages, book_description=@book_description, actual_stock=@actual_stock, current_stock=@current_stock, book_img_link=@book_img_link where book_id=@book_id", conn);

                cmd.Parameters.AddWithValue("@book_name", o.book_name);
                cmd.Parameters.AddWithValue("@genre", o.genre);
                cmd.Parameters.AddWithValue("@author_name", o.author_name);
                cmd.Parameters.AddWithValue("@publisher_name", o.publisher_name);
                cmd.Parameters.AddWithValue("@publish_date", o.publish_date);
                cmd.Parameters.AddWithValue("@language", o.language);
                cmd.Parameters.AddWithValue("@edition", o.edition);
                cmd.Parameters.AddWithValue("@book_cost", o.book_cost);
                cmd.Parameters.AddWithValue("@no_of_pages", o.no_of_page);
                cmd.Parameters.AddWithValue("@book_description", o.book_description);
                cmd.Parameters.AddWithValue("@actual_stock", o.actual_stock);
                cmd.Parameters.AddWithValue("@current_stock", o.current_stock);
                cmd.Parameters.AddWithValue("@book_img_link", o.book_img_link);
                cmd.Parameters.AddWithValue("@book_id", o.book_id);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}