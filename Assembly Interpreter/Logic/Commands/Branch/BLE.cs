﻿namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void BLE(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction)
        {
            //Ensure correct types for operand data
            ErrorManager.OperandCountMustBe(operand, 3, currentInstruction);
            ErrorManager.OperandValueMustBeInRange(operand, 2, 0, 29, currentInstruction);

            //If register one is less than or equal to register two, branch
            if (GetData(operand.Values[0], memory, registers) <= GetData(operand.Values[1], memory, registers))
                currentInstruction = (int)operand.Values[2].Value;
        }
    }
}
