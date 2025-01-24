using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Calc.CalculationProvider
{
    public class SideCalculationProvider: ICalculationProvider
    {
        public async Task<string> Calculate(string expression)
        {
            expression = HttpUtility.UrlEncode(expression);
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://api.mathjs.org/v4/");
            var response = await httpClient.GetAsync($"?expr={expression}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(response.ReasonPhrase);
        }

        public string GetDescription()
        {
            return "Использовать сторонний провайдер вычислений";
        }
    }
}