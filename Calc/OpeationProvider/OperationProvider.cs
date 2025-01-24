using System;
using System.Collections.Generic;
using Operation = System.Func<double, double, double>;
using SimpleOperation = System.Func<double, double>;

namespace Calc.OpeationProvider
{
    
    public static class OperationProvider
    {
        public static Dictionary<string, Operation> GetLowPriorityOperations()
        {
            return new Dictionary<string, Operation>
            {
                { "+", (a, b) => a + b },
                { "-", (a, b) => a - b }
            };
        }

        public static Dictionary<string, Operation> GetMidPriorityOperations()
        {
            return new Dictionary<string, Operation>
            {
                { "*", (a, b) => a * b },
                { "/", (a, b) => a / b }
            };
        }
        
        public static Dictionary<string, Operation> GetHighPriorityOperations()
        {
            return new Dictionary<string, Operation>
            {
                { "^", (a, b) => Math.Pow(a, b) }
            };
        }

        public static Dictionary<string, SimpleOperation> GetSimpleOperations()
        {
            return new Dictionary<string, SimpleOperation>()
            {
                {"sin", Math.Sin },
                {"cos", Math.Cos },
                {"sqrt", Math.Sqrt}
            };
        }
    }
}