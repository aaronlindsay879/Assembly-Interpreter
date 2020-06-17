using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void HALT(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction)
        {
            //Ensure correct types for operand data
            if (operand.Values.Count != 0)
                throw new ArgumentException();

            //Set current instruction to -2 (will be handled by CU)
            currentInstruction = -2;
        }
    }
}
