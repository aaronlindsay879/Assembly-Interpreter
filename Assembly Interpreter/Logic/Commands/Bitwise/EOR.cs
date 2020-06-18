namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void EOR(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction)
        {
            //Ensure correct types for operand data
            ErrorManager.OperandCountMustBe(operand, 3, currentInstruction);
            ErrorManager.OperandMustBe(operand, 0, OperandType.Register, currentInstruction);
            ErrorManager.OperandMustBe(operand, 1, OperandType.Register, currentInstruction);

            //Fetch data and use bitwise xor on casted data
            float firstArg = GetData(operand.Values[1], memory, registers);
            float secondArg = GetData(operand.Values[2], memory, registers);
            float data = (int)firstArg ^ (int)secondArg;

            //Set a register to that value
            registers.SetData((int)operand.Values[0].Value, data);
        }
    }
}
