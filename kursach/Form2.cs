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

        IntPtr hwd;
        int selected_index;
        string selected_name;
        public Form2()
        {
            InitializeComponent();
            

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            WindowsList = OpenWindowGetter.GetOpenWindows();
            foreach (var item in WindowsList)
            {
                comboBox1.Items.Add(item.Value.ToString());
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if(selected_index == -1)
            {
                MessageBox.Show("Выберите окно, которое надо записать", "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
            else
            {
                if (WindowsList.Values.Contains(selected_name))
                {
                    hwd = WindowsList.ToArray()[selected_index].Key; // берет индекс выбранного элемента и по ключу находит его в списке окон
                    ShowWindow(WindowsList.ToArray()[selected_index].Key, 4); // выводит выбранное окно
                    SetForegroundWindow(WindowsList.ToArray()[selected_index].Key); // на передний план

                    Thread.Sleep(150);

                    button2.Visible = false; // убирается кнопка запуска стрима
                    button4.Visible = true; // появляется кнопка остановки стрима

                    Form1 form1 = new Form1(hwd); // открывается форма для скриншота
                    form1.ShowDialog();
                    form1.Close();
                    CupruteOne.SelectedRect(hwd);

                }
                else
                {
                    MessageBox.Show("Выбранного окна не существует", "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                }
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

        private void button4_Click(object sender, EventArgs e) // выключить стрим
        {
            button4.Visible = false; // убрать кнопку стоп стрим
            // сюда логику выключения стрима
            button2.Visible = true; // вернуть кнопку старт стрим
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1 sec интервал между обновлениями
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected_index = comboBox1.SelectedIndex;
            selected_name = comboBox1.Items[selected_index].ToString();
        }
    }
}
