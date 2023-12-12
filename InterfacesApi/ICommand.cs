using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Entities;

namespace WinFormsApp1.InterfacesApi
{
    public interface ICommand
    {
        public string ExecuteGetResult(EntityCalculator entityCalculator);
        public IEnumerable<string>  GetOperators();

        bool InsertNewOperator(EntityCalculator _entityCalculator);


    }

}
