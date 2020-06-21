namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void BGE(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction, int maxInstruction)
        {
            //Ensure correct types for operand data
            ErrorManager.OperandCountMustBe(operand, 3, currentInstruction);
            ErrorManager.OperandValueMustBeInRange(operand, 2, 0, maxInstruction, currentInstruction);

            //If register 1 is greater than or equal to register 2, branch
            if (GetData(operand.Values[0], memory, registers) >= GetData(operand.Values[1], memory, registers))
                currentInstruction = (int)operand.Values[2].Value;
        }
    }
}
