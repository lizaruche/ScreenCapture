﻿using System;
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
            int windowWidth = boundsOfWindow.Width; // ширина окна
            int windowHeight = boundsOfWindow.Height; // высота окна
            Size b = new Size(windowWidth - botRightDif.Width, windowHeight - botRightDif.Height); // трэчит размеры окна
            Size windowSize = new Size(windowWidth, windowHeight); // берет размеры окна

            if (b.Width > 0 && b.Height > 0 || Form2.CaptureFullScreen)
            {
                Bitmap bitmap;

                if (Form2.CaptureFullScreen)
                {
                    bitmap = User32.PrintWindow(hwd); // формируем объект 

                    //bitmap = new Bitmap(windowSize.Width, windowSize.Height); // формируем объект 
                    //Graphics g1 = Graphics.FromImage(bitmap); // формируем поверхность для рисования на объекте Bitmap
                    //g1.CopyFromScreen(boundsOfWindow.Left, boundsOfWindow.Top, 0, 0, windowSize);
                    //g1.Dispose(); // удаляем средство рисования
                }
                else
                {
                    bitmap = User32.PrintWindow(hwd); // формируем объект 

                    //bitmap = new Bitmap(b.Width, b.Height); // формируем объект 
                    //Graphics g1 = Graphics.FromImage(bitmap); // формируем поверхность для рисования на объекте Bitmap
                    //g1.CopyFromScreen(boundsOfWindow.Left + topLeftDif.X, boundsOfWindow.Top + topLeftDif.Y, 0, 0, b); // копируем изображение с экрана
                    //g1.Dispose(); // удаляем средство рисования
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

                            Form2.StreamIsRunning = false;
                        }
                    }
                    Stream.Stop();
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
