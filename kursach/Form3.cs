using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static kursach.User32;
using HWND = System.IntPtr;

namespace kursach
{
    public partial class Form3 : Form
    {
        #region -- Получение статуса окна (свернуто, обычное, максимизирвоанное) --

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public ShowWindowCommands showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }

        internal enum ShowWindowCommands : int
        {
            Hide = 0,
            Normal = 1,
            Minimized = 2,
            Maximized = 3,
        }
        private static WINDOWPLACEMENT GetPlacement(IntPtr hwnd)
        {
            WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);
            GetWindowPlacement(hwnd, ref placement);
            return placement;
        }
        #endregion

        #region -- Получение списка всех окон одного процесса --

        public static Dictionary<IntPtr, string> GetOpenWindowsFromPID(int processID)
        {
            IntPtr hShellWindow = User32.GetShellWindow();
            Dictionary<IntPtr, string> dictWindows = new Dictionary<IntPtr, string>();

            User32.EnumWindows(delegate (IntPtr hWnd, int lParam) // Функция для подсчета кол-ва окон
            {
                if (hWnd == hShellWindow) return true; // Выходим если дескриптор с  рабочего стола
                if (!User32.IsWindowVisible(hWnd)) return true; // Выходим если окно невидимое

                int length = User32.GetWindowTextLength(hWnd); // Если название из 0 букв - выходим
                if (length == 0) return true;

                uint windowPid;
                User32.GetWindowThreadProcessId(hWnd, out windowPid);
                if (windowPid != processID) return true; // Выходим, если ID от процесса окна не совпадает с ID процесса

                StringBuilder stringBuilder = new StringBuilder(length);
                User32.GetWindowText(hWnd, stringBuilder, length + 1); // Сохраняем название
                dictWindows.Add(hWnd, stringBuilder.ToString()); // Добавляем в словарь дескриптор окна с его названием
                return true;
            }, 0);

            return dictWindows;
        }
        #endregion

        #region -- Обработчики событий --
        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            PictureBox picture = (PictureBox)sender;

            if (!pictureIsSelected)
            {
                using (Graphics g = picture.Parent.CreateGraphics())
                {
                    g.DrawRectangle(new Pen(Color.FromArgb(100, 0, 0, 255), 5), picture.Bounds);
                }
            }
        }
        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            PictureBox picture = (PictureBox)sender;

            if (!pictureIsSelected)
            {
                picture.Parent.Invalidate();
            }
        }
        private void pictureBox_MouseClick(object sender, EventArgs e)
        {
            PictureBox picture = (PictureBox)sender;
            Image[] images = pictureBoxes.Select(x => x.Image).ToArray();
            int current_position = Array.IndexOf(images, picture.Image);


            if (pictureIsSelected && current_position != selected_index) // Если картинка уже выбрана, но выбрали другую, то
            {
                picture.Parent.Invalidate(new Rectangle(selected_index % 2 * 300 + 10, 20 + 225 * (int)Math.Floor((float)selected_index / 2), 260, 185));

                using (Graphics g = picture.Parent.CreateGraphics())
                {
                    g.DrawRectangle(new Pen(Color.FromArgb(255, 0, 0, 255), 5), picture.Bounds);
                }
                selected_index = current_position;
            }
            else if (pictureIsSelected && current_position == selected_index) // Если выбрали ту же, то убрать выделение
            {
                selected_index = -1;
                pictureIsSelected = false;
                picture.Parent.Invalidate();
            }
            else // если не была выбрана
            {
                using (Graphics g = picture.Parent.CreateGraphics())
                {
                    g.DrawRectangle(new Pen(Color.FromArgb(255, 0, 0, 255), 5), picture.Bounds);
                }
                pictureIsSelected = true;
                selected_index = current_position;
            }

            if (pictureIsSelected)
            {
                selected_name = names[selected_index];
                selected_process_handle = handles[selected_index];
            }
        }
        private void customButton1_Click(object sender, EventArgs e) // Запуск стрима
        {
            if (selected_index == -1)
            {
                MessageBox.Show("Выберите окно, которое надо записать", "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
            else
            {
                if (selected_name != null && windowsList.Values.Contains(selected_name))
                {
                    hwd = selected_process_handle;

                    var selectedWindowStatement = GetPlacement(selected_process_handle).showCmd.ToString(); // проверяем свернуто или нет

                    if (selectedWindowStatement == "Minimized" || selectedWindowStatement == "Hide")
                    {
                        User32.ShowWindow(hwd, 4); // выводит выбранное окно
                    }
                    Form2.StreamIsRunning = true;

                    if (!Form2.CaptureFullScreen) // Если выбрана опция стримить часть приложения
                    {
                        Form1 form1 = new Form1(hwd); // открывается форма для скриншота
                        form1.ShowDialog();
                        form1.Close();
                    }
                    Stream.Start(hwd);

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Выбранного окна не существует", "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                }
            }
        }
        private void customButton2_Click(object sender, EventArgs e) // Отмена
        {
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!pictureIsSelected)
            {
                customButton1.Enabled = false;
                customButton1.NewBackColor = Color.FromArgb(125, 125, 125);
            }
            else
            {
                customButton1.NewBackColor = Color.FromArgb(240, 147, 43);
                customButton1.Enabled = true;
            }
        }

        #endregion

        #region -- Переменные для работы логики формы --
        public static Dictionary<HWND, string> windowsList = new Dictionary<HWND, string>(); // Список всех открытых окон

        private HWND hwd;

        PictureBox[] pictureBoxes; // Массив со всеми pictureBox'ами
        String[] names; // Массив хранит в себе все названия на экране
        HWND[] handles; // Массив хранит в себе все хэндлы процессов на экране

        private int selected_index = -1;
        private string selected_name;
        private HWND selected_process_handle;
        private bool pictureIsSelected = false;
        #endregion

        #region -- Методы для формы --
        public void RefreshGrid()
        {
            RefreshWindowsList();
            CreatePictureGrid(windowsList);
        }
        public static void RefreshWindowsList()
        {
            windowsList.Clear();
            IEnumerable<Process> allProcesses = Process.GetProcesses().Where(proc => proc.MainWindowTitle != "" && proc.MainWindowHandle != IntPtr.Zero);
            foreach (var process in allProcesses)
            {
                var process_windows = GetOpenWindowsFromPID(process.Id);

                foreach (var window in process_windows)
                {
                    windowsList.Add(window.Key, window.Value);
                }
            }
        }
        private void CreatePictureGrid(Dictionary<HWND, string> allWindows)
        {
            int counter = 0;
            names = new string[allWindows.Count];
            handles = new HWND[allWindows.Count];
            pictureBoxes = new PictureBox[allWindows.Count];

            Rect rect = new Rect();

            foreach (var item in allWindows)
            {
                // Получаем размер картинки
                PictureBox pictureBox = new PictureBox();
                User32.GetWindowRect(item.Key, out rect);
                Rectangle bounds = User32.RectToRectangle(rect);
                Label name = new Label();

                // Параметры картинок
                pictureBox.Parent = panel1;
                pictureBox.Visible = true;
                pictureBox.Size = new Size(250, 175);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Image = Stream.PrintWindow(item.Key, bounds);
                pictureBox.Left = counter % 2 * 300 + 15;
                pictureBox.Top = 25 + 225 * (int)Math.Floor((float)counter / 2);

                //Параметры label
                name.Parent = panel1;
                name.Text = item.Value;
                name.Width = pictureBox.Width;
                name.Font = new Font("Verdana", 11);
                name.Top = pictureBox.Bottom + 15;
                name.Left = pictureBox.Left;
                name.Visible = true;
                name.ForeColor = Color.White;
                name.TextAlign = ContentAlignment.MiddleCenter;

                //Привязка событий
                pictureBox.MouseEnter += pictureBox_MouseEnter;
                pictureBox.MouseLeave += pictureBox_MouseLeave;
                pictureBox.MouseClick += pictureBox_MouseClick;

                // Добавляются  в общий массив картинок
                handles[counter] = item.Key;
                names[counter] = item.Value;
                pictureBoxes[counter] = pictureBox;

                counter++;
            }
        }

        #endregion

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            RefreshGrid();

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 100; // 0.1 sec интервал между обновлениями
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
    }
}
