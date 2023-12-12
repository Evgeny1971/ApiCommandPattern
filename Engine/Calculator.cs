using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Accessors;

namespace WinFormsApp1.Engine
{

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
                    isRunSql = true;
                    break;
            }
            _result = $"Result = {operand1}; (operator={@operator}, operand2={operand2})";
            if (isRunSql)
            {
                string resultSql = _recentDataAccessor.ExecuteOperator(operand1, @operator, operand2);
                _result = $"Result = {resultSql} ;(operator={@operator}; @param1={operand1}, @param2={operand2})";
            }
        }
    }

}
