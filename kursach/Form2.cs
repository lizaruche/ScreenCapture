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
        public static List<Process> WindowsList = new List<Process>(); // Список всех открытых окон
        public static bool StreamIsRunning { get; set; } // Проверка на то идет ли стрим
        public static bool CaptureFullScreen { get; set; } = false; // Выбор захватывать весь экран или область

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        IntPtr hwd;
        int selected_index;
        string selected_name;
        Process selected_process;

        delegate void WindowProc(IntPtr hWnd, IntPtr lParam);

        public Form2()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeComponent();

            Animator.Start();

            customComboBox1.Refresh();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 250; // 1 sec интервал между обновлениями
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        public static void RefreshWindowsList()
        {
            WindowsList.Clear();
            WindowsList = Process.GetProcesses().Where(proc => proc.MainWindowTitle != "" && proc.MainWindowHandle != IntPtr.Zero).ToList();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            SwitchStopStartButton();
        }

        private void SwitchStopStartButton()
        {
            if (!StreamIsRunning)
            {
                customButton1.Visible = false; // убрать кнопку стоп стрим
                customButton2.Visible = true; // вернуть кнопку старт стрим
            }
            else
            {
                customButton2.Visible = false; // убрать кнопку старт стрим
                customButton1.Visible = true; // вернуть кнопку стоп стрим
            }
        }

        private void customComboBox1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if(customComboBox1.SelectedIndex != -1)
            {
                selected_index = customComboBox1.SelectedIndex;
                selected_name = customComboBox1.SelectedItem.ToString();
                selected_process = WindowsList.Where(x=>x.MainWindowTitle == selected_name).FirstOrDefault();
            }
        }

        private void customButton2_Click_1(object sender, EventArgs e) // Включить стрим
        {
            if (selected_index == -1)
            {
                MessageBox.Show("Выберите окно, которое надо записать", "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
            else
            {
                if (customComboBox1.Items.Contains(selected_name))
                {
                    hwd = selected_process.MainWindowHandle;

                    //ShowWindow(hwd, 4); // выводит выбранное окно
                    //SetForegroundWindow(hwd); // на передний план
                    //Thread.Sleep(250); // Пауза перед скрином, чтобы окно открылось

                    customButton1.Visible = true; // появляется кнопка остановки стрима
                    customButton2.Visible = false; // убирается кнопка запуска стрима
                    StreamIsRunning = true;

                    if (!CaptureFullScreen) // Если выбрана опция стримить часть приложения
                    {
                        Form1 form1 = new Form1(hwd); // открывается форма для скриншота
                        form1.ShowDialog();
                        form1.Close();
                    }
                    Stream.Start(hwd);
                }
                else
                {
                    MessageBox.Show("Выбранного окна не существует", "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                }
            }
        }

        private void customButton1_Click_1(object sender, EventArgs e) // Остановить стрим
        {
            customButton1.Visible = false; // убрать кнопку стоп стрим
            customButton2.Visible = true; // вернуть кнопку старт стрим
            StreamIsRunning = false;

            Stream.Stop();
        }

        private void customButton3_Click(object sender, EventArgs e) // Выход
        {
            DialogResult res = new DialogResult();
            res = MessageBox.Show("Вы действительно хотите выйти?", "Выход из программы", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
                Close();
            else
                return;
        }

        private void customToggleSwitch1_CheckedChanged(object sender)
        {
            if (customToggleSwitch1.Checked)
            {
                CaptureFullScreen = true;
            }
            else
            {
                CaptureFullScreen = false;
            }
        }
    }
}
