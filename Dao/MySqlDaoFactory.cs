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
    public class MySqlDaoFactory : DAOFactory
    {
        public override DAO<Admin> getAdminDAO()
        {
            throw new NotImplementedException();
        }

        public override DAO<Author> getAuthorDAO()
        {
            throw new NotImplementedException();
        }

        public override DAO<Book> getBookDAO()
        {
            throw new NotImplementedException();
        }

        public override DAO<Issue> getIssueDAO()
        {
            throw new NotImplementedException();
        }

        public override DAO<Member> getMemberDAO()
        {
            throw new NotImplementedException();
        }

        public override DAO<Publisher> getPublisherDAO()
        {
            throw new NotImplementedException();
        }
    }
}