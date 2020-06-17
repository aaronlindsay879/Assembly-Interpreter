using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void B(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction, float delay)
        {
            //Ensure correct types for operand data
            if (operand.Values[0].OperandType != OperandType.Value
             || operand.Values[0].Value < 0 || operand.Values[0].Value > 29
             || operand.Values.Count != 1)
                throw new ArgumentException();

            //Set current instruction to value
            currentInstruction = (int)GetData(operand.Values[0], memory, registers);
        }
    }
}
