using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace kursach
{
    // структура для корректной работы работы GetWindowRect
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        private int left;
        private int top;
        private int right;
        private int bottom;
        public int Left
        {
            get { return left; }
            set { left = value; }
        }
        public int Top
        {
            get { return top; }
            set { top = value; }
        }
        public int Right
        {
            get { return right; }
            set { right = value; }
        }
        public int Bottom
        {
            get { return bottom; }
            set { bottom = value; }
        }
        public int Height
        {
            get { return bottom - top; }
            set { bottom = value + top; }
        }
        public int Width
        {
            get { return right - left; }
            set { right = value + left; }
        }
    }
    public class User32
    {
        public delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool GetWindowRect(IntPtr hwnd, out Rect rectangle); // создает Rect-прямоугольник на основе окна 
        
        [DllImport("USER32.DLL")]
        public static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("USER32.DLL")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("USER32.DLL")]
        public static extern IntPtr GetShellWindow();
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        public static bool CheckSize(Rect rc)
        {
            return rc.Width != 0 || rc.Height != 0;
        }

        public static Bitmap PrintWindow(IntPtr hwnd)
        {
            Rect rc;
            GetWindowRect(hwnd, out rc);
            if (CheckSize(rc)) // если окно существует
            {
                Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppRgb);
                Graphics gfxBmp = Graphics.FromImage(bmp);
                IntPtr hdcBitmap = gfxBmp.GetHdc();

                PrintWindow(hwnd, hdcBitmap, 0);

                gfxBmp.ReleaseHdc(hdcBitmap);
                gfxBmp.Dispose();

                return bmp;
            }
            else // если окно не существует
            {
                Stream.Stop();
                return new Bitmap(1, 1);
            }
        }
    }
}
