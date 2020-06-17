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
        public void RunCode_Click(object sender, EventArgs e)
        {
            currentInstruction = 0;

            //Clear any potential old hightligting on LineNumbers (ie from error)
            ClearAllHighlighting("LineNumbers");

            //Get the text from the textbox, and split on every newline
            string textBox = Controls["Code"].Text.Trim();
            string[] splitText = textBox.Split(new[] { "\n" }, StringSplitOptions.None);

            //Convert all the strings into commands
            Command[] commands = splitText.Select(x => new Command(x)).ToArray();

            float.TryParse(Controls["Delay"].Text, out float delay);
            program = new Program(commands);

            //Create a thread which will execute commands one by one
            runThread = new Thread(() => {
                try
                {
                    program.Execute(ref memory, ref registers, ref currentInstruction, delay);
                }
                catch (ArgumentException err)
                {
                    //Needed in order to show error after halting thread
                    new Thread(() => ThreadExceptionHandler(err)).Start();
                }
            });
            runThread.Start();
        }

        public void StopCode_Click(object sender, EventArgs e)
        {
            if (runThread != null)
                if (runThread.IsAlive)
                    runThread.Abort();

            currentInstruction = -1;
            ClearAllHighlighting("LineNumbers");
        }

        public void Reset_Click(object sender, EventArgs e)
        {
            StopCode_Click(sender, e);
            memory.SetToZero();
            registers.SetToZero();

            ClearAllHighlighting("LineNumbers");
            currentInstruction = -1;
        }

        public void OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = "*.txt";
            fileDialog.Filter = "TXT Files|*.txt";
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
                ((RichTextBox)Controls["Code"]).Text = File.ReadAllText(fileDialog.FileName);

            SyntaxHighlighting("Code");
        }

        public void SaveFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = "*.txt";
            fileDialog.Filter = "TXT Files|*.txt";
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
                File.WriteAllText(fileDialog.FileName, ((RichTextBox)Controls["Code"]).Text);
        }

        public void Code_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SyntaxHighlighting("Code");
        }

        public void Delay_Changed(object sender, EventArgs e)
        {
            float.TryParse(Controls["delay"].Text, out float delay);

            //Constrain at 0.1 to avoid division by zero errors
            delay = Math.Max(0.1f, delay);

            //Update memory/registers twice as frequently as instructions are executed
            timer.Interval = (int)(delay / 2 * 1000);
        }
    }
}
