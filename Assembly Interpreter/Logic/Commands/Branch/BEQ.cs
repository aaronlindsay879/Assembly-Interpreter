﻿namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void BEQ(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction, int maxInstruction, ref Operand comparer)
        {
            //Ensure correct types for operand data
            ErrorManager.OperandCountMustBe(operand, 1, currentInstruction);
            ErrorManager.OperandValueMustBeInRange(operand, 0, 0, maxInstruction, currentInstruction);

            //If the values are equal, branch
            if (GetData(comparer.Values[0], memory, registers) == GetData(comparer.Values[1], memory, registers))
                currentInstruction = (int)operand.Values[0].Value;
        }
    }
}
