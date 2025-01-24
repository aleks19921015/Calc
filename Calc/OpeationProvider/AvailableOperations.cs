using System.Collections;
using System.Collections.Generic;

namespace Calc.OpeationProvider
{
    public class AvailableOperations
    {
        public IEnumerable<string> Operations { get; set; }
        public IEnumerable<string> SimpleOperations { get; set; }
    }
}