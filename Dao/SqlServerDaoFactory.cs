using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ELibraryManagement.Beans;
using ELibraryManagement.Connexion;
using ELibraryManagement.DaoImplements;

namespace ELibraryManagement.Dao
{
    public class SqlServerDaoFactory:DAOFactory
    {
        SqlConnection conn = SQLServer.getConnection();
        public override DAO<Admin> getAdminDAO()
        {
            return new AdminDAO(conn);
        }

        public override DAO<Author> getAuthorDAO()
        {
            return new AuthorDAO(conn);
        }

        public override DAO<Book> getBookDAO()
        {
            return new BookDAO(conn);
        }

        public override DAO<Issue> getIssueDAO()
        {
            return new IssueDAO(conn);
        }

        public override DAO<Member> getMemberDAO()
        {
            return new MemberDAO(conn);
        }

        public override DAO<Publisher> getPublisherDAO()
        {
            return new PublisherDAO(conn);
        }
    }
}