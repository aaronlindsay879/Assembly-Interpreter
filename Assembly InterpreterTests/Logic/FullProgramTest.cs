using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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

            Program program = new Program(new string[]
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
                "CMP R2,20",
                "BGE #16",
                "HALT",
                "",
                "",
                "",
                "MOV R7,R2"
            }.Aggregate((a, b) => a + "\n" + b));


            program.Execute(ref memory, ref registers, ref currentInstruction, 30);
            Assert.IsTrue(registers.GetData(7) == 40f);
        }

        [TestMethod()]
        public void FullProgramTwo()
        {
            DataStorage memory = new DataStorage(100, true);
            DataStorage registers = new DataStorage(8, true);
            int currentInstruction = 0;

            Program program = new Program(new string[]
            {
                "LDR R0,#7",
                "LDR R1,#15",
                "AND R2,R0,R1",
                "ADD R3,R0,R1",
                "ORR R3,R0,R3",
                "CMP #23,R3",
                "BEQ #8",
                "HALT",
                "LSL R3,R3,R0",
                "MOV R7,R3",
                "CMP R7,R3",
                "BNE #17",
                "STR R7,#15",
                "HALT",
                "",
                "",
                "",
                "HALT"
            }.Aggregate((a, b) => a + "\n" + b));


            program.Execute(ref memory, ref registers, ref currentInstruction, 30);
            Assert.IsTrue(memory.GetData(15) == 2944f);
        }
    }
}
