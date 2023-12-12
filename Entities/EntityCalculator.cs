using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Entities
{
    public class EntityCalculator
    {
        public int Operand1 { get; set; } = 1;

        public int Operand2 { get; set; }=2;

        public string Operator { get; set; }

        public string Result { get; set; }
        public EntityCalculator() { }
        
    }
}
