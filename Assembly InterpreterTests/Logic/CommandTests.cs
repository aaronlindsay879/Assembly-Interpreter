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
    public class CommandTests
    {
        [TestMethod()]
        public void ParseCommandTest()
        {
            Dictionary<string, Command> pairs = new Dictionary<string, Command>();

            pairs.Add("HALT", new Command(Opcode.HALT, new Operand()));
            pairs.Add("B R0", new Command(Opcode.B, new Operand(new Element(OperandType.Register, 0))));
            pairs.Add("LDR R0,10", new Command(Opcode.LDR, new Operand(new Element(OperandType.Register, 0),
                                                                       new Element(OperandType.Value, 10))));
            pairs.Add("ADD R0,R1,#10", new Command(Opcode.ADD, new Operand(new Element(OperandType.Register, 0),
                                                                           new Element(OperandType.Register, 1),
                                                                           new Element(OperandType.Memory, 10))));

            foreach (var pair in pairs)
            {
                Command command = new Command(pair.Key);
                Assert.IsTrue(command.Equals(pair.Value));
            }
        }
    }
}