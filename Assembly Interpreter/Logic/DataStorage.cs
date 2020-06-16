using System;
using System.Linq;

namespace Assembly_Interpreter
{
    public class DataStorage
    {
        private float[] memory;

        public DataStorage(int memLength, bool fillWithZeroes = false)
        {
            memory = new float[memLength];

            if (fillWithZeroes)
                memory = memory.Select(x => 0f).ToArray();
        }

        public float GetData(int pos)
        {
            if (pos < memory.Length && pos >= 0)
                return memory[pos];

            return 0f;
        }

        public bool SetData(int pos, float data)
        {
            if (pos < memory.Length && pos >= 0)
            {
                memory[pos] = data;
                return true;
            }

            return false;
        }

        public string CreateOutput(int rows = 10)
        {
            string output = "│";
            int cols = (int)Math.Ceiling((float)memory.Length / rows);
            int currentPosOne = 0;
            int currentPosTwo = 0;
            int currentPosThree = 0;

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (currentPosOne >= memory.Length)
                        break;
                    output += $"  {currentPosOne++.ToString().PadLeft(2, '0')}  │";
                }
                output += "\n│";

                for (int j = 0; j < rows; j++)
                {
                    if (currentPosTwo >= memory.Length)
                        break;
                    output += $" {memory[currentPosTwo++].ToString().PadLeft(4, '0')} │";
                }
                output += "\n┼";

                for (int j = 0; j < rows; j++)
                {
                    if (currentPosThree++ >= memory.Length)
                        break;
                    output += "──────┼";
                }
                output += (currentPosThree >= memory.Length) ? "\n" : "\n│";
            }

            return output;
        }
    }
}
