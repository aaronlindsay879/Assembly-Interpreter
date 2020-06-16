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

        public MyForm()
        {
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
        }

        void InitComponents()
        {
            //Create all UI elements
            TextBox textBox = new TextBox
            {
                Size = new Size(240, 500),
                Location = new Point(5, 5),
                Multiline = true,
                Name = "Code"
            };

            Button button = new Button
            {
                Size = new Size(80, 40),
                Location = new Point(5, 505),
                Text = "Run code",
                Name = "RunCode"
            };

            Button buttonTwo = new Button
            {
                Size = new Size(80, 40),
                Location = new Point(85, 505),
                Text = "Stop code",
                Name = "StopCode"
            };

            Label delayLabel = new Label
            {
                Size = new Size(80, 20),
                Location = new Point(165, 505),
                Text = "Delay (s)"
            };

            NumericUpDown delay = new NumericUpDown
            {
                Size = new Size(80, 20),
                Location = new Point(165, 525),
                Increment = 0.1M,
                DecimalPlaces = 1,
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

            //Add all elements to screen
            Controls.Add(textBox);
            Controls.Add(button);
            Controls.Add(buttonTwo);
            Controls.Add(delayLabel);
            Controls.Add(delay);
            Controls.Add(memory);
            Controls.Add(registers);
            Size = new System.Drawing.Size(1000, 600);
        }

        public void RunCode_Click(object sender, EventArgs e)
        {
            //Get the text from the textbox, and split on every newline
            string textBox = Controls["Code"].Text;
            string[] splitText = textBox.Split(new[] { "\r\n" }, StringSplitOptions.None);

            //Convert all the strings into commands
            Command[] commands = splitText.Select(x => new Command(x)).ToArray();

            float.TryParse(Controls["Delay"].Text, out float delay);
            program = new Program(commands);
            
            //Create a thread which will execute commands one by one
            runThread = new Thread(() => program.Execute(ref memory, ref registers, delay));
            runThread.Start();
        }
        public void StopCode_Click(object sender, EventArgs e)
        {
            runThread.Abort();
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
