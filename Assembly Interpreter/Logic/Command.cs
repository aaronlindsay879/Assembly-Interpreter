using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_Interpreter
{
    enum Opcode
    {
        LDR,
        ADD,
        SUB
    }

    enum Addressing_Mode
    {
        Immediate,
        Direct
    }

    class Command
    {
        private Opcode opcode;
        private Operand operand;

        public Opcode Opcode { get => opcode; set => opcode = value; }
        public Operand Operand { get => operand; set => operand = value; }

        public Command(string input)
        {
            operand = new Operand();
            ParseCommand(input);
        }

        public void ParseCommand(string input)
        {
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
                returnData += $"{value.Key}: {value.Value}\n";

            return returnData;
        }
    }
}
