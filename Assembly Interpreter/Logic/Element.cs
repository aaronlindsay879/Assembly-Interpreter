namespace Assembly_Interpreter
{
    public class Element
    {
        private OperandType operandType;
        private float value;

        public float Value { get => value; set => this.value = value; }
        public OperandType OperandType { get => operandType; set => operandType = value; }

        public Element(OperandType operandType, float value)
        {
            this.OperandType = operandType;
            this.Value = value;
        }
    }
}
