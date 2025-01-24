using System;

namespace Calc.ExpressionProcessor
{
    
    public class ParseException: Exception
    {
        public ParseException():base("Syntax error."){}
    }
}