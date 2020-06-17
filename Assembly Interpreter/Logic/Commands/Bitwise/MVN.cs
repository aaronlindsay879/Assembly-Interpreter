using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void MVN(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction)
        {
            //Ensure correct types for operand data
            if (operand.Values[0].OperandType != OperandType.Register
             || operand.Values.Count != 2)
                throw new ArgumentException();

            //Fetch data and use bitwise not on casted data
            float firstArg = GetData(operand.Values[1], memory, registers);
            float data = ~(int)firstArg;

            //Set a register to that value
            registers.SetData((int)operand.Values[0].Value, data);
        }
    }
}
