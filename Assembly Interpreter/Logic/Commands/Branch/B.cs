using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void B(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction)
        {
            //Ensure correct types for operand data
            ErrorManager.OperandCountMustBe(operand, 1);
            ErrorManager.OperandValueMustBeInRange(operand, 0, 0, 29);

            //Set current instruction to value
            currentInstruction = (int)GetData(operand.Values[0], memory, registers);
        }
    }
}
