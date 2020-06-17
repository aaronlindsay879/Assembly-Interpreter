using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void MOV(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction)
        {
            //Ensure correct types for operand data
            if (operand.Values[0].OperandType != OperandType.Register
             || operand.Values[1].OperandType != OperandType.Register
             || operand.Values.Count != 2)
                throw new ArgumentException();

            //Get data at second register
            float data = GetData(operand.Values[1], memory, registers);

            //Set the first register to the data stored in the second register
            registers.SetData((int)operand.Values[0].Value, data);
        }
    }
}
