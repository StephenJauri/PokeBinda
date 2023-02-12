using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using System.Data.SqlClient;

namespace DataAccesslayer
{
    internal class DBConnection : IDBConnection
    {
        public SqlConnection GetConnection()
        {
            SqlConnection conn = null;

            string connectionString = "Data Source=localhost;Initial Catalog=pokemon_management_db;Integrated Security=True";
            conn = new SqlConnection(connectionString);

            return conn;
        }
    }
}
