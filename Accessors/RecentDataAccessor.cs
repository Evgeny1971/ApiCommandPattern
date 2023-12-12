using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Accessors
{
    public interface IRecentDataAccessor
    {
        IEnumerable<string> GetOperators();
        bool InsertNewOperator(string @operator);
        string ExecuteOperator(int operand1, string @operator, int operand2);
    }

    public class RecentDataAccessor : IRecentDataAccessor
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public RecentDataAccessor(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        ///
        public string ExecuteOperator(int operand1, string @operator, int operand2)
        {
            string result = string.Empty;
            using (var conn = _connectionFactory.GetConnection())
            {
                using (var cmd = new SqlCommand("[dbo].[stp_ExecuteOperator]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@operator", @operator));
                    cmd.Parameters.Add(new SqlParameter("@operand1", operand1));
                    cmd.Parameters.Add(new SqlParameter("@operand2", operand2));
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ///
                    while (reader.Read())
                    {
                        result = reader["A"].ToString();
                    }
                }
            }
            return result;
        }

        public IEnumerable<string> GetOperators()
        {
            using (var conn = _connectionFactory.GetConnection())
            {
                using (var cmd = new SqlCommand("[dbo].[GetOperators]", conn))
                {
                    IList<string> operators = new List<string>();
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    ///
                    while (reader.Read())
                    {
                        string operatorValue = reader["operator"].ToString();
                        operators.Add(operatorValue);
                    }
                    return operators;
                }
            }
        }

        public bool InsertNewOperator(string @operator)
        {
            bool result = true;
            using (var conn = _connectionFactory.GetConnection())
            {
                using (var cmd = new SqlCommand("[dbo].[InsertNewOperator]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@operator", @operator));
                    conn.Open();
                    int? rc = cmd.ExecuteNonQuery() as int?;

                    return result;
                }
            }
        }
    }

}
