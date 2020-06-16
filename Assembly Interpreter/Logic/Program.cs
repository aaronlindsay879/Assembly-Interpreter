using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assembly_Interpreter.Logic
{
    class Program
    {
        private List<Command> commands;

        public Program(params Command[] commands)
        {
            this.commands = commands.ToList();
        }

        private bool IsJump(Opcode opcode)
        {
            string stringOpcode = opcode.ToString();
            return stringOpcode[0] == 'B';
        }

        public void Execute(ref DataStorage memory, ref DataStorage registers, ref int currentInstruction, float delay = 1.5f)
        {
            Command lastCommand = commands[0];
            while (currentInstruction < commands.Count - 1 || IsJump(lastCommand.Opcode)) 
            {
                Thread.Sleep((int)(delay * 1000));

                if (!IsJump(lastCommand.Opcode))
                    currentInstruction++;

                lastCommand = commands[currentInstruction];

                if (!commands[currentInstruction].IsNull())
                    commands[currentInstruction].Execute(ref memory, ref registers, ref currentInstruction, delay);
            }
        }
    }
}
