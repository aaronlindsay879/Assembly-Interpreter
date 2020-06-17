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
            if (operand.Values[0].OperandType != OperandType.Register
             || operand.Values[1].OperandType != OperandType.Memory
             || operand.Values.Count != 2)
                throw new ArgumentException();

            //Fetch data
            float data = GetData(operand.Values[0], memory, registers);

            //Set a certain memory address to that value
            memory.SetData((int)operand.Values[1].Value, data);
        }
    }
}
