namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void BNE(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction, int maxInstruction)
        {
            //Ensure correct types for operand data
            ErrorManager.OperandCountMustBe(operand, 3, currentInstruction);
            ErrorManager.OperandValueMustBeInRange(operand, 2, 0, maxInstruction, currentInstruction);

            //If the values are not equal, branch - otherwise just increment currentInstruction like usual (this is due to change)
            if (GetData(operand.Values[0], memory, registers) != GetData(operand.Values[1], memory, registers))
                currentInstruction = (int)operand.Values[2].Value;
        }
    }
}
