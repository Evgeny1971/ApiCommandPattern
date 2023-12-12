using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    /// <summary>
    /// The 'Command' abstract class
    /// </summary>

    public interface ICommand
    {
        public string ExecuteGetResult(char @operator, int operand);
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

        public string ExecuteGetResult(char @operator, int operand)
        {
            _calculator.Operation(@operator, operand);
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

        public string Result { get { return _result; } }

        public void Operation(char @operator, int operand)
        {
            switch (@operator)
            {
                case '+': curr += operand; break;
                case '-': curr -= operand; break;
                case '*': curr *= operand; break;
                case '/': curr /= operand; break;
            }
            _result = $"Current value = {curr} (following {@operator} {operand})";
        }
    }

    public interface IRecentDataAccessor
    {
        Task<IEnumerable<string>> GetOperators();
    }

    public class RecentDataAccessor : IRecentDataAccessor
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public RecentDataAccessor(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<string>> GetOperators()
        {
            using (var conn = _connectionFactory.GetConnection())
            {
                using (var cmd = new SqlCommand("[dbo].[GetOperators]", conn))
                {
                    IList<string> operators = new List<string>();
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    await conn.OpenAsync();
                    reader = await cmd.ExecuteReaderAsync();
                    ///
                    while (reader.Read())
                    {
                        string operatorValue = reader["table_name"].ToString();
                        operators.Add(operatorValue);
                    }
                    return operators;
                }
            }
        }
    }
}
