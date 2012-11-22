using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringCalculator;

namespace StringCalculatorTests
{
    [TestClass]
    public class CalculatorTest
    {
        private Calculator calculator;
        
        [TestInitialize]
        public void TestInitialize()
        {
            calculator = new Calculator();
        }

        [TestMethod]
        public void Add_NoNumber_ReturnsZero()
        {
            Assert.AreEqual(0, calculator.Add(""));
        }

        [TestMethod]
        public void Add_OneNumber_ReturnsNumber()
        {
            Assert.AreEqual(1, calculator.Add("1"));
        }

        [TestMethod]
        public void Add_TwoNumbers_ReturnsSum()
        {
            Assert.AreEqual(3, calculator.Add("1,2"));
        }

        [TestMethod]
        public void Add_MultipleNumbers_ReturnsSum()
        {
            Assert.AreEqual(17, calculator.Add("1,2,6,8"));
        }

        [TestMethod]
        public void Add_NewlineDelimitedNumbers_ReturnsSum()
        {
            Assert.AreEqual(6, calculator.Add(@"1\n2,3"));
        }

        [TestMethod]
        public void Add_CustomDelimiterNumbers_ReturnsSum()
        {
            Assert.AreEqual(3, calculator.Add(@"//;\n1;2"));
        }

        [TestMethod]
        public void Add_OneNegativeNumber_ReturnsExceptionAndShowNumberInMessage()
        {
            try
            {
                calculator.Add("-1");

                Assert.Fail("This should fail as number is negative");
            }
            catch (ArgumentException exception)
            {
                // Ok, this works
                Assert.IsTrue(exception.Message.Contains("-1"));
            }
        }

        [TestMethod]
        public void Add_TwoNegativeNumbers_ReturnsExceptionAndShowTwoNumbersInMessage()
        {
            try
            {
                calculator.Add("-1,-5");

                Assert.Fail("This should fail as numbers are negatives");
            }
            catch (ArgumentException exception)
            {
                // Ok, this works
                Assert.IsTrue(exception.Message.Contains("-1,-5"));
            }
        }

        [TestMethod]
        public void Add_NumbersBigger1000_ReturnsSumIngnoringThese()
        {
            Assert.AreEqual(2, calculator.Add("2,1001"));
        }

        [TestMethod]
        public void Add_LargeDelimiterNumbers_ReturnsSum()
        {
            Assert.AreEqual(6, calculator.Add(@"//[***]\n1***2***3"));
        }

        [TestMethod]
        public void Add_MultipleDemilimerNumbers_ReturnsSum()
        {
            Assert.AreEqual(6,calculator.Add(@"//[*][%]\n1*2%3"));
        }
    }
}
