namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void CMP(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction, int maxInstruction, ref Operand comparer)
        {
            //Ensure correct types for operand data
            ErrorManager.OperandCountMustBe(operand, 2, currentInstruction);

            //Set current instruction to value
            comparer = operand;
        }
    }
}
