using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void BGE(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction)
        {
            //Ensure correct types for operand data
            if (operand.Values[2].OperandType != OperandType.Value
             || operand.Values[2].Value < 0 || operand.Values[2].Value > 29
             || operand.Values.Count != 3)
                throw new ArgumentException();

            //If register 1 is greater than or equal to register 2, branch
            if (GetData(operand.Values[0], memory, registers) >= GetData(operand.Values[1], memory, registers))
                currentInstruction = (int)operand.Values[2].Value;
        }
    }
}
