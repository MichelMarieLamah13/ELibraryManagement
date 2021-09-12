using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibraryManagement.Beans
{
    public class Book
    {
        public String book_id { get; set; }
        public String book_name { get; set; }
        public String genre { get; set; }
        public String author_name { get; set; }
        public String publisher_name { get; set; }
        public String publish_date { get; set; }
        public String language { get; set; }
        public String edition { get; set; }
        public Double book_cost { get; set; }
        public int no_of_page { get; set; }
        public String book_description { get; set; }
        public int actual_stock { get; set; }
        public int current_stock { get; set; }
        public String book_img_link { get; set; }

        public Book(string book_id, string book_name, string genre, string author_name, string publisher_name, string publish_date, string language, string edition, double book_cost, int no_of_page, string book_description, int actual_stock, int current_stock, string book_img_link)
        {
            this.book_id = book_id;
            this.book_name = book_name;
            this.genre = genre;
            this.author_name = author_name;
            this.publisher_name = publisher_name;
            this.publish_date = publish_date;
            this.language = language;
            this.edition = edition;
            this.book_cost = book_cost;
            this.no_of_page = no_of_page;
            this.book_description = book_description;
            this.actual_stock = actual_stock;
            this.current_stock = current_stock;
            this.book_img_link = book_img_link;
        }

        public Book()
        {
        }
    }
}