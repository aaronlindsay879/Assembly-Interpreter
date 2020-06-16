using System.Linq;

namespace Assembly_Interpreter
{
    class DataStorage
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
    }
}
