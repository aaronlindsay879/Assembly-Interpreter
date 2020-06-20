using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Assembly_Interpreter.Tests
{
    [TestClass()]
    public class ErrorManagerTests
    {
        [DataTestMethod()]
        [DataRow(0, "First")]
        [DataRow(1, "Second")]
        [DataRow(2, "Third")]
        [DataRow(3, "Fourth")]
        public void NumToOrdinalTest(int num, string expectedOutput)
        {
            Assert.AreEqual(ErrorManager.NumToOrdinal(num), expectedOutput);
        }

        [DataTestMethod()]
        [DataRow(1, "00")]
        [DataRow(10, "09")]
        [DataRow(44, "43")]
        [DataRow(101, "100")]
        public void HandleInstructionTest(int num, string expectedOutput)
        {
            Assert.AreEqual(ErrorManager.HandleInstruction(num), expectedOutput);
        }

        [TestMethod()]
        public void OperandMustBeTest()
        {
            Operand operand = new Operand(new Element(OperandType.Register, 10));

            //This should not throw an error
            ErrorManager.OperandMustBe(operand, 0, OperandType.Register, 0);

            Assert.ThrowsException<ArgumentException>(() => ErrorManager.OperandMustBe(operand, 0, OperandType.Memory, 0));
        }

        [TestMethod()]
        public void OperandMustNotBeTest()
        {
            Operand operand = new Operand(new Element(OperandType.Register, 10));

            //This should not throw an error
            ErrorManager.OperandMustNotBe(operand, 0, OperandType.Memory, 0);

            Assert.ThrowsException<ArgumentException>(() => ErrorManager.OperandMustNotBe(operand, 0, OperandType.Register, 0));
        }

        [TestMethod()]
        public void OperandCountMustBeTest()
        {
            Operand operand = new Operand(new Element(OperandType.Register, 10));

            //This should not throw an error
            ErrorManager.OperandCountMustBe(operand, 1, 0);

            Assert.ThrowsException<ArgumentException>(() => ErrorManager.OperandCountMustBe(operand, 2, 0));
        }

        [TestMethod()]
        public void OperandValueMustBeInRangeTest()
        {
            Operand operand = new Operand(new Element(OperandType.Value, 10));

            //This should not throw an error
            ErrorManager.OperandValueMustBeInRange(operand, 0, 0, 20, 0);

            Assert.ThrowsException<ArgumentException>(() => ErrorManager.OperandValueMustBeInRange(operand, 0, 0, 5, 0));
        }
    }
}