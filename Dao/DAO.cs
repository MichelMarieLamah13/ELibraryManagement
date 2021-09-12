using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ELibraryManagement.Dao
{
    public abstract class DAO<T>
    {
        protected SqlConnection conn;

        protected DAO(SqlConnection conn)
        {
            this.conn = conn;
        }
        public abstract T find(Object obj);
        public abstract bool create(T obj);
        public abstract bool update(T obj);
        public abstract bool delete(T obj);
    }
}