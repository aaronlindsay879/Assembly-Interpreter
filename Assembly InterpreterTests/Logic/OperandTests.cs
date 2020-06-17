using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assembly_Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_Interpreter.Tests
{
    [TestClass()]
    public class OperandTests
    {
        [TestMethod()]
        public void ParseTest()
        {
            Operand operand = new Operand();
            Dictionary<string, Operand> pairs = new Dictionary<string, Operand>();

            pairs.Add("R1", new Operand(new Element(OperandType.Register, 1)));
            pairs.Add("#1", new Operand(new Element(OperandType.Value, 1)));
            pairs.Add("1", new Operand(new Element(OperandType.Memory, 1)));
            pairs.Add("R1,300", new Operand(new Element(OperandType.Register, 1), new Element(OperandType.Memory, 300)));
            pairs.Add("R1,300,#20", new Operand(new Element(OperandType.Register, 1), new Element(OperandType.Memory, 300), new Element(OperandType.Value, 20)));

            foreach (var pair in pairs)
            {
                operand.Values = new List<Element>();
                operand.Parse(pair.Key);

                for (int i = 0; i < pair.Value.Values.Count; i++)
                    Assert.IsTrue(pair.Value.Values[i].Value == operand.Values[i].Value
                               && pair.Value.Values[i].OperandType == operand.Values[i].OperandType);
            }
        }
    }
}