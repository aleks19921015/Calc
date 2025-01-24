using System.Globalization;
using System.Threading.Tasks;
using Calc.ExpressionProcessor;

namespace Calc.CalculationProvider
{
    public class MyCalculationProvider: ICalculationProvider
    {
        public Task<string> Calculate(string expression)
        {
            return Task.FromResult(CalculationParser.Parse(expression).Calculate().ToString(CultureInfo.InvariantCulture));
        }

        public string GetDescription()
        {
            return "Использовать собственный провайдер вычислений";
        }
    }
}