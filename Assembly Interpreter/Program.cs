using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembly_Interpreter
{
    class MyForm : Form
    {
        public MyForm()
        {
            InitComponents();
        }

        void InitComponents()
        {
            Label label = new Label
            {
                AutoSize = true,
                Text = new Command("LDR R1,300").ToString()
            };

            Controls.Add(label);
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
