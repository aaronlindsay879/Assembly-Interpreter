namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void MOV(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction, int maxInstruction)
        {
            //Ensure correct types for operand data
            ErrorManager.OperandCountMustBe(operand, 2, currentInstruction);
            ErrorManager.OperandMustBe(operand, 0, OperandType.Register, currentInstruction);
            ErrorManager.OperandMustBe(operand, 1, OperandType.Register, currentInstruction);

            //Get data at second register
            float data = GetData(operand.Values[1], memory, registers);

            //Set the first register to the data stored in the second register
            registers.SetData((int)operand.Values[0].Value, data);
        }
    }
}
