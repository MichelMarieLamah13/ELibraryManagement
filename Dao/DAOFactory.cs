using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ELibraryManagement.Beans;
using ELibraryManagement.Dao;

namespace ELibraryManagement.Dao
{
    public abstract class DAOFactory
    {
        public static readonly int MYSQL_DAO_FACTORY = 0;
        public static readonly int MSERVER_DAO_FACTORY = 1;
        public abstract DAO<Admin> getAdminDAO();
        public abstract DAO<Author> getAuthorDAO();
        public abstract DAO<Book> getBookDAO();
        public abstract DAO<Issue> getIssueDAO();
        public abstract DAO<Member> getMemberDAO();
        public abstract DAO<Publisher> getPublisherDAO();
        public static DAOFactory getDAOFactory(int type) {
            switch (type) {
                case 0:
                    return new MySqlDaoFactory();
                case 1:
                    return new SqlServerDaoFactory();
                default:
                    return null;
            }
        }

    }
}