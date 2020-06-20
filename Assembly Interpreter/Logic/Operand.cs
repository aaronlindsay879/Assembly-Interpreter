using System;
using System.Collections.Generic;
using System.Linq;

namespace Assembly_Interpreter
{

    public class Operand
    {
        private List<Element> values;

        public List<Element> Values { get => values; set => values = value; }

        public Operand()
        {
            values = new List<Element>();
        }
        public Operand(params Element[] values)
        {
            this.values = values.ToList();
        }

        public void Parse(string input, int index)
        {
            string[] parts = input.Split(',');

            //Get data and current position for each part
            foreach (var (data, i) in parts.Select((a, b) => (a, b)))
            {
                switch (data)
                {
                    case string register when register[0] == 'R':
                        //If the partial operand is a register, strip the first character and parse the register number
                        if (!int.TryParse(register.Remove(0, 1), out int registerNum))
                            throw new ArgumentException($"{ErrorManager.NumToOrdinal(i)} operand in instruction" +
                                $" {ErrorManager.HandleInstruction(index, 0)} was not a valid Register");
                        Values.Add(new Element(OperandType.Register, registerNum));
                        break;
                    case string value when value[0] == '#':
                        //If partial operand is value (immediate addressing), strip first character and parse value
                        if (!int.TryParse(value.Remove(0, 1), out int valueNum))
                            throw new ArgumentException($"{ErrorManager.NumToOrdinal(i)} operand in instruction" +
                                $" {ErrorManager.HandleInstruction(index, 0)} was not a valid Value");
                        Values.Add(new Element(OperandType.Value, valueNum));
                        break;
                    case string memory:
                        //If partial operand is memory address (direct addressing), parse value
                        if (!int.TryParse(memory, out int memoryRef))
                            throw new ArgumentException($"{ErrorManager.NumToOrdinal(i)} operand in instruction" +
                                $" {ErrorManager.HandleInstruction(index, 0)} was not a valid Memory Address");
                        Values.Add(new Element(OperandType.Memory, memoryRef));
                        break;
                }
            }
        }

        public bool Equals(Operand other)
        {
            if (Values.Count != other.Values.Count)
                return false;

            if (Values.Count == 0)
                return true;

            return Values.Select((x, i) => x == other.Values[i]).Contains(false);
        }
    }
}
