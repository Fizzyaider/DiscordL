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
using System.Windows.Input;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DeepS
{
    public partial class Form1 : Form
    {
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int GetKeyState(System.Windows.Forms.Keys vKey);

        const int WM_SETTEXT = 0X000C;
        const int WM_GETTEXT = 0x000D;

        public static IntPtr dwindowHandler;

        public static bool reBirth = false;

        public bool DiscordShortcut = false;

        public int trayPingPong = 0;

        Thread dThread = new Thread(onDemandDiscordHandler);

        public Form1()
        {
            this.Icon = Properties.Resources.DL_ICON;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void NotifyIcon1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            trayPingPong = 1;
            Show();
            //this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.notifyIcon1.Visible = false;
            this.OnDemandMode.Checked = false;
            //dThread.Suspend();
        }

        private void pasteDiscord(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                baseDiscordHandler(this.richTextBox1, this.translationBox, dwindowHandler);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            System.Threading.Thread.Sleep(5000);
            dwindowHandler = GetForegroundWindow();
            this.dWID.Text = GetForegroundWindow().ToString();
            this.WindowState = FormWindowState.Normal;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }

        private void OnDemandMode_CheckedChanged(object sender, EventArgs e)
        {
            this.notifyIcon1.Visible = true;
            if (dThread.IsAlive == false)
            {
                dThread.Start();
            }
            else if (dThread.IsAlive == true && trayPingPong == 1)
            {
                dThread.Suspend();
            }
            else if (dThread.ThreadState == System.Threading.ThreadState.Suspended)
            {
                dThread.Resume();
            }
            if (trayPingPong == 0)
            {
                Hide();
            }
            else if (trayPingPong == 1)
            {
                trayPingPong = 0;
            }
        }

        public void baseDiscordHandler(RichTextBox rich, RichTextBox tranBox, IntPtr dWHandler)
        {
            Process thisProcess = Process.GetCurrentProcess();
            Process DL = Process.GetProcessesByName("DeepL")[0];

            Clipboard.SetText(rich.Text);
            string clipboardBuffer = Clipboard.GetText();

            ShowWindow(DL.MainWindowHandle, 1);

            bool success = SetForegroundWindow(DL.MainWindowHandle);

            Clipboard.Clear();
            SendKeys.Send("^(a)");
            SendKeys.Send("^(c)");

            if (!string.IsNullOrEmpty(Clipboard.GetText()))
            {
                System.Console.WriteLine("DETECTED TEXT\n");
                SendKeys.Send("^(a)");
                SendKeys.Send("{BS}");
                Clipboard.Clear();
                SendKeys.Send("#");
                SendKeys.Send("{BS}");
            }

            Clipboard.SetText(clipboardBuffer);
            SendKeys.Send("{BS}");
            SendKeys.Send("^(v)");

            System.Threading.Thread.Sleep(1500);

            SendKeys.Send("{TAB}");
            SendKeys.Send("^(a)");
            SendKeys.Send("^(c)");

            string translatedText = Clipboard.GetText();

            SendKeys.Send("+{TAB}");
            SendKeys.Send("^(a)");
            SendKeys.Send("{BS}");


            tranBox.Text = Clipboard.GetText();

            ShowWindow(dWHandler, 1);

            bool duoSuccess = SetForegroundWindow(dWHandler);

            SendKeys.Send("^(v)");
            SendKeys.Send("~");

            bool trioSucess = SetForegroundWindow(thisProcess.MainWindowHandle);
            rich.Clear();

            SendKeys.Send("{BS}");
        }

        public static void onDemandDiscordHandler()
        {
            Process DL = Process.GetProcessesByName("DeepL")[0];
            IntPtr dWHandler = Form1.dwindowHandler;
            while (true)
            {
                if (!Control.ModifierKeys.HasFlag(Keys.Control) && !Control.ModifierKeys.HasFlag(Keys.Alt)) {
                    System.Threading.Thread.Sleep(150);
                }
                else if (Control.ModifierKeys.HasFlag(Keys.Control) && Control.ModifierKeys.HasFlag(Keys.Alt))
                {
                    onDemandCode(dWHandler, DL);
                }
            }
        }

        public static void onDemandCode(IntPtr dWH, Process DL)
        {
            System.Threading.Thread.Sleep(100);
            SendKeys.SendWait("^(a)");
            SendKeys.SendWait("^(c)");

            ShowWindow(DL.MainWindowHandle, 1);

            bool success = SetForegroundWindow(DL.MainWindowHandle);

            SendKeys.SendWait("^(a)");
            SendKeys.SendWait("{BS}");
            SendKeys.SendWait("^(v)");

            System.Threading.Thread.Sleep(1250);

            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("^(a)");
            SendKeys.SendWait("^(c)");

            SendKeys.SendWait("+{TAB}");
            SendKeys.SendWait("^(a)");
            SendKeys.SendWait("{BS}");

            ShowWindow(dWH, 1);

            bool duoSuccess = SetForegroundWindow(dWH);

            SendKeys.SendWait("^(a)");
            SendKeys.SendWait("{BS}");
            SendKeys.SendWait("^(v)");
            SendKeys.SendWait("~");
        }
    }
}