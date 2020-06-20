using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Assembly_Interpreter.Tests
{
    [TestClass()]
    public class OperandTests
    {
        [TestMethod()]
        public void ParseTest()
        {
            Dictionary<string, Operand> pairs = new Dictionary<string, Operand>();

            pairs.Add("R1", new Operand(new Element(OperandType.Register, 1)));
            pairs.Add("#1", new Operand(new Element(OperandType.Value, 1)));
            pairs.Add("1", new Operand(new Element(OperandType.Memory, 1)));
            pairs.Add("R1,300", new Operand(new Element(OperandType.Register, 1), new Element(OperandType.Memory, 300)));
            pairs.Add("R1,300,#20", new Operand(new Element(OperandType.Register, 1), new Element(OperandType.Memory, 300), new Element(OperandType.Value, 20)));

            foreach (var pair in pairs)
            {
                Operand operand = new Operand();
                operand.Parse(pair.Key, 0);

                Assert.IsTrue(operand.Equals(pair.Value));
            }
        }
    }
}