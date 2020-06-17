using System;

namespace Assembly_Interpreter
{
    abstract class ErrorManager
    {
        //Class to help remove duplicate code in checking operand conditions

        private static string NumToOrdinal(int num)
        {
            //Converts number into ordinal, accounting for 0 indexing
            if (num == 0)
                return "First";
            else if (num == 1)
                return "Second";
            else if (num == 2)
                return "Third";
            else if (num == 3)
                return "Fourth";

            return "";
        }

        public static void OperandMustBe(Operand operand, int operandNum, OperandType needed)
        {
            //Ensure operand is a certain type
            if (operand.Values[operandNum].OperandType != needed)
                throw new ArgumentException($"{NumToOrdinal(operandNum)} operand needs to be a {needed}, is currently {operand.Values[operandNum].OperandType}");
        }

        public static void OperandMustNotBe(Operand operand, int operandNum, OperandType needed)
        {
            //Ensure operand is not a certain type
            if (operand.Values[operandNum].OperandType == needed)
                throw new ArgumentException($"{NumToOrdinal(operandNum)} operand needs to be a {needed}, is currently {operand.Values[operandNum].OperandType}");
        }

        public static void OperandCountMustBe(Operand operand, int operandsNeeded)
        {
            //Ensure there is a certain number of operands
            if (operand.Values.Count != operandsNeeded)
                throw new ArgumentException($"There needs to be {operandsNeeded} operands, currently {operand.Values.Count}");
        }

        public static void OperandValueMustBeInRange(Operand operand, int operandNum, int lowerRange, int upperRange, bool inclusive = true)
        {
            //Ensure a value operand falls within a certain range
            OperandMustBe(operand, operandNum, OperandType.Value);
            float value = operand.Values[operandNum].Value;

            if (inclusive)
                if (value < lowerRange || value > upperRange)
                    throw new ArgumentException($"The operand must be in the range {lowerRange}<x<{upperRange}, but is {value}");
                else if (value <= lowerRange || value >= upperRange)
                    throw new ArgumentException($"The operand must be in the range {lowerRange}<=x<={upperRange}, but is {value}");
        }
    }
}
