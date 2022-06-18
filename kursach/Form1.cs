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
        Point original; // локация точки, при нажатии мышью
        public static Rectangle SelectedRectangle; // выбранная пользователем область 
        public IntPtr Hwd; // выбранное пользователем окно
        public Form1(IntPtr hwd) // форма для выбора области
        {
            Hwd = hwd;
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);

            new Button { Text = "Close me", Parent = this }.Click += (o, e) => Application.Exit();
            this.FormBorderStyle = FormBorderStyle.None;
            TopMost = true;
            ShowInTaskbar = false;
            WindowState = FormWindowState.Maximized;
            BackgroundImage = Shoot();
        }
        private Bitmap Shoot() // для бэкграунда формы
        {
            Rect bounds = default;
            User32.GetWindowRect(Hwd, ref bounds);
            var bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (var gr = Graphics.FromImage(bmp))
                gr.CopyFromScreen(bounds.Left, bounds.Top, bounds.Left, bounds.Top, new Size(bounds.Right - bounds.Left, bounds.Bottom - bounds.Top));
            return bmp;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            original = e.Location;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (SelectedRectangle.Width > 0 && SelectedRectangle.Height > 0)
            {
                CupruteOne.SelectedRectangle = SelectedRectangle;
                SelectedRectangle.Size = Size.Empty;
                Invalidate();
            }
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
