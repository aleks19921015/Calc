using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Calc.OpeationProvider;

namespace Calc.ExpressionProcessor
{
    public static class CalculationParser
    {
        public static ICalculation Parse(string expression)
        {
            try
            {
                if (string.IsNullOrEmpty(expression))
                    throw new ParseException();
                if (expression.StartsWith('-'))
                    expression = $"0{expression}";
                var simpleOperations = OperationProvider.GetSimpleOperations();
                expression = WithExplicitMultiplications(expression, simpleOperations);
            
                expression = SimplifyBrackets(expression);
            
                foreach (var operation in OperationProvider.GetLowPriorityOperations())
                    if (TryParseCalculation(expression, operation, out var calculation)) 
                        return calculation;
                foreach (var operation in OperationProvider.GetMidPriorityOperations())
                    if (TryParseCalculation(expression, operation, out var calculation))
                        return calculation;
                foreach (var operation in OperationProvider.GetHighPriorityOperations())
                    if (TryParseCalculation(expression, operation, out var calculation))
                        return calculation;
                foreach (var operation in simpleOperations)
                    if (TryParseSimpleCalculation(expression, operation, out var calculation)) return calculation;
            
                return new Constant{Value = expression};
            }
            catch (Exception e)
            {
                throw new ParseException();
            }
        }

        private static string SimplifyBrackets(string expression)
        {
            while (expression.Contains(")") || expression.Contains("("))
            {
                var indexOfSubExpressionEnd = expression.IndexOf(')') - 1;
                var indexOfSubExpressionStart = expression.LastIndexOf('(', indexOfSubExpressionEnd) + 1;
                var subExpressionLength = indexOfSubExpressionEnd - indexOfSubExpressionStart + 1;
                var subExpression = expression.Substring(indexOfSubExpressionStart, subExpressionLength);
                var subExpressionValue = Parse(subExpression).Calculate();
                expression = expression.Remove(indexOfSubExpressionStart - 1, subExpressionLength + 2);
                expression = expression.Insert(indexOfSubExpressionStart - 1, subExpressionValue.ToString(CultureInfo.InvariantCulture));
            }

            return expression;
        }

        private static string WithExplicitMultiplications(string expression, Dictionary<string, Func<double, double>> simpleOperations)
        {
            var builder = new StringBuilder(expression);
            var positions = simpleOperations.Keys.Append("(");
            foreach (var positionIndexes in positions.Select(position => expression.IndexesOf(position).Reverse()))
            {
                foreach (var index in positionIndexes)
                {
                    if (index > 0 && (char.IsDigit(expression[index - 1]) || expression[index - 1] == '.'))
                        builder.Insert(index, '*');
                }
                
                expression = builder.ToString();
            }

            foreach (var index in expression.IndexesOf(")").Reverse())
            {
                if (index < expression.Length -1 && (char.IsDigit(expression[index + 1]) || expression[index + 1] == '.'))
                    builder.Insert(index+1, '*');
            }

            expression = builder.ToString();
            return expression;
        }
        
        private static bool TryParseCalculation(string expression, KeyValuePair<string, Func<double, double, double>> operation, out ICalculation calculation)
        {
            var operationStartIndex = expression.IndexOf(operation.Key, StringComparison.Ordinal);
            var rightArgumentStartIndex = operationStartIndex + operation.Key.Length;
            if (operationStartIndex > -1 && (char.IsDigit(expression[operationStartIndex-1]) || expression[operationStartIndex-1] == '.' || expression[operationStartIndex - 1] == ')'))
            {
                calculation = new Calculation
                {
                    LeftArgument = Parse(expression.Substring(0, operationStartIndex)),
                    RightArgument = Parse(expression.Substring(rightArgumentStartIndex,
                        expression.Length - rightArgumentStartIndex)),
                    Operation = operation.Value
                };
                return true;
            }

            calculation = null;
            return false;
        }
        
        private static bool TryParseSimpleCalculation(string expression, KeyValuePair<string, Func<double, double>> operation, out ICalculation calculation)
        {
            var operationStartIndex = expression.IndexOf(operation.Key, StringComparison.Ordinal);
            var argumentStartIndex = operationStartIndex + operation.Key.Length;
            if (operationStartIndex == 0)
            {
                calculation = new SimpleCalculation
                {
                    Argument = Parse(expression.Substring(argumentStartIndex, expression.Length - argumentStartIndex)),
                    Operation = operation.Value
                };
                return true;
            }

            calculation = null;
            return false;
        }
    }
}