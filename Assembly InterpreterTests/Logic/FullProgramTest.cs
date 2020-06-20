using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_Interpreter.Tests
{
    [TestClass()]
    public class FullProgramTest
    {
        [TestMethod()]
        public void FullProgramOne()
        {
            DataStorage memory = new DataStorage(100, true);
            DataStorage registers = new DataStorage(8, true);
            int currentInstruction = 0;

            string command = new string[]
            {
                "LDR R0,#10",
                "LDR R1,#2",
                "LSL R2,R0,R1",
                "",
                "LDR R3,#10",
                "LDR R4,#20",
                "ADD R3,R3,R4",
                "MOV R4,R3",
                "STR R4,#20",
                "",
                "BGE R2,20,#15",
                "HALT",
                "",
                "",
                "",
                "MOV R7,R2"
            }.Aggregate((a, b) => a + "\n" + b);
            Program program = new Program(command);


            program.Execute(ref memory, ref registers, ref currentInstruction, 0);
            Assert.IsTrue(registers.GetData(7) == 40f);
        }

        [TestMethod()]
        public void FullProgramTwo()
        {
            DataStorage memory = new DataStorage(100, true);
            DataStorage registers = new DataStorage(8, true);
            int currentInstruction = 0;

            string command = new string[]
            {
                "LDR R0,#7",
                "LDR R1,#15",
                "AND R2,R0,R1",
                "ADD R3,R0,R1",
                "ORR R3,R0,R3",
                "BEQ #23,R3,#07",
                "HALT",
                "LSL R3,R3,R0",
                "MOV R7,R3",
                "BNE R7,R3,#15",
                "STR R7,#15",
                "HALT",
                "",
                "",
                "",
                "HALT"
            }.Aggregate((a, b) => a + "\n" + b);
            Program program = new Program(command);


            program.Execute(ref memory, ref registers, ref currentInstruction, 0);
            Assert.IsTrue(memory.GetData(15) == 2944f);
        }
    }
}
