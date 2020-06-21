namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void BLT(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction, int maxInstruction)
        {
            //Ensure correct types for operand data
            ErrorManager.OperandCountMustBe(operand, 3, currentInstruction);
            ErrorManager.OperandValueMustBeInRange(operand, 2, 0, maxInstruction, currentInstruction);

            //If register one is less than register two, branch
            if (GetData(operand.Values[0], memory, registers) < GetData(operand.Values[1], memory, registers))
                currentInstruction = (int)operand.Values[2].Value;
        }
    }
}
