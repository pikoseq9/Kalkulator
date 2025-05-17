using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kalkulator.Model
{
    public class Calculator
    {
        private double? _value1;
        private string? _op;
        private double? _result;
        public double? LastValue { get; set; } = 0;

        public double? Value1 { get; set; } //automatiker
        public string? Op { get; set; } //automatiker
        public double? Result { get; set; } //automatiker
        public bool LastClickIsNumber { get; set; }


        public Calculator(double? value1, string? op, double? result)
        {
            this._value1 = value1;
            this._op = op;
            this._result = result;
        }

        public void calculate()
        {
            switch (this.Op)
            {
                case "+": Result = Result + Value1; break;
                case "-": Result = Result - Value1; break;
                case "*": Result = Result * Value1; break;
                case "/": if (Value1 != 0) Result = Result / Value1; break;
                case "%": Result = Result / Value1 * 100; break;
                default: Result = Result; break;
            }
        }
    }
}
