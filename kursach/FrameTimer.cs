using System;
using System.Windows.Forms;
using System.Net;
using System.Drawing;

namespace kursach
{
    class FrameTimer : Timer
    {
        private static Rect boundsOfWindow;
        private static Point topLeftDif;
        private static Size botRightDif;
        private static IntPtr hwd;
        public static Rectangle SelectedRectangle;
        bool MsgBoxIsDisplayed = false;
        
        protected override void OnTick(EventArgs e)
        {
            User32.GetWindowRect(hwd, out boundsOfWindow); // трэчит размер окна
            if (boundsOfWindow.Width == 0 && boundsOfWindow.Height == 0) // если размер (0;0) - окно закрыто
            {
                Stream.Stop();
            }
            else
            {
                int windowWidth = boundsOfWindow.Width; // ширина окна
                int windowHeight = boundsOfWindow.Height; // высота окна
                Size windowSize = new Size(windowWidth - botRightDif.Width, windowHeight - botRightDif.Height); // трэчит размеры окна

                if (windowSize.Width > 0 && windowSize.Height > 0 || Form2.CaptureFullScreen)
                {
                    Bitmap bitmap;

                    bitmap = User32.PrintWindow(hwd); // формируем объект 

                    try { Stream.SendToServ(bitmap); } // отправляем на сервер
                    catch (WebException)
                    {
                        if (MsgBoxIsDisplayed == false)
                        {
                            MsgBoxIsDisplayed = true;
                            if (MessageBox.Show("Ошибка при подключении к серверу. Трансляция остановлена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                MsgBoxIsDisplayed = false;
                            }
                        }
                        Stream.Stop();
                    }
                }
            }
        }
        public static void SelectedRect(IntPtr hwd)
        {
            FrameTimer.hwd = hwd;

            User32.GetWindowRect(hwd, out boundsOfWindow); // достает размеры окна

            topLeftDif = new Point(SelectedRectangle.X - boundsOfWindow.Left, SelectedRectangle.Y - boundsOfWindow.Top); // разница левого верхнего угла 

            botRightDif = new Size(boundsOfWindow.Right - boundsOfWindow.Left - SelectedRectangle.Width, boundsOfWindow.Bottom - boundsOfWindow.Top - SelectedRectangle.Height); // разница в ширине и высоте окна и выбранной области 
        }
    }
}
