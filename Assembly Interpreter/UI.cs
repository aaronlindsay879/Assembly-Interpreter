﻿using Assembly_Interpreter;
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
    class MyForm : Form
    {
        private DataStorage memory;
        private DataStorage registers;
        private Program program;
        private Thread runThread;
        private int currentInstruction;
        private System.Windows.Forms.Timer timer;

        public MyForm()
        {
            currentInstruction = -1;
            memory = new DataStorage(100, true);
            registers = new DataStorage(8, true);
            InitComponents();
            SyntaxHighlighting("code");

            //Create timer to update screen every 0.1 seconds
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(UpdateScreen);
            timer.Interval = 100;
            timer.Enabled = true;
        }

        public void UpdateScreen(object source, EventArgs e)
        {
            Controls["mem"].Text = "Memory:\n" + memory.CreateOutput();
            Controls["reg"].Text = "Registers:\n" + registers.CreateOutput(1);

            if (currentInstruction >= 0)
            {
                RichTextBox textBox = (RichTextBox)Controls["LineNumbers"];

                ClearAllHighlighting("LineNumbers");

                textBox.SelectionStart = 2 * currentInstruction;
                textBox.SelectionLength = 2;
                textBox.SelectionBackColor = Color.Red;
            }
        }

        public void ClearAllHighlighting(string textbox)
        {
            RichTextBox textBox = (RichTextBox)Controls[textbox];
            
            textBox.SelectionStart = 0;
            textBox.SelectionLength = 60;
            textBox.SelectionBackColor = Color.Transparent;
        }

        void InitComponents()
        {
            //Create all UI elements
            RichTextBox lineNumbers = new RichTextBox
            {
                Size = new Size(20, 480),
                Location = new Point(10, 5),
                Multiline = true,
                Name = "LineNumbers",
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White
            };

            RichTextBox textBox = new RichTextBox
            {
                Size = new Size(215, 480),
                Location = new Point(30, 5),
                Multiline = true,
                Name = "Code",
                BorderStyle = BorderStyle.None,
                Text = "LDR R0,#10; R0=10\nLDR R1,#2; R1=2\nLSL R2,R0,R1\n\nLDR R3,#10\nLDR R4,#20\nADD R3,R3,R4\nMOV R4,R3\nSTR R4,#20\n\nBGE R2,20,#15\nHALT\n\n\n\nLDR R7,#10"
            };

            Button button = new Button
            {
                Size = new Size(90, 30),
                Location = new Point(5, 485),
                Text = "Run code",
                Name = "RunCode"
            };

            Button buttonTwo = new Button
            {
                Size = new Size(90, 30),
                Location = new Point(95, 485),
                Text = "Stop code",
                Name = "StopCode"
            };

            Button buttonThree = new Button
            {
                Size = new Size(100, 80),
                Location = new Point(855, 450),
                Text = "Clear memory/\nregisters",
                Name = "StopCode"
            };

            Label delayLabel = new Label
            {
                Size = new Size(50, 20),
                Location = new Point(195, 485),
                Text = "Delay (s)"
            };

            NumericUpDown delay = new NumericUpDown
            {
                Size = new Size(50, 20),
                Location = new Point(195, 505),
                Increment = 0.1M,
                DecimalPlaces = 1,
                Value = 0.5M,
                Name = "Delay"
            };

            Button openFile = new Button
            {
                Size = new Size(90, 30),
                Location = new Point(5, 520),
                Text = "Open file",
                Name = "openFile"
            };

            Button saveFile = new Button
            {
                Size = new Size(90, 30),
                Location = new Point(95, 520),
                Text = "Save",
                Name = "save"
            };

            Label memory = new Label
            {
                AutoSize = true,
                Text = "Memory:\n" + this.memory.CreateOutput(),
                Font = new Font(FontFamily.GenericMonospace.Name, 8),
                Location = new Point(250, 0),
                Name = "mem"
            };

            Label registers = new Label
            {
                AutoSize = true,
                Text = "Registers:\n" + this.registers.CreateOutput(1),
                Font = new Font(FontFamily.GenericMonospace.Name, 8),
                Location = new Point(850, 0),
                Name = "reg"
            };

            //Add event listeners to run code and stop code buttons
            button.Click += new EventHandler(RunCode_Click);
            buttonTwo.Click += new EventHandler(StopCode_Click);
            buttonThree.Click += new EventHandler(Reset_Click);
            openFile.Click += new EventHandler(OpenFile_Click);
            saveFile.Click += new EventHandler(SaveFile_Click);
            textBox.KeyDown += new KeyEventHandler(Code_KeyPress);
            delay.ValueChanged += new EventHandler(Delay_Changed);

            //Adds line numbers to textbox
            for (int i = 0; i < 30; i++)
                lineNumbers.Text += i.ToString().PadLeft(2, '0');

            //Add all elements to screen
            Controls.Add(lineNumbers);
            Controls.Add(textBox);
            Controls.Add(button);
            Controls.Add(buttonTwo);
            Controls.Add(buttonThree);
            Controls.Add(openFile);
            Controls.Add(saveFile);
            Controls.Add(delayLabel);
            Controls.Add(delay);
            Controls.Add(memory);
            Controls.Add(registers);

            //General window setup
            ActiveControl = textBox;
            Size = new System.Drawing.Size(1000, 600);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Text = "Assembly Interpreter";
        }

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
                    new Thread(() => ThreadHandler(err)).Start();
                }
            });
            runThread.Start();
        }

        public void ThreadHandler(ArgumentException err)
        {
            //Kill thread that threw error
            runThread.Abort();
            
            MessageBox.Show(err.Message, "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        public void Delay_Changed(object sender, EventArgs e)
        {
            float.TryParse(Controls["delay"].Text, out float delay);

            //Constrain at 0.1 to avoid division by zero errors
            delay = Math.Max(0.1f, delay);

            //Update memory/registers twice as frequently as instructions are executed
            timer.Interval = (int)(delay / 2 * 1000);
        }

        private static void EventHandler(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(EventHandler);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.Run(new MyForm());
        }
    }
}
