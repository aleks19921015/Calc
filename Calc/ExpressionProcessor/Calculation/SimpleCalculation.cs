using System;

namespace Calc.ExpressionProcessor
{
    public class SimpleCalculation: ICalculation
    {
        public ICalculation Argument { get; set; }
        
        public Func<double, double> Operation { get; set; }
        
        public double Calculate() => Operation(Argument.Calculate());
    }
}