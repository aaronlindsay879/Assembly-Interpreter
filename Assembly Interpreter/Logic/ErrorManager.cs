using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_Interpreter
{
    abstract class ErrorManager
    {
        public static void OperandMustBe(Operand operand, int operandNum, OperandType needed)
        {
            if (operand.Values[operandNum].OperandType != needed)
                throw new ArgumentException($"First operand needs to be a {needed}, is currently {operand.Values[operandNum].OperandType}");
        }

        public static void OperandMustNotBe(Operand operand, int operandNum, OperandType needed)
        {
            if (operand.Values[operandNum].OperandType == needed)
                throw new ArgumentException($"First operand needs to be a {needed}, is currently {operand.Values[operandNum].OperandType}");
        }

        public static void OperatorCountMustBe(Operand operand, int operandsNeeded)
        {
            if (operand.Values.Count != operandsNeeded)
                throw new ArgumentException($"There needs to be {operandsNeeded} operands, currently {operand.Values.Count}");
        }
    }
}
