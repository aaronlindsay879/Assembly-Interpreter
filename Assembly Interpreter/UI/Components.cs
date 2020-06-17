using System;
using System.Drawing;
using System.Windows.Forms;

namespace Assembly_Interpreter
{
    partial class MyForm : Form
    {
        void InitComponents()
        {
            //Create all UI elements
            RichTextBox textBox = new RichTextBox
            {
                Size = new Size(215, 480),
                Location = new Point(30, 5),
                Multiline = true,
                Name = "Code",
                BorderStyle = BorderStyle.None,
                Text = "LDR R0,#10; R0=10\nLDR R1,#2; R1=2\nLSL R2,R0,R1\n\nLDR R3,#10\nLDR R4,#20\nADD R3,R3,R4\nMOV R4,R3\nSTR R4,#20\n\nBGE R2,20,#15\nHALT\n\n\n\nMOV R7,R2"
            };

            RichTextBox lineNumbers = new RichTextBox
            {
                Size = new Size(20, 480),
                Location = new Point(10, 5),
                Multiline = true,
                Name = "LineNumbers",
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = textBox.BackColor,
                SelectionProtected = true,
                Enabled = false
            };

            Label label = new Label
            {
                Size = new Size(100, 480),
                Location = new Point(25, 5),
                BackColor = textBox.BackColor
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
                lineNumbers.Text += i.ToString().PadLeft(2, '0') + "\n";

            //Add all elements to screen
            Controls.Add(textBox);
            Controls.Add(label);
            Controls.Add(lineNumbers);
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
    }
}
