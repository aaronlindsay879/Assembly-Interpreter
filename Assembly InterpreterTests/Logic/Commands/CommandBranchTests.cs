using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assembly_Interpreter.Tests
{
    [TestClass()]
    public class CommandBranchTests
    {
        [TestMethod()]
        public void ExecuteTestB()
        {
            DataStorage memory = new DataStorage(100, true);
            DataStorage registers = new DataStorage(8, true);
            Program program = new Program(new Command("B #2"),
                                          new Command("LDR R0,#5"),
                                          new Command("LDR R0,#10"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

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
            Program program = new Program(new Command("BEQ 0,1,#3"),
                                          new Command("LDR R0,#5"),
                                          new Command("B #4"),
                                          new Command("LDR R0,#10"),
                                          new Command("BEQ 2,3,#7"),
                                          new Command("LDR R1,#5"),
                                          new Command("HALT"),
                                          new Command("LDR R1,#10"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

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
            Program program = new Program(new Command("BNE 0,1,#3"),
                                          new Command("LDR R0,#5"),
                                          new Command("B #4"),
                                          new Command("LDR R0,#10"),
                                          new Command("BNE 2,3,#7"),
                                          new Command("LDR R1,#5"),
                                          new Command("HALT"),
                                          new Command("LDR R1,#10"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

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
            Program program = new Program(new Command("BGT 0,1,#3"),
                                          new Command("LDR R0,#5"),
                                          new Command("B #4"),
                                          new Command("LDR R0,#10"),
                                          new Command("BGT 2,3,#7"),
                                          new Command("LDR R1,#5"),
                                          new Command("HALT"),
                                          new Command("LDR R1,#10"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

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
            Program program = new Program(new Command("BLT 0,1,#3"),
                                          new Command("LDR R0,#5"),
                                          new Command("B #4"),
                                          new Command("LDR R0,#10"),
                                          new Command("BLT 2,3,#7"),
                                          new Command("LDR R1,#5"),
                                          new Command("HALT"),
                                          new Command("LDR R1,#10"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

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
            Program program = new Program(new Command("BGE 0,1,#3"),
                                          new Command("LDR R0,#5"),
                                          new Command("B #4"),
                                          new Command("LDR R0,#10"),
                                          new Command("BGE 2,3,#7"),
                                          new Command("LDR R1,#5"),
                                          new Command("HALT"),
                                          new Command("LDR R1,#10"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

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
            Program program = new Program(new Command("BLE 0,1,#3"),
                                          new Command("LDR R0,#5"),
                                          new Command("B #4"),
                                          new Command("LDR R0,#10"),
                                          new Command("BLE 2,3,#7"),
                                          new Command("LDR R1,#5"),
                                          new Command("HALT"),
                                          new Command("LDR R1,#10"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

            Assert.IsTrue(registers.GetData(0) == 10f
                       && registers.GetData(1) == 10f);
        }
    }
}
