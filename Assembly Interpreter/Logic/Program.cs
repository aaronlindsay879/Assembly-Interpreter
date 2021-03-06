﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Assembly_Interpreter
{
    public class Program
    {
        private List<Command> commands;

        public Program(params Command[] commands)
        {
            this.commands = commands.ToList();
        }

        public Program(string input)
        {
            //Split text on every newline
            string[] splitText = input.Split(new[] { "\n" }, StringSplitOptions.None);

            //Convert all the strings into commands
            commands = splitText.Select((x, i) => new Command(x, i)).ToList();
        }

        public void Execute(ref DataStorage memory, ref DataStorage registers, ref int currentInstruction, int maxInstruction, float delay = 0f)
        {
            bool HALT = false;
            Operand comparer = new Operand();

            while (currentInstruction < commands.Count && !HALT)
            {
                //Sleep for the specified delay so the user can see it executing
                Thread.Sleep((int)(delay * 1000));

                if (currentInstruction == -2)
                {
                    HALT = true;
                    break;
                }

                //Fetch current instruction and then increment currentInstruction
                Command CIR = commands[currentInstruction];
                currentInstruction++;

                if (currentInstruction > maxInstruction)
                    throw new ArgumentException($"Instruction {ErrorManager.HandleInstruction(currentInstruction, 0)} is outside the range allowed," +
                                                $" {ErrorManager.HandleInstruction(maxInstruction, 0)} is the highest allowed instruction");

                //If there is a command, execute it
                if (!CIR.IsNull())
                    CIR.Execute(ref memory, ref registers, ref currentInstruction, maxInstruction, ref comparer);
            }
        }
    }
}
