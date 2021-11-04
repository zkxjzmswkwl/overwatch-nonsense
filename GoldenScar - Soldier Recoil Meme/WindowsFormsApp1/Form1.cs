using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public string GetForegroundTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr ForegroundHandle = GetForegroundWindow();

            if (GetWindowText(ForegroundHandle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }


        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        public static void Move(int xDelta, int yDelta)
        {
            mouse_event(MOUSEEVENTF_MOVE, xDelta, yDelta, 0, 0);
        }

        public Form1()
        {
            InitializeComponent();
        }

        public void SoldierLoop()
        {
            DateTime start = DateTime.UtcNow;
            TimeSpan deltaTime = TimeSpan.FromMilliseconds(250);

            for ( ; ; )
            {
                if (GetAsyncKeyState(Keys.LButton) < 0 && GetForegroundTitle() == "Overwatch")
                {
                    if (DateTime.UtcNow - start > deltaTime)
                    {
                        start = DateTime.UtcNow;
                        Thread.Sleep(200);
                    }
                    else
                    {
                        Move(0, 4);
                        start = DateTime.UtcNow;
                    }
                }
                Thread.Sleep(9);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SoldierLoop();
        }
    }
}
