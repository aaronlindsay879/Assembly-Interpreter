﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Assembly_Interpreter
{
    partial class MyForm : Form
    {
        public void UpdateScreen(object source, EventArgs e)
        {
            Controls["mem"].Text = "Memory:\n" + memory.CreateOutput();
            Controls["reg"].Text = "Registers:\n" + registers.CreateOutput(1);

            if (currentInstruction >= 0)
            {
                RichTextBox textBox = (RichTextBox)Controls["LineNumbers"];

                ClearAllHighlighting("LineNumbers");

                textBox.SelectionStart = 3 * currentInstruction;
                textBox.SelectionLength = 3;
                textBox.SelectionBackColor = Color.Red;
            }
        }

        public void ClearAllHighlighting(string textbox)
        {
            RichTextBox textBox = (RichTextBox)Controls[textbox];

            textBox.SelectionStart = 0;
            textBox.SelectionLength = 3 * numLines;
            textBox.SelectionBackColor = textBox.BackColor;
        }
    }
}
