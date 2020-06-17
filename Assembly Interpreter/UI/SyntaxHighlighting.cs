using Assembly_Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembly_Interpreter
{
    partial class MyForm : Form
    {
        private void SyntaxHighlighting(string textBoxStr)
        {
            RichTextBox textBox = (RichTextBox)Controls[textBoxStr];

            //Save current position and clear all highlighting
            int currentSelection = textBox.SelectionStart;
            textBox.SelectAll();
            textBox.SelectionColor = Color.Black;

            string[] opcodes = Enum.GetNames(typeof(Opcode));
            //Generates query containing all opcodes from enum
            string query = opcodes.Aggregate((a, b) => a + "|" + b)
                //Adds regex for comments
                + "|(\\;)(?:.*)"
                //Adds regex for operands
                + "|R\\d|#\\d{1,3}|\\d{1,3}";
            MatchCollection matches = Regex.Matches(textBox.Text, query);

            //For each match, identify the type of match (operand, comment) and colour it accordingly
            foreach (Match match in matches)
            {
                textBox.Select(match.Index, match.Length);
                if (match.Value[0] == ';')
                    textBox.SelectionColor = Color.Blue;
                else if (match.Value[0] == 'R' && Char.IsDigit(match.Value[1]))
                    textBox.SelectionColor = Color.FromArgb(0x56a79e);
                else if (match.Value[0] == '#' && Char.IsDigit(match.Value[1]))
                    textBox.SelectionColor = Color.FromArgb(0x647a7f);
                else if (int.TryParse(match.Value, out int output))
                    textBox.SelectionColor = Color.FromArgb(0xcb5b3e);
                else if (int.TryParse(match.Value, out output))
                    textBox.SelectionColor = Color.FromArgb(0xcb5b3e);
                else
                    textBox.SelectionColor = Color.FromArgb(0x288cd2);
            }

            //Revert to old position
            textBox.SelectionStart = currentSelection;
            textBox.SelectionLength = 0;
            textBox.SelectionColor = Color.Black;
        }
    }
}
