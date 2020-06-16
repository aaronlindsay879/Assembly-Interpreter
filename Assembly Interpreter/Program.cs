using Assembly_Interpreter.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembly_Interpreter
{
    class MyForm : Form
    {
        private DataStorage memory;
        private DataStorage registers;
        private Program program;

        public MyForm()
        {
            memory = new DataStorage(100, true);
            registers = new DataStorage(8, true);
            program = new Program(new Command("LDR R0,#2"), new Command("LDR R1,#5"), new Command("ADD R2,R0,R1"), new Command("STR R2,0"));
            Debug.WriteLine(registers);

            InitComponents();

            Task.Run(() => program.Execute(ref memory, ref registers));

            Timer timer = new Timer();
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = 100;
            timer.Enabled = true;
        }

        public void OnTimedEvent(object source, EventArgs e)
        {
            Controls["mem"].Text = "Memory:\n" + memory.CreateOutput();
            Controls["reg"].Text = "Registers:\n" + registers.CreateOutput(1);
        }

        void InitComponents()
        {
            TextBox textBox = new TextBox
            {
                Size = new Size(240, 500),
                Location = new Point(5, 5),
                Multiline = true
            };

            Button button = new Button
            {
                Size = new Size(100, 40),
                Location = new Point(5, 505),
                Text = "Run code"
            };

            Button buttonTwo = new Button
            {
                Size = new Size(100, 40),
                Location = new Point(105, 505),
                Text = "Stop code"
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

            Controls.Add(textBox);
            Controls.Add(button);
            Controls.Add(buttonTwo);
            Controls.Add(memory);
            Controls.Add(registers);
            Size = new System.Drawing.Size(1000, 600);
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
