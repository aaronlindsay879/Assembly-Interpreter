namespace Assembly_Interpreter
{
    public partial class Command
    {
        public void HALT(Operand operand, ref DataStorage memory, ref DataStorage registers, ref int currentInstruction, int maxInstruction)
        {
            //Set current instruction to -2 (will be handled by CU)
            currentInstruction = -2;
        }
    }
}
