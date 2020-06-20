using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assembly_Interpreter.Tests
{
    [TestClass()]
    public class CommandBitwiseTests
    {
        [TestMethod()]
        public void ANDTest()
        {
            DataStorage memory = new DataStorage(100, true);
            DataStorage registers = new DataStorage(8, true);
            registers.SetData(0, 7);
            registers.SetData(1, 15);
            registers.SetData(2, 16);
            Program program = new Program(new Command("AND R6,R0,R1"),
                                          new Command("AND R7,R1,R2"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

            Assert.IsTrue(registers.GetData(6) == 7f
                       && registers.GetData(7) == 0f);
        }

        [TestMethod()]
        public void EORTest()
        {
            DataStorage memory = new DataStorage(100, true);
            DataStorage registers = new DataStorage(8, true);
            registers.SetData(0, 7);
            registers.SetData(1, 15);
            registers.SetData(2, 16);
            Program program = new Program(new Command("EOR R6,R0,R1"),
                                          new Command("EOR R7,R1,R2"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

            Assert.IsTrue(registers.GetData(6) == 8f
                       && registers.GetData(7) == 31f);
        }

        [TestMethod()]
        public void LSLTest()
        {
            DataStorage memory = new DataStorage(100, true);
            DataStorage registers = new DataStorage(8, true);
            registers.SetData(0, 7);
            registers.SetData(1, 15);
            registers.SetData(2, 16);
            Program program = new Program(new Command("LSL R6,R0,R1"),
                                          new Command("LSL R7,R1,R2"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

            Assert.IsTrue(registers.GetData(6) == 229376f
                       && registers.GetData(7) == 983040f);
        }

        [TestMethod()]
        public void LSRTest()
        {
            DataStorage memory = new DataStorage(100, true);
            DataStorage registers = new DataStorage(8, true);
            registers.SetData(0, 7);
            registers.SetData(1, 1);
            registers.SetData(2, 3);
            Program program = new Program(new Command("LSR R6,R0,R1"),
                                          new Command("LSR R7,R0,R2"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

            Assert.IsTrue(registers.GetData(6) == 3f
                       && registers.GetData(7) == 0f);
        }

        [TestMethod()]
        public void MVNTest()
        {
            DataStorage memory = new DataStorage(100, true);
            DataStorage registers = new DataStorage(8, true);
            registers.SetData(0, 7);
            registers.SetData(1, 15);
            Program program = new Program(new Command("MVN R6,R0"),
                                          new Command("MVN R7,R1"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

            Assert.IsTrue(registers.GetData(6) == -8f
                       && registers.GetData(7) == -16f);
        }

        [TestMethod()]
        public void ORRTest()
        {
            DataStorage memory = new DataStorage(100, true);
            DataStorage registers = new DataStorage(8, true);
            registers.SetData(0, 7);
            registers.SetData(1, 15);
            registers.SetData(2, 16);
            Program program = new Program(new Command("ORR R6,R0,R1"),
                                          new Command("ORR R7,R1,R2"));
            int cIR = 0;

            program.Execute(ref memory, ref registers, ref cIR, 0);

            Assert.IsTrue(registers.GetData(6) == 15f
                       && registers.GetData(7) == 31f);
        }
    }
}