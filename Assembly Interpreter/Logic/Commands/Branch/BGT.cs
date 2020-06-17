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
        public void BGT(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction)
        {
            //Ensure correct types for operand data
            ErrorManager.OperandValueMustBeInRange(operand, 0, 0, 29);
            ErrorManager.OperandCountMustBe(operand, 3);

            //If register 1 is greater than register 2, branch
            if (GetData(operand.Values[0], memory, registers) > GetData(operand.Values[1], memory, registers))
                currentInstruction = (int)operand.Values[2].Value;
        }
    }
}
