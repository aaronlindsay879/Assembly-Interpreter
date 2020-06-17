using System;
using System.Threading;
using System.Windows.Forms;

namespace Assembly_Interpreter
{
    partial class MyForm : Form
    {
        public void ThreadExceptionHandler(ArgumentException err)
        {
            //Kill thread that threw error
            runThread.Abort();
            Controls["RunCode"].Enabled = true;
            ((RichTextBox)Controls["Code"]).ReadOnly = false;

            MessageBox.Show(err.Message, "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static void EventHandler(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
