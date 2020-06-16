using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembly_Interpreter
{

    public partial class Command
    {
        private Opcode opcode;
        private Operand operand;

        private Opcode Opcode { get => opcode; set => opcode = value; }
        public Operand Operand { get => operand; set => operand = value; }

        public Command(string input)
        {
            opcode = Opcode.NONE;
            operand = new Operand();
            ParseCommand(input);
        }

        public Command(Opcode opcode, Operand operand)
        {
            this.opcode = opcode;
            this.operand = operand;
        }

        public bool IsNull()
        {
            return opcode == Opcode.NONE;
        }

        public void ParseCommand(string input)
        {
            if (input == "")
                return;

            string[] parts = input.Split(' ');

            if (parts.Length != 2) 
                throw new ArgumentException();

            opcode = (Opcode)Enum.Parse(typeof(Opcode), parts[0].ToUpper());
            operand.Parse(parts[1]);
        }

        public override string ToString()
        {
            string returnData = opcode.ToString() + "\n";

            foreach (var value in operand.Values)
                returnData += $"{value.OperandType}: {value.Value}\n";

            return returnData;
        }

        private float GetData(Element element, DataStorage memory, DataStorage registers)
        {
            switch (element.OperandType)
            {
                case OperandType.Value:
                    return element.Value;

                case OperandType.Memory:
                    return memory.GetData((int)element.Value);

                case OperandType.Register:
                    return registers.GetData((int)element.Value);
            }

            return 0f;
        }

        public void Execute(ref DataStorage memory, ref DataStorage registers)
        {
            switch (opcode)
            {
                case Opcode.LDR:
                    LDR(operand, ref memory, ref registers);
                    break;

                case Opcode.STR:
                    STR(operand, ref memory, ref registers);
                    break;

                case Opcode.ADD:
                    ADD(operand, ref memory, ref registers);
                    break;

                case Opcode.SUB:
                    SUB(operand, ref memory, ref registers);
                    break;

            }
        }
    }
}
