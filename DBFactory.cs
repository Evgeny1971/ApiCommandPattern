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

namespace WinFormsApp1
{
   
     public interface IDbConnectionFactory { SqlConnection GetConnection(string connectionStringName); } 
     public class DbConnectionFactory : IDbConnectionFactory
    {
        string connectionStringName = "";
        public DbConnectionFactory() {  } 
        public SqlConnection GetConnection(string connectionStringName) 
        { string connectionString = connectionStringName; 
            SqlConnection conn = new SqlConnection(connectionString); 
            return conn; 
        }
    } 
}
