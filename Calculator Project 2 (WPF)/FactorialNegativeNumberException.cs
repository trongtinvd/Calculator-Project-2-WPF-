using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_Project_2__WPF_
{
    class FactorialNegativeNumberException : Exception
    {
        public FactorialNegativeNumberException() : base("Factorial for negative number")
        {

        }
    }
}
