using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DeepS
{
    public partial class Form1 : Form
    {

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int WM_SETTEXT = 0X000C;

        public IntPtr dwindowHandler;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e, KeyPressEventArgs t)
        {
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pasteDiscord(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            { 
                Process thisProcess = Process.GetCurrentProcess();
                Process DL = Process.GetProcessesByName("DeepL")[0];

                progressBar1.Value = 10;

                Clipboard.SetText(this.richTextBox1.Text);
                string clipboardBuffer = Clipboard.GetText();

                ShowWindow(DL.MainWindowHandle, 1);

                progressBar1.Value = 20;

                bool success = SetForegroundWindow(DL.MainWindowHandle);

                Clipboard.Clear();
                SendKeys.Send("^(a)");
                SendKeys.Send("^(c)");

                if (!string.IsNullOrEmpty(Clipboard.GetText()))
                {
                    SendKeys.Send("^(a)");
                    SendKeys.Send("{BS}");
                    Clipboard.Clear();
                    SendKeys.Send("#");
                    SendKeys.Send("{BS}");
                }

                Clipboard.SetText(clipboardBuffer);
                SendKeys.Send("{BS}");
                SendKeys.Send("^(v)");

                progressBar1.Value = 30;

                System.Threading.Thread.Sleep(1500);

                SendKeys.Send("{TAB}");
                SendKeys.Send("^(a)");
                SendKeys.Send("^(c)");

                progressBar1.Value = 40;

                string translatedText = Clipboard.GetText();

                progressBar1.Value = 50;

                SendKeys.Send("+{TAB}");
                SendKeys.Send("^(a)");
                SendKeys.Send("{BS}");

                progressBar1.Value = 60;

                this.translationBox.Text = Clipboard.GetText();

                progressBar1.Value = 70;

                bool duoSuccess = SetForegroundWindow(dwindowHandler);

                progressBar1.Value = 80;

                SendKeys.Send("^(v)");
                SendKeys.Send("~");

                progressBar1.Value = 90;

                bool trioSucess = SetForegroundWindow(thisProcess.MainWindowHandle);
                this.richTextBox1.Clear();

                progressBar1.Value = 100;

                SendKeys.Send("{BS}");

                progressBar1.Value = 0;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            System.Threading.Thread.Sleep(5000);
            dwindowHandler = GetForegroundWindow();
            this.dWID.Text = GetForegroundWindow().ToString();
            this.WindowState = FormWindowState.Normal;
        }

        private void dWID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void translationBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
