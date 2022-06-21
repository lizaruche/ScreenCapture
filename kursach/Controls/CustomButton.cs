using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursach
{
    public class CustomButton : Control
    {

        #region -- Свойства -- 

        private Font font = new Font("Verdana", 13F, FontStyle.Bold);
        [Description("Шрифт")]
        public Font NewFont
        {
            get => font;
            set
            {
                font = value;
                Refresh();
            }
        }

        private Color backColor = Color.FromArgb(240, 147, 43); // оранжевый цвет
        [Description("Цвет фона")]
        public Color NewBackColor
        {
            get => backColor;
            set
            {
                backColor = value;

                Refresh();
            }
        }

        private Color animColor = Color.White;
        [Description("Цвет анимации")]
        public Color AnimColor
        {
            get => animColor;
            set
            {
                animColor = value;

                Refresh();
            }
        }

        private Color borderColor = Color.Black;
        [Description("Цвет обводки (границы) кнопки")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;

                Refresh();
            }
        }

        private bool borderColorEnabled = false;
        [Description("Указывает, включено ли использование отдельного цвета обводки (границы) кнопки")]
        public bool BorderColorEnabled
        {
            get => borderColorEnabled;
            set
            {
                borderColorEnabled = value;

                Refresh();
            }
        }

        private int borderSize = 1;
        [DisplayName("BorderSize")]
        [DefaultValue(1)]
        [Description("Размер границы")]
        public int BorderSize
        {
            get => borderSize;
            set
            {
                if (value >= 0 && value <= Size.Height)
                {
                    borderSize = value;

                    Refresh();
                }
            }
        }

        private bool roundingEnable = false;
        [Description("Вкл/Выкл закругление объекта")]
        public bool RoundingEnable
        {
            get => roundingEnable;
            set
            {
                roundingEnable = value;

                Refresh();
            }
        }

        private int roundingPercent = 100;
        [DisplayName("Rounding [%]")]
        [DefaultValue(100)]
        [Description("Степень закругления в %")]
        public int Rounding
        {
            get => roundingPercent;
            set
            {
                if(value >=0 && value <= 100)
                {
                    roundingPercent = value;

                    Refresh();
                }
            }
        }

        #endregion

        #region -- Переменные --

        private StringFormat SF = new StringFormat();

        private bool MouseEntered = false;

        private bool MousePressed = false;

        Animation CurtainButtonAnim = new Animation();

        #endregion

        public CustomButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            Size = new Size(100, 30);

            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;

            CurtainButtonAnim.Value = 0;
        }

        private void ButtonCurtainAction()
        {
            if (MouseEntered)
            {
                CurtainButtonAnim = new Animation("ButtonCurtain_" + Handle, Invalidate, CurtainButtonAnim.Value, Width - 1);
            }
            else
            {
                CurtainButtonAnim = new Animation("ButtonCurtain_" + Handle, Invalidate, CurtainButtonAnim.Value, 0);
            }

            CurtainButtonAnim.StepDivider = 8;
            Animator.Request(CurtainButtonAnim, true);
        }
        #region -- События --
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            CurtainButtonAnim.Value = 0;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.HighQuality;

            graph.Clear(Parent.BackColor);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            Rectangle curtainRect = new Rectangle(0, 0, (int)CurtainButtonAnim.Value, Height - 1);

            // Настройка текста
            ForeColor = Color.White;
            Font = NewFont;

            //Закругление
            float roundingValue = 0.1F;
            if (roundingEnable && Rounding > 0)
            {
                roundingValue = Height / 100F * Rounding;
            }
            GraphicsPath rectPath = Drawer.RoundedRectangle(rect, roundingValue);



            // Отрисовка кнопки (основной прямоугольник)
            graph.DrawPath(new Pen(NewBackColor), rectPath);
            graph.FillPath(new SolidBrush(NewBackColor), rectPath);

            // Отрисовка границы
            if (BorderColorEnabled)
            {
                graph.DrawPath(new Pen(BorderColor, BorderSize), rectPath);
            }
            graph.SetClip(rectPath);


            // Отрисовка анимации (рисуем доп. прямоугольник)
            graph.DrawRectangle(new Pen(Color.FromArgb(60, AnimColor)), curtainRect);
            graph.FillRectangle(new SolidBrush(Color.FromArgb(60, AnimColor)), curtainRect);




            // Выделение кнопки черным при нажатии
            if (MousePressed == true)
            {
                graph.DrawRectangle(new Pen(Color.FromArgb(60, Color.Black)), rect);
                graph.FillRectangle(new SolidBrush(Color.FromArgb(60, Color.Black)), rect);
            }

            graph.DrawString(Text, Font, new SolidBrush(ForeColor), rect, SF);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MousePressed = true;

            CurtainButtonAnim.Value = CurtainButtonAnim.TargetValue;

            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            MousePressed = false;

            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            MouseEntered = true;

            ButtonCurtainAction();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            MouseEntered = false;

            ButtonCurtainAction();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
        }
        #endregion
    }
}
