using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WinFormsApp1.Accessors
{
    public interface IDbConnectionFactory
    {
        SqlConnection GetConnection(string connectionStringName);
        SqlConnection GetConnection();
    }
    public class DbConnectionFactory : IDbConnectionFactory
    {
        string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CommandDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        //string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CommandDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public DbConnectionFactory() { }
        public SqlConnection GetConnection(string connectionStringName)
        {
            _connectionString = connectionStringName;
            SqlConnection conn = new SqlConnection(_connectionString);
            return conn;
        }

        public SqlConnection GetConnection()
        {
            string connectionString = _connectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
