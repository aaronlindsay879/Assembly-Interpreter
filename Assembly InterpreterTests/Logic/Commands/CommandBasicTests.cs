using Assembly_Interpreter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assembly_Interpreter.Tests
{
    [TestClass()]
    public class CommandBasicTests
    {
        [TestMethod()]
        public void ExecuteTestLdr()
        {
            DataStorage memory = new DataStorage(100, true);
            memory.SetData(0, 100f);
            DataStorage registers = new DataStorage(8, true);
            Program program = new Program(new Command("LDR R0,#10"),
                                          new Command("LDR R1,0"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

            Assert.IsTrue(registers.GetData(0) == 10f
                       && registers.GetData(1) == 100f);
        }

        [TestMethod()]
        public void ExecuteTestHalt()
        {
            DataStorage memory = new DataStorage(100, true);
            DataStorage registers = new DataStorage(8, true);
            Program program = new Program(new Command("HALT"),
                                          new Command("LDR R0,#10"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

            Assert.IsTrue(registers.GetData(0) == 0f);
        }

        [TestMethod()]
        public void ExecuteTestStr()
        {
            DataStorage memory = new DataStorage(100, true);
            DataStorage registers = new DataStorage(8, true);
            registers.SetData(0, 100f);
            Program program = new Program(new Command("STR R0,#10"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

            Assert.IsTrue(memory.GetData(10) == 100f);
        }

        [TestMethod()]
        public void ExecuteTestAdd()
        {
            DataStorage memory = new DataStorage(100, true);
            memory.SetData(10, 15f);
            memory.SetData(20, 45f);
            DataStorage registers = new DataStorage(8, true);
            registers.SetData(6, 10f);
            registers.SetData(7, 40f);
            Program program = new Program(new Command("ADD R0,#10,#20"),
                                          new Command("ADD R1,R6,R7"),
                                          new Command("ADD R2,10,20"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

            Assert.IsTrue(registers.GetData(0) == 30f
                       && registers.GetData(1) == 50f
                       && registers.GetData(2) == 60f);
        }

        [TestMethod()]
        public void ExecuteTestSub()
        {
            DataStorage memory = new DataStorage(100, true);
            memory.SetData(10, 15f);
            memory.SetData(20, 45f);
            DataStorage registers = new DataStorage(8, true);
            registers.SetData(6, 10f);
            registers.SetData(7, 40f);
            Program program = new Program(new Command("SUB R0,#20,#10"),
                                          new Command("SUB R1,R7,R6"),
                                          new Command("SUB R2,20,10"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

            Assert.IsTrue(registers.GetData(0) == 10f
                       && registers.GetData(1) == 30f
                       && registers.GetData(2) == 30f);
        }

        [TestMethod()]
        public void ExecuteTestMov()
        {
            DataStorage memory = new DataStorage(100, true);
            DataStorage registers = new DataStorage(8, true);
            registers.SetData(0, 10f);
            Program program = new Program(new Command("MOV R1,R0"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

            Assert.IsTrue(registers.GetData(1) == 10f);
        }
    }
}