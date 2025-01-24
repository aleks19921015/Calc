using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Calc.ExpressionProcessor;
using NUnit.Framework;

namespace Calc.Tests
{
    [TestFixture]
    public class CalculationsParserTests
    {

        [TestCase("2+3", 5, TestName = "Basic calculation")]
        [TestCase("2+2*2", 6, TestName = "Priority test 1")]
        [TestCase("2*2^3", 16, TestName = "Priority test 2")]
        [TestCase("2sqrt((1-2)4(5-6))", 4, TestName = "Brackets test")]
        public void Test1(string expression, double expectedResult)
        {
            var calculation = CalculationParser.Parse(expression);
            Assert.That(calculation.Calculate(), Is.EqualTo(expectedResult));
        }
    }
}