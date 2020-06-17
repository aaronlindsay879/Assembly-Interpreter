using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void STR(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction)
        {
            //Ensure correct types for operand data
            ErrorManager.OperandMustBe(operand, 0, OperandType.Register);
            ErrorManager.OperandMustBe(operand, 1, OperandType.Register);
            ErrorManager.OperandCountMustBe(operand, 2);

            //Fetch data
            float data = GetData(operand.Values[0], memory, registers);

            //Set a certain memory address to that value
            memory.SetData((int)operand.Values[1].Value, data);
        }
    }
}
