using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace kursach
{
    public class User32
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

        public delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);
        // winapi для вывода окна
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // winapi для вывода окна на передний план
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool GetWindowRect(IntPtr hwnd, out Rect rectangle); // создает Rect-прямоугольник на основе окна
        //public static bool GetWindowRectUpdate(IntPtr hwnd, out Rect new_rectangle)
        //{
        //    Rect rectangle;
        //    GetWindowRect(hwnd, out rectangle);

        //    new_rectangle = new Rect();
        //    new_rectangle.Left = rectangle.Left - 7;
        //    new_rectangle.Top = rectangle.Top;
        //    new_rectangle.Width = rectangle.Width + 14;
        //    new_rectangle.Height = rectangle.Height + 7;
        //    return true;
        //}
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

        public static bool CheckSize(Rectangle rc)
        {
            return rc.Width != 0 || rc.Height != 0;
        }
        public static Rectangle RectToRectangle(Rect rc)
        {
            return new Rectangle(rc.Left, rc.Top, rc.Width, rc.Height);
        }
    }
}
