using System;
using System.Drawing;
using static System.Drawing.Image;

namespace kursach
{
    public class Shot
    {
        /// <summary>
        /// Делает сриншот окна не работает с дс и хромом (мб вообще со всеми не winforms приложениями)
        /// </summary>
        /// <param name="hwd"> хэндл окна (GetForegroundWindow)</param>
        /// <returns> скрин в битмапе </returns>
        public static Bitmap GetWindowShot(IntPtr hwd)
        {

            IntPtr hBitmap;

            // Достаем дискриптор DC на основе дискриптора окна
            IntPtr hDC = User32.GetDC(hwd);


            IntPtr hMemDC = Gdi32.CreateCompatibleDC(hDC);

            // Достаем размеры окна
            Rect bounds = default;
            User32.GetWindowRect(hwd, ref bounds);



            int width = bounds.Right - bounds.Left;

            //We pass SM_CYSCREEN constant to GetSystemMetrics to get the
            //Y coordinates of the screen.
            int height = bounds.Bottom - bounds.Top;

            // Создаем хэндлер BitMap объекта
            hBitmap = Gdi32.CreateCompatibleBitmap
                        (hDC, width, height);

            // Проверяем на пустой (логично)
            if (hBitmap != IntPtr.Zero)
            {
                //Here we select the compatible bitmap in the memeory device
                //context and keep the refrence to the old bitmap.
                IntPtr hOld = (IntPtr)Gdi32.SelectObject
                                       (hMemDC, hBitmap);
                //We copy the Bitmap to the memory device context. SRCCOPY - код растровой операции
                Gdi32.BitBlt(hMemDC, 0, 0, width, height, hDC,
                                           0, 0, Gdi32.SRCCOPY);
                //We select the old bitmap back to the memory device context.
                Gdi32.SelectObject(hMemDC, hOld);
                //We delete the memory device context.
                Gdi32.DeleteDC(hMemDC);
                //We release the screen device context.
                User32.ReleaseDC(hwd, hDC);
                //Image is created by Image bitmap handle and stored in
                //local variable.
                Bitmap bmp = FromHbitmap(hBitmap);
                //Release the memory to avoid memory leaks.
                Gdi32.DeleteObject(hBitmap);
                //This statement runs the garbage collector manually.
                GC.Collect();
                //Return the bitmap 
                return bmp;
            }
            return null;
        }
        /// <summary>
        /// Создаем скриншот выбранной области окна
        /// </summary>
        /// <param name="bounds"> Область окна для захвата </param>
        /// <returns> Объект Bitmap </returns>
        public static Bitmap GetScreenShot(Rect bounds)
        {
            int width = bounds.Right - bounds.Left;
            int height = bounds.Bottom - bounds.Top;
            Size size = new Size(width, height);

            Bitmap btp = new Bitmap(width, height);
            Graphics gr = Graphics.FromImage(btp);

            gr.CopyFromScreen(bounds.Left, bounds.Top, 0, 0, size);

            return btp;
        }
    }
}
