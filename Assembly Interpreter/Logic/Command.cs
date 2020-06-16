using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembly_Interpreter
{
    public enum Opcode
    {
        LDR,
        STR,
        ADD,
        SUB
    }

    public class Command
    {
        private Opcode opcode;
        private Operand operand;

        private Opcode Opcode { get => opcode; set => opcode = value; }
        public Operand Operand { get => operand; set => operand = value; }

        public Command(string input)
        {
            operand = new Operand();
            ParseCommand(input);
        }

        public Command(Opcode opcode, Operand operand)
        {
            this.opcode = opcode;
            this.operand = operand;
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
                    //Ensure correct types for operand data
                    if (operand.Values[0].OperandType != OperandType.Register
                     || operand.Values[1].OperandType == OperandType.Register
                     || operand.Values.Count != 2)
                        throw new ArgumentException();

                    float data = GetData(operand.Values[1], memory, registers);

                    registers.SetData((int)operand.Values[0].Value, data);
                    break;

                case Opcode.STR:
                    //Ensure correct types for operand data
                    if (operand.Values[0].OperandType != OperandType.Register
                     || operand.Values[1].OperandType != OperandType.Memory
                     || operand.Values.Count != 2)
                        throw new ArgumentException();

                    data = GetData(operand.Values[0], memory, registers);

                    memory.SetData((int)operand.Values[1].Value, data);
                    break;

                case Opcode.ADD:
                    //Ensure correct types for operand data
                    if (operand.Values[0].OperandType != OperandType.Register
                     || operand.Values.Count != 3)
                        throw new ArgumentException();

                    float firstArg = GetData(operand.Values[1], memory, registers);
                    float secondArg = GetData(operand.Values[2], memory, registers);
                    data = firstArg + secondArg;

                    registers.SetData((int)operand.Values[0].Value, data);
                    break;

                case Opcode.SUB:
                    //Ensure correct types for operand data
                    if (operand.Values[0].OperandType != OperandType.Register
                     || operand.Values.Count != 3)
                        throw new ArgumentException();

                    firstArg = GetData(operand.Values[1], memory, registers);
                    secondArg = GetData(operand.Values[2], memory, registers);
                    data = firstArg - secondArg;

                    registers.SetData((int)operand.Values[0].Value, data);
                    break;

            }
        }
    }
}
