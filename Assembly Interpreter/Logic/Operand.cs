using System;
using System.Collections.Generic;

namespace Assembly_Interpreter
{
    enum Operand_Type
    {
        Register,
        Memory,
        Value
    }

    class Operand
    {
        private List<KeyValuePair<Operand_Type, float>> values;

        internal List<KeyValuePair<Operand_Type, float>> Values { get => values; set => values = value; }

        public Operand()
        {
            values = new List<KeyValuePair<Operand_Type, float>>();
        }

        public void Parse(string input)
        {
            string[] parts = input.Split(',');

            foreach(string data in parts)
            {
                switch (data)
                {
                    case string register when register[0] == 'R':
                        int.TryParse(register.Remove(0, 1), out int registerNum);
                        Values.Add(new KeyValuePair<Operand_Type, float>(Operand_Type.Register, registerNum));
                        break;
                    case string value when value[0] == '#':
                        int.TryParse(value.Remove(0, 1), out int valueNum);
                        Values.Add(new KeyValuePair<Operand_Type, float>(Operand_Type.Register, valueNum));
                        break;
                    case string memory:
                        int.TryParse(memory, out int memoryRef);
                        Values.Add(new KeyValuePair<Operand_Type, float>(Operand_Type.Memory, memoryRef));
                        break;
                }
            }
        }
    }
}
