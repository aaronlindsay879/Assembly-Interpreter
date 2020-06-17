using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void SUB(Operand operand, ref DataStorage memory, ref DataStorage registers)
        {
            //Ensure correct types for operand data
            if (operand.Values[0].OperandType != OperandType.Register
             || operand.Values.Count != 3)
                throw new ArgumentException();

            //Fetch data and subtract them
            float firstArg = GetData(operand.Values[1], memory, registers);
            float secondArg = GetData(operand.Values[2], memory, registers);
            float data = firstArg - secondArg;

            //Set a register to that value
            registers.SetData((int)operand.Values[0].Value, data);
        }
    }
}
