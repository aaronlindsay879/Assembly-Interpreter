using System;
using System.Collections.Generic;

namespace Assembly_Interpreter
{
    //Custom action type for opcode functions
    delegate void OpcodeAction<T1, T2, T3, T4, T5, T6>(T1 operand, ref T2 mem, ref T3 reg, ref T4 cInstruct, T5 mInstruct, ref T6 comparer);

    public partial class Command
    {
        private Opcode opcode;
        private Operand operand;
        private int index;

        public Opcode Opcode { get => opcode; set => opcode = value; }
        public Operand Operand { get => operand; set => operand = value; }

        public Command(string input, int index)
        {
            //Set default opcode, before parsing input
            opcode = Opcode.NONE;
            operand = new Operand();

            ParseCommand(input, index);

            this.index = index;
        }
        public Command(string input)
        {
            //Set default opcode, before parsing input
            opcode = Opcode.NONE;
            operand = new Operand();

            ParseCommand(input, 0);
            index = 0;
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

        public void ParseCommand(string input, int index)
        {
            input = input.Split(';')[0];
            if (input == "")
                return;

            string[] parts = input.Split(' ');

            //Check the input is right format
            if (parts.Length != 2 && parts[0] != "HALT")
                throw new ArgumentException($"Instruction {ErrorManager.HandleInstruction(index, 0)} did not have 2 parts");

            //Parse part-by-part
            try
            {
                opcode = (Opcode)Enum.Parse(typeof(Opcode), parts[0].ToUpper());
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"Opcode {parts[0].ToUpper()} on instruction {ErrorManager.HandleInstruction(index, 0)} is not valid");
            }  

            if (parts[0] != "HALT")
                operand.Parse(parts[1], index);
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
                    return memory.GetData((int)element.Value, element);

                case OperandType.Register:
                    //If register, fetch value at that register
                    return registers.GetData((int)element.Value, element);
            }

            throw new ArgumentException($"{element} is not a register, memory address or value.");
        }

        public void Execute(ref DataStorage memory, ref DataStorage registers, ref int currentInstruction, int maxInstruction, ref Operand comparer)
        {
            //Create dictionary to map opcodes to functions
            Dictionary<Opcode, OpcodeAction<Operand, DataStorage, DataStorage, int, int, Operand>> map =
                new Dictionary<Opcode, OpcodeAction<Operand, DataStorage, DataStorage, int, int, Operand>>();

            //Add all opcodes and functions to dictionary
            map[Opcode.LDR] = LDR;
            map[Opcode.STR] = STR;
            map[Opcode.ADD] = ADD;
            map[Opcode.SUB] = SUB;
            map[Opcode.MOV] = MOV;

            map[Opcode.CMP] = CMP;
            map[Opcode.B] = B;
            map[Opcode.BEQ] = BEQ;
            map[Opcode.BNE] = BNE;
            map[Opcode.BGT] = BGT;
            map[Opcode.BGE] = BGE;
            map[Opcode.BLT] = BLT;
            map[Opcode.BLE] = BLE;

            map[Opcode.AND] = AND;
            map[Opcode.EOR] = EOR;
            map[Opcode.MVN] = MVN;
            map[Opcode.ORR] = ORR;
            map[Opcode.LSL] = LSL;
            map[Opcode.LSR] = LSR;

            map[Opcode.HALT] = HALT;

            //Run current instruction with all arguments given
            map[opcode].Invoke(operand, ref memory, ref registers, ref currentInstruction, maxInstruction, ref comparer);
        }

        public bool Equals(Command other)
        {
            return Opcode == other.Opcode
                && Operand.Equals(other.Operand);
        }
    }
}
