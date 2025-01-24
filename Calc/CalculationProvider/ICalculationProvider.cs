using System.Threading.Tasks;

namespace Calc.CalculationProvider
{
    public interface ICalculationProvider
    {
        public Task<string> Calculate(string expression);
        public string GetDescription();
    }
}