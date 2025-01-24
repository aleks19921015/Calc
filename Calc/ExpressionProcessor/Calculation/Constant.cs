namespace Calc.ExpressionProcessor
{
    public class Constant: ICalculation
    {
        public string Value { get; init; }

        public double Calculate() => double.Parse($"{Value}", System.Globalization.CultureInfo.InvariantCulture);
    }
}