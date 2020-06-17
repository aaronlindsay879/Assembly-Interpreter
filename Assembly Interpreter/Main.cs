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
            ClearAllHighlighting("LineNumbers");

            //Create timer to update screen every 0.1 seconds
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(UpdateScreen);
            timer.Interval = 100;
            timer.Enabled = true;
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
