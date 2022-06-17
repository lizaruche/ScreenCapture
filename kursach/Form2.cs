using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using HWND = System.IntPtr;

namespace kursach
{
    public partial class Form2 : Form
    {
        IDictionary<IntPtr, string> WindowsList = new Dictionary<IntPtr, string>();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        static void BringWindowToFront()
        {
            var currentProcess = Process.GetCurrentProcess();
            var processes = Process.GetProcessesByName(currentProcess.ProcessName);
            var process = processes.FirstOrDefault(p => p.Id != currentProcess.Id);
            if (process == null) return;

            SetForegroundWindow(process.MainWindowHandle);
        }
        IntPtr hwd;
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hwd = WindowsList.ToArray()[comboBox1.SelectedIndex].Key;
            ShowWindow(WindowsList.ToArray()[comboBox1.SelectedIndex].Key, 4);
            SetForegroundWindow(WindowsList.ToArray()[comboBox1.SelectedIndex].Key);

            Thread.Sleep(100);
            if (hwd != IntPtr.Zero)
            {
                Form1 form1 = new Form1(hwd);
                form1.ShowDialog();
                form1.Close();
                CupruteOne.SelectedRect(hwd);
            }
            else
            {
                MessageBox.Show("Выберите окно, которое надо записать", "Ошибка", buttons: MessageBoxButtons.OK);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WindowsList = OpenWindowGetter.GetOpenWindows();
            foreach(var item in WindowsList)
            {
                comboBox1.Items.Add(item.Value.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res = new DialogResult();
            res = MessageBox.Show("Вы действительно хотите выйти?","Выход из программы",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
                Close();
            else
                return; 
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
