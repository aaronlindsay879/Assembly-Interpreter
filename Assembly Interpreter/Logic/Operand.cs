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

        public void Parse(string input)
        {
            string[] parts = input.Split(',');

            foreach(string data in parts)
            {
                switch (data)
                {
                    case string register when register[0] == 'R':
                        //If the partial operand is a register, strip the first character and parse the register number
                        int.TryParse(register.Remove(0, 1), out int registerNum);
                        Values.Add(new Element(OperandType.Register, registerNum));
                        break;
                    case string value when value[0] == '#':
                        //If partial operand is value (immediate addressing), strip first character and parse value
                        int.TryParse(value.Remove(0, 1), out int valueNum);
                        Values.Add(new Element(OperandType.Value, valueNum));
                        break;
                    case string memory:
                        //If partial operand is memory address (direct addressing), parse value
                        int.TryParse(memory, out int memoryRef);
                        Values.Add(new Element(OperandType.Memory, memoryRef));
                        break;
                }
            }
        }
    }
}
