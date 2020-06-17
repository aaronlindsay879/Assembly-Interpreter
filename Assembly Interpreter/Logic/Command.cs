﻿using System;
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

        public Opcode Opcode { get => opcode; set => opcode = value; }
        public Operand Operand { get => operand; set => operand = value; }

        public Command(string input)
        {
            //Set default opcode, before parsing input
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
            //Check if opcode is still default value
            return opcode == Opcode.NONE;
        }

        public void ParseCommand(string input)
        {
            input = input.Split(';')[0];
            if (input == "")
                return;

            string[] parts = input.Split(' ');

            //Check the input is right format
            if (parts.Length != 2) 
                throw new ArgumentException();

            //Parse part-by-part
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
                    //If memory address, fetch value at that memory address
                    return memory.GetData((int)element.Value);

                case OperandType.Register:
                    //If register, fetch value at that register
                    return registers.GetData((int)element.Value);
            }

            return 0f;
        }

        public void Execute(ref DataStorage memory, ref DataStorage registers, ref int currentInstruction)
        {
            //Run relevant code for each opcode
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

                case Opcode.MOV:
                    MOV(operand, ref memory, ref registers);
                    break;

                case Opcode.B:
                    B(operand, ref memory, ref registers, ref currentInstruction);
                    break;

                case Opcode.BEQ:
                    BEQ(operand, ref memory, ref registers, ref currentInstruction);
                    break;

                case Opcode.BNE:
                    BNE(operand, ref memory, ref registers, ref currentInstruction);
                    break;

                case Opcode.BGT:
                    BGT(operand, ref memory, ref registers, ref currentInstruction);
                    break;

                case Opcode.BLT:
                    BLT(operand, ref memory, ref registers, ref currentInstruction);
                    break;

                case Opcode.BGE:
                    BGE(operand, ref memory, ref registers, ref currentInstruction);
                    break;

                case Opcode.BLE:
                    BLE(operand, ref memory, ref registers, ref currentInstruction);
                    break;
            }
        }
    }
}
