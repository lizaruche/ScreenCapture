using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursach
{
    public partial class Form1 : Form
    {
        public Point original; // локация точки, при нажатии мышью
        public static Rectangle SelectedRectangle; // выбранная пользователем область 
        public IntPtr Hwd; // выбранное пользователем окно
        public Point realCorner; // Запоминает положение окна, если оно на другом мониторе
        public bool isOnOtherDesktop;
        public Form1(IntPtr hwd) // форма для выбора области
        {
            Hwd = hwd;
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);

            this.FormBorderStyle = FormBorderStyle;
            TopMost = true;
            ShowInTaskbar = false;
            WindowState = FormWindowState.Maximized;
            BackgroundImage = Shoot();
            new CustomButton { Text = "Закрыть", Parent = this,NewFont = new Font("Verdana",8,FontStyle.Regular), Location = original,BorderColorEnabled = true,BorderColor=Color.White,BorderSize = 1,Size = new Size(100,20), NewBackColor = Color.FromArgb(230,0,0,0), ForeColor = Color.White}.Click += (o, e) =>
            {
                this.Close();
                Cursor.Clip = Rectangle.Empty;
                Stream.Stop();
            };
        }
        private Bitmap Shoot() // для бэкграунда формы
        {
            Rectangle bounds = default;
            
            User32.Rect bounds_rect = default;
            User32.GetWindowRect(Hwd, out bounds_rect);
            bounds = User32.RectToRectangle(bounds_rect);
            isOnOtherDesktop = bounds.X < 0 || bounds.X > System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width || bounds.Y < 0 || bounds.Y > System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height || bounds.Y + bounds.Height > System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height || bounds.X + bounds.Width > System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width;
            // Если приложение не на основном мониторе
            if (isOnOtherDesktop)
            {
                original = new Point(0, 0); // Будем строить изображение в левом верхнем углу
                Cursor.Position = original; // перемещение курсора в левый верхний угол
                Cursor.Clip = new Rectangle(0, 0, bounds.Width, bounds.Height); // ограничение для перемещения курсора
                realCorner = new Point(bounds.Left, bounds.Top);
            }
            else // Если на основном
            {
                original = new Point(bounds.Left, bounds.Top); // Левый верхний угол окна
                realCorner = original;
                Cursor.Position = original; // перемещение курсора в левый верхний угол
                Cursor.Clip = new Rectangle(bounds.Left, bounds.Top, bounds.Width, bounds.Height); // ограничение для перемещения курсора
            }
            
            Bitmap bmp = Stream.PrintWindow(Hwd, bounds); // bmp - изображение приложения

            var bmpFullScreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height); // изображение всего экрана

            using(var gr = Graphics.FromImage(bmpFullScreen)) 
                gr.DrawImage(bmp, original.X, original.Y); // перенос скрина приложения на пустое изображение экрана
            return bmpFullScreen;
        }
        protected virtual void OnMouseEnter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);
        }

        protected virtual void OnMouseLeave(object sender, EventArgs e)
        {
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            original = e.Location;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (SelectedRectangle.Width > 0 && SelectedRectangle.Height > 0)
            {
                if (isOnOtherDesktop)
                {
                    FrameTimer.SelectedRectangle = new Rectangle(SelectedRectangle.X + realCorner.X, SelectedRectangle.Y + realCorner.Y, SelectedRectangle.Width, SelectedRectangle.Height);
                }
                else
                {
                    FrameTimer.SelectedRectangle = SelectedRectangle;
                }
                SelectedRectangle.Size = Size.Empty;
                Invalidate();
            }
            Cursor.Clip = Rectangle.Empty;
            this.Close();
        }

        Rectangle GetSelRectangle(Point orig, Point location) // получаем выделенный прямоугольник
        {
            int deltaX = location.X - orig.X;
            int deltaY = location.Y - orig.Y;
            Size s = new Size(Math.Abs(deltaX), Math.Abs(deltaY));
            Rectangle rect = new Rectangle();
            if (deltaX >= 0 & deltaY >= 0)
                rect = new Rectangle(orig, s);
            if (deltaX < 0 & deltaY > 0)
                rect = new Rectangle(location.X, orig.Y, s.Width, s.Height);
            if (deltaX < 0 & deltaY < 0)
                rect = new Rectangle(location, s);
            if (deltaX > 0 & deltaY < 0)
                rect = new Rectangle(orig.X, location.Y, s.Width, s.Height);
            return rect;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            SelectedRectangle = GetSelRectangle(original, e.Location);

            if (MouseButtons == MouseButtons.Left)
                if (SelectedRectangle.Width > 5 && SelectedRectangle.Height > 5)
                {
                    Invalidate();
                }
        }
        protected override void OnPaint(PaintEventArgs e) // закрашивает область
        {
            var r = new Region(ClientRectangle);
            r.Exclude(SelectedRectangle);
            using (var brush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
                e.Graphics.FillRegion(brush, r);
        }
    }
}
