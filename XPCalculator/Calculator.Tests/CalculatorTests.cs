using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Calculator.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        XPCalculator.Calculator calculator;
        double delta = 0.00000001;

        [TestInitialize]
        public void TesInitialize()
        {
            calculator = new XPCalculator.Calculator();
        }

        [TestMethod]
        public void Sum_1and5_and_2and7_returned4and2()
        {
            double a = 1.5;
            double b = 2.7;
            double exp = 4.2;

            double actual = calculator.sum(a, b);

            Assert.AreEqual(exp, actual, delta, "Wrong summed");
        }

        [TestMethod]
        public void Sub_1and5_and_2and7_returned4and2()
        {
            double a = 1.5;
            double b = 2.7;
            double exp = -1.2;

            double actual = calculator.sub(a, b);

            Assert.AreEqual(exp, actual, delta, "Wrong subtracted");
        }

        [TestMethod]
        public void Mul_1and5_and_2and7_returned4and2()
        {
            double a = 1.5;
            double b = 2.7;
            double exp = 4.05;

            double actual = calculator.mul(a, b);

            Assert.AreEqual(exp, actual, delta, "Wrong multiplied");
        }

        [TestMethod]
        public void Div_1and5_and_2and7_returned4and2()
        {
            double a = 1.5;
            double b = 2.7;
            double exp = 1.8;

            double actual = calculator.div(b,a);

            Assert.AreEqual(exp, actual, delta, "Wrong divised");
        }
    }
}
