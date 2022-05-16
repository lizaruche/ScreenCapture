using System;
using System.Runtime.InteropServices;

namespace kursach
{
    public class Gdi32
    {
        public const int SRCCOPY = 13369376; // код расровой опирации для метода BitBlt
                                             // Копирует исходный прямоугольник непосредственно в целевой прямоугольник
        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern IntPtr DeleteDC(IntPtr hDc); // удаляет указанный DC

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern IntPtr DeleteObject(IntPtr hDc); // удаляет растровое избражение (или перо, кисть, шрифт),
                                                              // освобождает все системные ресурсы, связанные с объектом
                                                              // после удаления указанный дискриптор больше не действителен

        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        public static extern bool BitBlt(IntPtr hdcDest, int xDest, // выполняет побитовую передачу данных о цвете
            int yDest, int wDest, int hDest, IntPtr hdcSource,
            int xSrc, int ySrc, int RasterOp);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, // создает растровое изображение, совместимое с устройством 
            int nWidth, int nHeight);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc); // создает memory DC совместимый с указанным устройством 

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp); // выбирает объект 
    }
}
