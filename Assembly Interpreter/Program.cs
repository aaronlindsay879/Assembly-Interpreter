using Assembly_Interpreter.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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

        public MyForm()
        {
            currentInstruction = -1;
            memory = new DataStorage(100, true);
            registers = new DataStorage(8, true);
            InitComponents();

            //Create timer to update screen every 0.1 seconds
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(UpdateScreen);
            timer.Interval = 100;
            timer.Enabled = true;
        }

        public void UpdateScreen(object source, EventArgs e)
        {
            Controls["mem"].Text = "Memory:\n" + memory.CreateOutput();
            Controls["reg"].Text = "Registers:\n" + registers.CreateOutput(1);

            if (currentInstruction != -1)
            {
                RichTextBox textBox = (RichTextBox)Controls["LineNumbers"];

                ClearAllHighlighting();

                textBox.SelectionStart = 2 * currentInstruction;
                textBox.SelectionLength = 2;
                textBox.SelectionBackColor = Color.Red;
            }
        }

        public void ClearAllHighlighting()
        {
            RichTextBox textBox = (RichTextBox)Controls["LineNumbers"];
            
            textBox.SelectionStart = 0;
            textBox.SelectionLength = 60;
            textBox.SelectionBackColor = Color.Transparent;
        }

        void InitComponents()
        {
            //Create all UI elements
            RichTextBox lineNumbers = new RichTextBox
            {
                Size = new Size(25, 500),
                Location = new Point(5, 5),
                Multiline = true,
                Name = "LineNumbers",
                ReadOnly = true
            };

            RichTextBox textBox = new RichTextBox
            {
                Size = new Size(215, 500),
                Location = new Point(30, 5),
                Multiline = true,
                Name = "Code"
            };

            Button button = new Button
            {
                Size = new Size(60, 50),
                Location = new Point(5, 505),
                Text = "Run\r\n code",
                Name = "RunCode"
            };

            Button buttonTwo = new Button
            {
                Size = new Size(60, 50),
                Location = new Point(65, 505),
                Text = "Stop\r\n code",
                Name = "StopCode"
            };

            Button buttonThree = new Button
            {
                Size = new Size(60, 50),
                Location = new Point(125, 505),
                Text = "Reset",
                Name = "StopCode"
            };

            Label delayLabel = new Label
            {
                Size = new Size(50, 20),
                Location = new Point(195, 505),
                Text = "Delay (s)"
            };

            NumericUpDown delay = new NumericUpDown
            {
                Size = new Size(50, 20),
                Location = new Point(195, 525),
                Increment = 0.1M,
                DecimalPlaces = 1,
                Value = 0.5M,
                Name = "Delay"
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

            //Adds line numbers to textbox
            for (int i = 0; i < 30; i++)
                lineNumbers.Text += i.ToString().PadLeft(2, '0');

            //Add all elements to screen
            Controls.Add(lineNumbers);
            Controls.Add(textBox);
            Controls.Add(button);
            Controls.Add(buttonTwo);
            Controls.Add(buttonThree);
            Controls.Add(delayLabel);
            Controls.Add(delay);
            Controls.Add(memory);
            Controls.Add(registers);
            Size = new System.Drawing.Size(1000, 620);

            //Ensures correct item is selected
            ActiveControl = textBox;
        }

        public void RunCode_Click(object sender, EventArgs e)
        {
            currentInstruction = 0;

            //Get the text from the textbox, and split on every newline
            string textBox = Controls["Code"].Text.Trim();
            string[] splitText = textBox.Split(new[] { "\n" }, StringSplitOptions.None);

            //Convert all the strings into commands
            Command[] commands = splitText.Select(x => new Command(x)).ToArray();

            float.TryParse(Controls["Delay"].Text, out float delay);
            program = new Program(commands);
            
            //Create a thread which will execute commands one by one
            runThread = new Thread(() => program.Execute(ref memory, ref registers, ref currentInstruction, delay));
            runThread.Start();
        }

        public void StopCode_Click(object sender, EventArgs e)
        {
            runThread.Abort();
        }

        public void Reset_Click(object sender, EventArgs e)
        {
            StopCode_Click(sender, e);
            memory.SetToZero();
            registers.SetToZero();

            ClearAllHighlighting();
            currentInstruction = 0;
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MyForm());
        }
    }
}
