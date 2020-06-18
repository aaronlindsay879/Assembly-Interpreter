namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void LDR(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction)
        {
            //Ensure correct types for operand data
            ErrorManager.OperandCountMustBe(operand, 2, currentInstruction);
            ErrorManager.OperandMustBe(operand, 0, OperandType.Register, currentInstruction);
            ErrorManager.OperandMustNotBe(operand, 1, OperandType.Register, currentInstruction);

            //Fetch data
            float data = GetData(operand.Values[1], memory, registers);

            //Set a register to that value
            registers.SetData((int)operand.Values[0].Value, data);
        }
    }
}
