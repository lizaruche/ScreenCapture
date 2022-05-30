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
        public IntPtr Hwd;
        public Form1()
        {
            InitializeComponent();
        }
        public Form1(IntPtr hwd)
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
        private Bitmap Shoot()
        {
            Rect bounds = default;
            User32.GetWindowRect(Hwd, ref bounds);
            var bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (var gr = Graphics.FromImage(bmp))
                gr.CopyFromScreen(bounds.Left, bounds.Top, bounds.Left, bounds.Top, new Size(bounds.Right - bounds.Left, bounds.Bottom - bounds.Top));
            return bmp;
        }

        public static Rectangle SelectedRectangle;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            SelectedRectangle.Location = e.Location;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (SelectedRectangle.Width > 0 && SelectedRectangle.Height > 0)
            {
                CupruteOne.SelectedRectangle = SelectedRectangle;
                SelectedRectangle.Size = Size.Empty;
                Invalidate();
                //Application.Exit();
                //BackgroundImage = null;
                //CupruteOne.SelectedRect(Hwd);
            }
            //SelectedRectangle.Size = Size.Empty;
            //CupruteOne.SelectedRect(Hwd); // метод для передачи изображения
            this.Close();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            var newSize = new Size(e.X - SelectedRectangle.Left, e.Y - SelectedRectangle.Top);

            if (MouseButtons == MouseButtons.Left)
                if (newSize.Width > 5 && newSize.Height > 5)
                {
                    SelectedRectangle.Size = newSize;
                    Invalidate();
                }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var r = new Region(ClientRectangle);
            r.Exclude(SelectedRectangle);
            using (var brush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
                e.Graphics.FillRegion(brush, r);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
