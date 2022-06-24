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
        public static bool StreamIsRunning { get; set; } // Проверка на то идет ли стрим
        public static bool CaptureFullScreen { get; set; } = false; // Выбор захватывать весь экран или область

        public Form2()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeComponent();
            
            Animator.Start();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 250; // 1 sec интервал между обновлениями
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
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


        private void customButton2_Click_1(object sender, EventArgs e) // Включить стрим
        {
            form3.RefreshGrid();
            form3.Show();
            Stream.Address = textBox1.Text;
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
            {
                Stream.Stop();
                Close();
            }   
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                customButton2.Enabled = false;
                customButton2.NewBackColor = Color.Gray;
            }
            else
            {
                customButton2.Enabled = true;
                customButton2.NewBackColor = Color.FromArgb(240, 147, 43);
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            form3.Close();
        }
    }
}
