using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembly_Interpreter
{
    enum Opcode
    {
        LDR,
        ADD,
        SUB
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

        public void Execute(DataStorage memory, DataStorage registers)
        {
            switch (opcode)
            {
                case Opcode.LDR:
                    //Ensure correct types for operand data
                    if (operand.Values[0].Key != Operand_Type.Register
                     || operand.Values[1].Key == Operand_Type.Register)
                        throw new ArgumentException();

                    var operandData = operand.Values[1];
                    float data;

                    if (operandData.Key == Operand_Type.Value)
                        //If operand second value is immediate addressing, just pass value
                        data = operandData.Value;
                    else
                        //If memory address, get the value from memory
                        data = memory.GetData((int)operandData.Value);

                    registers.SetData((int)operand.Values[0].Value, data);
                    break;
            }
        }
    }
}
