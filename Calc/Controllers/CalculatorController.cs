using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Calc.CalculationProvider;
using Calc.ExpressionProcessor;
using Calc.OpeationProvider;
using Microsoft.AspNetCore.Mvc;

namespace Calc.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CalculatorController : ControllerBase
    {
        private readonly AvailableOperations _availableOperations;

        private readonly Dictionary<string, ICalculationProvider> _calculationProviders;

        public CalculatorController()
        {
            var myCalculationProvider = new MyCalculationProvider();
            var sideCalculationProvider = new SideCalculationProvider();
            _calculationProviders = new Dictionary<string, ICalculationProvider>
            {
                { myCalculationProvider.GetDescription(), myCalculationProvider },
                { sideCalculationProvider.GetDescription(), sideCalculationProvider  }
            };
            _availableOperations = new AvailableOperations
            {
                Operations = OperationProvider
                    .GetHighPriorityOperations().Keys.Concat(OperationProvider.GetMidPriorityOperations().Keys)
                    .Concat(OperationProvider.GetLowPriorityOperations().Keys),
                SimpleOperations = OperationProvider.GetSimpleOperations().Keys
            };
        }

        [HttpGet]
        public AvailableOperations GetAvailableOperations()
        {
            return _availableOperations;
        }
        
        [HttpGet]
        public IEnumerable<string> GetCalculationProviders()
        {
            return _calculationProviders.Keys;
        }
        
        [HttpGet]
        public async Task<string> CalculateExpression(string expression, string calculationProviderName)
        {
            return await _calculationProviders[calculationProviderName].Calculate(expression);
        }
    }
}