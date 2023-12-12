using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Engine;
using WinFormsApp1.InterfacesApi;
using WinFormsApp1.Accessors;
using WinFormsApp1.Entities;

namespace WinFormsApp1.Controllers
{

    public class CalculatorCommand : ICommand
    {
        char @operator;
        int operand;
        Calculator _calculator;
        private readonly IRecentDataAccessor _recentDataAccessor;

        // Constructor

        public CalculatorCommand(Calculator calculator, IRecentDataAccessor recentDataAccessor)
        {
            _calculator = calculator;
            _recentDataAccessor = recentDataAccessor;
        }

        // Gets operator



        // Execute new command

        public string ExecuteGetResult(EntityCalculator entityCalculator)
        {
            _calculator.Operation(entityCalculator.Operand1, entityCalculator.Operator, entityCalculator.Operand2);
            entityCalculator.Result = _calculator.Result;
            return _calculator.Result;
        }

        public IEnumerable<string> GetOperators()
        {
            IEnumerable<string> operators = _recentDataAccessor.GetOperators();
            return operators;
        }

        public bool InsertNewOperator(EntityCalculator entityCalculator)
        {
            bool isSuccess = _recentDataAccessor.InsertNewOperator(entityCalculator.Operator);
            return isSuccess;
        }

        // Unexecute last command


    }

}
