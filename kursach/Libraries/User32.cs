using System;
using System.Runtime.InteropServices;

namespace kursach
{
    // структура для корректной работы работы GetWindowRect
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
    public class User32
    {
        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle); // создает Rect-прямоугольник на основе окна 
    }
}
