namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void BGT(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction, int maxInstruction, ref Operand comparer)
        {
            //Ensure correct types for operand data
            ErrorManager.OperandCountMustBe(operand, 1, currentInstruction);
            ErrorManager.OperandValueMustBeInRange(operand, 0, 0, maxInstruction, currentInstruction);

            //If register 1 is greater than register 2, branch
            if (GetData(comparer.Values[0], memory, registers) > GetData(comparer.Values[1], memory, registers))
                currentInstruction = (int)operand.Values[0].Value;
        }
    }
}
