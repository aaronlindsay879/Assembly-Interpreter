﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembly_Interpreter
{
    //Custom action type for opcode functions
    delegate void OpcodeAction<T1, T2, T3, T4>(T1 operand, ref T2 mem, ref T3 reg, ref T4 cInstruct);

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
            //Create dictionary to map opcodes to functions
            Dictionary<Opcode, OpcodeAction<Operand, DataStorage, DataStorage, int>> map = 
                new Dictionary<Opcode, OpcodeAction<Operand, DataStorage, DataStorage, int>>();

            //Add all opcodes and functions to dictionary
            map[Opcode.LDR] = LDR;
            map[Opcode.STR] = STR;
            map[Opcode.ADD] = ADD;
            map[Opcode.SUB] = SUB;
            map[Opcode.MOV] = MOV;

            map[Opcode.B] = B;
            map[Opcode.BEQ] = BEQ;
            map[Opcode.BEQ] = BNE;
            map[Opcode.BGT] = BGT;
            map[Opcode.BGE] = BGE;
            map[Opcode.BLT] = BLT;
            map[Opcode.BLE] = BLE;

            map[Opcode.AND] = AND;
            map[Opcode.EOR] = EOR;
            map[Opcode.MVN] = MVN;
            map[Opcode.ORR] = ORR;

            //Run current instruction with all arguments given
            map[opcode].Invoke(operand, ref memory, ref registers, ref currentInstruction);
        }
    }
}
