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

        public void Execute(ref DataStorage memory, ref DataStorage registers, float delay = 1.5f)
        {
            foreach (Command command in commands)
            {
                Thread.Sleep((int)(delay * 1000));
                command.Execute(ref memory, ref registers);
            }
        }
    }
}
