﻿namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void B(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction, int maxInstruction, ref Operand comparer)
        {
            //Ensure correct types for operand data
            ErrorManager.OperandCountMustBe(operand, 1, currentInstruction);
            ErrorManager.OperandValueMustBeInRange(operand, 0, 0, maxInstruction, currentInstruction);

            //Set current instruction to value
            currentInstruction = (int)GetData(operand.Values[0], memory, registers);
        }
    }
}
