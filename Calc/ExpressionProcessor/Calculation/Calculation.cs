#nullable enable
using System;

namespace Calc.ExpressionProcessor
{
    public class Calculation: ICalculation
    {
        public ICalculation LeftArgument { get; set; }
        public ICalculation RightArgument { get; set; }
        public Func<double, double, double> Operation { get; set; }
        public double Calculate() => Operation(LeftArgument.Calculate(), RightArgument.Calculate());
    }
}