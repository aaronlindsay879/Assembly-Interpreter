namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void STR(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction, int maxInstruction, ref Operand comparer)
        {
            //Ensure correct types for operand data
            ErrorManager.OperandCountMustBe(operand, 2, currentInstruction);
            ErrorManager.OperandMustBe(operand, 0, OperandType.Register, currentInstruction);
            ErrorManager.OperandMustBe(operand, 1, OperandType.Value, currentInstruction);

            //Fetch data
            float data = GetData(operand.Values[0], memory, registers);

            //Set a certain memory address to that value
            memory.SetData((int)operand.Values[1].Value, data);
        }
    }
}
