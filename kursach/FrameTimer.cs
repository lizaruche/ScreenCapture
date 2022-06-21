using System;
using System.Windows.Forms;
using System.Net;
using System.Drawing;

namespace kursach
{
    class FrameTimer : Timer
    {
        private static Rectangle boundsOfWindow;
        private static Point topLeftDif;
        private static Size botRightDif;
        private static IntPtr hwd;
        public static Rectangle SelectedRectangle;
        bool MsgBoxIsDisplayed = false;
        
        protected override void OnTick(EventArgs e)
        {
            User32.Rect boundsOfWindow_rect;
            User32.GetWindowRect(hwd, out boundsOfWindow_rect); // трэчит размер окна
            boundsOfWindow = User32.RectToRectangle(boundsOfWindow_rect); // Перевод из Rect в Rectangle

            if (boundsOfWindow.Width == 0 && boundsOfWindow.Height == 0) // если размер (0;0) - окно закрыто
            {
                Stream.Stop();
            }
            else
            {
                int windowWidth = boundsOfWindow.Width; // ширина окна
                int windowHeight = boundsOfWindow.Height; // высота окна

                Size selectedRectangleSize = new Size(SelectedRectangle.Width,SelectedRectangle.Height); // трэчит размеры окна
                
                if (selectedRectangleSize.Width > 0 && selectedRectangleSize.Height > 0 || Form2.CaptureFullScreen)
                {
                    Bitmap bitmap;
                    if (Form2.CaptureFullScreen)
                    {
                        bitmap = User32.PrintWindow(hwd); // формируем объект 
                    }
                    else
                    {
                        bitmap = User32.PrintWindow(hwd, new Rectangle(topLeftDif,selectedRectangleSize));
                    }
                        

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
            User32.Rect boundsOfWindow_rect;
            User32.GetWindowRect(hwd, out boundsOfWindow_rect); // достает размеры окна
            boundsOfWindow = User32.RectToRectangle(boundsOfWindow_rect); // Перевод из Rect в Rectangle

            topLeftDif = new Point(SelectedRectangle.X - boundsOfWindow.Left, SelectedRectangle.Y - boundsOfWindow.Top); // разница левого верхнего угла 

            botRightDif = new Size(boundsOfWindow.Right - boundsOfWindow.Left - SelectedRectangle.Width, boundsOfWindow.Bottom - boundsOfWindow.Top - SelectedRectangle.Height); // разница в ширине и высоте окна и выбранной области 
        }
    }
}
