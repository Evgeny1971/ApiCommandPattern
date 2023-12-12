using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Eventing.Reader;

namespace WinFormsApp1
{
    /// <summary>
    /// The 'Command' abstract class
    /// </summary>

    public interface ICommand
    {
        public string ExecuteGetResult(int operand1, string @operator, int operand);
    }

    /// <summary>
    /// The 'ConcreteCommand' class
    /// </summary>

    public class CalculatorCommand : ICommand
    {
        char @operator;
        int operand;
        Calculator _calculator;

        // Constructor

        public CalculatorCommand(Calculator calculator)
        {
            _calculator = calculator;
        }

        // Gets operator



        // Execute new command

        public string ExecuteGetResult(int operand1, string @operator, int operand2)
        {
            _calculator.Operation(operand1,@operator, operand2);
            return _calculator.Result;
        }

        // Unexecute last command


    }

    /// <summary>
    /// The 'Receiver' class
    /// </summary>

    public class Calculator
    {
        int curr = 0;
        string _result = string.Empty;
        private readonly IRecentDataAccessor _recentDataAccessor;

        public Calculator(IRecentDataAccessor recentDataAccessor)
        {
                _recentDataAccessor = recentDataAccessor;
        }
        public string Result { get { return _result; } }

        public void Operation(int operand1, string @operator, int operand2)
        {
            bool isRunSql = false;
            switch (@operator)
            {
                case "+": operand1 += operand2; break;
                case "-": operand1 -= operand2; break;
                case "*": operand1 *= operand2; break;
                case "/": operand1 /= operand2; break;
                default:
                    // code block
                    isRunSql=true;
                break;
            }
            _result = $"Current value = {operand1} (following {@operator} {operand2})";
            if (isRunSql) 
            {
               string resultSql= _recentDataAccessor.ExecuteOperator(operand1, @operator, operand2);
               _result=$"Current value = {resultSql} (following {@operator} {operand1},{operand2})";
            }
        }
    }

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

        public  IEnumerable<string> GetOperators()
        {
            using (var conn = _connectionFactory.GetConnection())
            {
                using (var cmd = new SqlCommand("[dbo].[GetOperators]", conn))
                {
                    IList<string> operators = new List<string>();
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    reader =  cmd.ExecuteReader();
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
