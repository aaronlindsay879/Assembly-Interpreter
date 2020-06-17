using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void LDR(Operand operand, ref DataStorage memory, ref DataStorage registers)
        {
            //Ensure correct types for operand data
            if (operand.Values[0].OperandType != OperandType.Register
             || operand.Values[1].OperandType == OperandType.Register
             || operand.Values.Count != 2)
                throw new ArgumentException();

            //Fetch data
            float data = GetData(operand.Values[1], memory, registers);

            //Set a register to that value
            registers.SetData((int)operand.Values[0].Value, data);
        }
    }
}
