using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void LSR(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction)
        {
            //Ensure correct types for operand data
            ErrorManager.OperandCountMustBe(operand, 3);
            ErrorManager.OperandMustBe(operand, 0, OperandType.Register);
            ErrorManager.OperandMustBe(operand, 1, OperandType.Register);

            //Fetch data and use bitwise or on casted data
            float firstArg = GetData(operand.Values[1], memory, registers);
            float secondArg = GetData(operand.Values[2], memory, registers);
            float data = (int)firstArg >> (int)secondArg;

            //Set a register to that value
            registers.SetData((int)operand.Values[0].Value, data);
        }
    }
}
