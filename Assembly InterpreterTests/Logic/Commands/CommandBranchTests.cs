using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Assembly_Interpreter.Tests
{
    [TestClass()]
    public class CommandBranchTests
    {
        [TestMethod()]
        public void ExecuteTestBranchMaxInstruction()
        {
            DataStorage memory = new DataStorage(100, true);
            DataStorage registers = new DataStorage(8, true);
            Program program = new Program(new Command("B #2"),
                                          new Command("LDR R0,#5"),
                                          new Command("LDR R0,#10"));
            int cIR = 0;

            Assert.ThrowsException<ArgumentException>(() => program.Execute(ref memory, ref registers, ref cIR, 1));
        }

        [TestMethod()]
        public void ExecuteTestB()
        {
            DataStorage memory = new DataStorage(100, true);
            DataStorage registers = new DataStorage(8, true);
            Program program = new Program(new Command("B #2"),
                                          new Command("LDR R0,#5"),
                                          new Command("LDR R0,#10"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 30);

            Assert.IsTrue(registers.GetData(0) == 10f);
        }

        [TestMethod()]
        public void ExecuteTestBEQ()
        {
            DataStorage memory = new DataStorage(100, true);
            memory.SetData(0, 10f);
            memory.SetData(1, 15f);
            memory.SetData(2, 5f);
            memory.SetData(3, 5f);
            DataStorage registers = new DataStorage(8, true);

            Program program = new Program(new string[]
            {
                "CMP 0,1",
                "BEQ #4",
                "LDR R0,#5",
                "B #5",
                "LDR R0,#10",
                "CMP 2,3",
                "BEQ #9",
                "LDR R1,#5",
                "HALT",
                "LDR R1,#10"
            }.Aggregate((a, b) => a + "\n" + b));


            int cIR = 0;
            program.Execute(ref memory, ref registers, ref cIR, 30);

            Assert.IsTrue(registers.GetData(0) == 5f
                       && registers.GetData(1) == 10f);
        }

        [TestMethod()]
        public void ExecuteTestBNE()
        {
            DataStorage memory = new DataStorage(100, true);
            memory.SetData(0, 10f);
            memory.SetData(1, 15f);
            memory.SetData(2, 5f);
            memory.SetData(3, 5f);
            DataStorage registers = new DataStorage(8, true);
            Program program = new Program(new string[]
            {
                "CMP 0,1",
                "BNE #4",
                "LDR R0,#5",
                "B #5",
                "LDR R0,#10",
                "CMP 2,3",
                "BNE #9",
                "LDR R1,#5",
                "HALT",
                "LDR R1,#10"
            }.Aggregate((a, b) => a + "\n" + b));


            int cIR = 0;
            program.Execute(ref memory, ref registers, ref cIR, 30);

            Assert.IsTrue(registers.GetData(0) == 10f
                       && registers.GetData(1) == 5f);
        }

        [TestMethod()]
        public void ExecuteTestBGT()
        {
            DataStorage memory = new DataStorage(100, true);
            memory.SetData(0, 10f);
            memory.SetData(1, 15f);
            memory.SetData(2, 10f);
            memory.SetData(3, 5f);
            DataStorage registers = new DataStorage(8, true);
            Program program = new Program(new string[]
            {
                "CMP 0,1",
                "BGT #4",
                "LDR R0,#5",
                "B #5",
                "LDR R0,#10",
                "CMP 2,3",
                "BGT #9",
                "LDR R1,#5",
                "HALT",
                "LDR R1,#10"
            }.Aggregate((a, b) => a + "\n" + b));


            int cIR = 0;
            program.Execute(ref memory, ref registers, ref cIR, 30);

            Assert.IsTrue(registers.GetData(0) == 5f
                       && registers.GetData(1) == 10f);
        }

        [TestMethod()]
        public void ExecuteTestBLT()
        {
            DataStorage memory = new DataStorage(100, true);
            memory.SetData(0, 10f);
            memory.SetData(1, 15f);
            memory.SetData(2, 10f);
            memory.SetData(3, 5f);
            DataStorage registers = new DataStorage(8, true);
            Program program = new Program(new string[]
            {
                "CMP 0,1",
                "BLT #4",
                "LDR R0,#5",
                "B #5",
                "LDR R0,#10",
                "CMP 2,3",
                "BLT #9",
                "LDR R1,#5",
                "HALT",
                "LDR R1,#10"
            }.Aggregate((a, b) => a + "\n" + b));


            int cIR = 0;
            program.Execute(ref memory, ref registers, ref cIR, 30);

            Assert.IsTrue(registers.GetData(0) == 10f
                       && registers.GetData(1) == 5f);
        }

        [TestMethod()]
        public void ExecuteTestBGE()
        {
            DataStorage memory = new DataStorage(100, true);
            memory.SetData(0, 10f);
            memory.SetData(1, 15f);
            memory.SetData(2, 10f);
            memory.SetData(3, 10f);
            DataStorage registers = new DataStorage(8, true);
            Program program = new Program(new string[]
            {
                "CMP 0,1",
                "BGE #4",
                "LDR R0,#5",
                "B #5",
                "LDR R0,#10",
                "CMP 2,3",
                "BGE #9",
                "LDR R1,#5",
                "HALT",
                "LDR R1,#10"
            }.Aggregate((a, b) => a + "\n" + b));


            int cIR = 0;
            program.Execute(ref memory, ref registers, ref cIR, 30);

            Assert.IsTrue(registers.GetData(0) == 5f
                       && registers.GetData(1) == 10f);
        }

        [TestMethod()]
        public void ExecuteTestBLE()
        {
            DataStorage memory = new DataStorage(100, true);
            memory.SetData(0, 10f);
            memory.SetData(1, 15f);
            memory.SetData(2, 10f);
            memory.SetData(3, 10f);
            DataStorage registers = new DataStorage(8, true);
            Program program = new Program(new string[]
            {
                "CMP 0,1",
                "BLE #4",
                "LDR R0,#5",
                "B #5",
                "LDR R0,#10",
                "CMP 2,3",
                "BLE #9",
                "LDR R1,#5",
                "HALT",
                "LDR R1,#10"
            }.Aggregate((a, b) => a + "\n" + b));


            int cIR = 0;
            program.Execute(ref memory, ref registers, ref cIR, 30);

            Assert.IsTrue(registers.GetData(0) == 10f
                       && registers.GetData(1) == 10f);
        }
    }
}
