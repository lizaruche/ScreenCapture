using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursach.Controls
{
    public class PicExt : PictureBox
    {
        private Color _borderColor;
        private int _borderWidth;
        private bool _showBorder;
        [Browsable(true)]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; this.Invalidate(); }
        }

        [Browsable(true)]
        public bool ShowBorder
        {
            get { return _showBorder; }
            set { _showBorder = value; this.Invalidate(); }
        }

        [Browsable(true)]
        public int BorderWidth
        {
            get { return _borderWidth; }
            set { _borderWidth = value; this.Invalidate(); }
        }

        public PicExt()
        {
            _borderColor = Color.FromArgb(0,0,0,255);
            _borderWidth = 3;
            _showBorder = true;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if(ShowBorder == true)
                pe.Graphics.DrawRectangle(new Pen(BorderColor, BorderWidth), 1, 1, this.Size.Width-BorderWidth, this.Size.Height-BorderWidth);
        }
    }
}
