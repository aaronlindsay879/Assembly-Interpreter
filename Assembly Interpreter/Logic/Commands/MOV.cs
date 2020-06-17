namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void MOV(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction)
        {
            //Ensure correct types for operand data
            ErrorManager.OperandCountMustBe(operand, 2);
            ErrorManager.OperandMustBe(operand, 0, OperandType.Register);
            ErrorManager.OperandMustBe(operand, 1, OperandType.Register);

            //Get data at second register
            float data = GetData(operand.Values[1], memory, registers);

            //Set the first register to the data stored in the second register
            registers.SetData((int)operand.Values[0].Value, data);
        }
    }
}
