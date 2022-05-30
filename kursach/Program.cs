using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// Работает осталось понять че тут происходит https://www.codeproject.com/Articles/3024/Capturing-the-Screen-Image-in-C
#region Библиотеки DLL
// Доки майков по библиотекам https://docs.microsoft.com/ru-ru/dotnet/framework/interop/consuming-unmanaged-dll-functions
// GDI32.dll - Функции интерфейса графических устройств (GDI) для вывода информации на устройство, например функции для рисования и управления шрифтами.
// User32.dll - Функции управления Windows для обработки сообщений, таймеров, меню и обмена данными.
// Kernel32.dll	- Низкоуровневые функции операционной системы для управления памятью и обработки ресурсов.
#endregion
#region Заметки по теории
// GDI-атрибуты - это шрифт, цвета, размер клиентской области нужные для работы DC(device context)-функций
// DC - механизм для одновременнового вывода множества источников, структура данных, описывающая устройство отображения информации 
// DC - логическое устройство вывода, посредством которого осуществляется вывод информации на устройство вывода (дисплей, принтер, память).
// DC-функции возвращают/предоставляют приложению дескриптор DC(Device Context Descriptor)
// В винде разные типы DC, здесь используется DC отображения (display), window display context
// Дескриптор DC - это указатель, с помощью которого приложение получает доступ к устройству вывода
// Дескриптор контекста устройства отображения воспринимается в качестве указателя на некую системную структуру в памяти ядра,
//через параметры которой системе задаются критерии вывода данных на физическое устройство отображения
#endregion
#region Юзфул ссылки
// Как получить Handle Окна при наведении мышкой https://www.cyberforum.ru/csharp-net/thread635343.html
// Saving a screenshot of a window using C#, WPF, and DWM https://stackoverflow.com/questions/1858122/saving-a-screenshot-of-a-window-using-c-wpf-and-dwm/1897285#1897285
// Как получить изображение свёрнутого окна? https://www.cyberforum.ru/csharp-net/thread1494328.html
// Как получить изображение окна другого приложения? https://www.cyberforum.ru/csharp-net/thread1385397.html
// Про контекст устройства отображения http://datadump.ru/device-context/#:~:text=%D0%94%D0%B5%D1%81%D0%BA%D1%80%D0%B8%D0%BF%D1%82%D0%BE%D1%80%20(%D0%BE%D0%BF%D0%B8%D1%81%D0%B0%D1%82%D0%B5%D0%BB%D1%8C)%20%D0%BA%D0%BE%D0%BD%D1%82%D0%B5%D0%BA%D1%81%D1%82%D0%B0%20%D0%BE%D1%82%D0%BE%D0%B1%D1%80%D0%B0%D0%B6%D0%B5%D0%BD%D0%B8%D1%8F%2C,%D0%B2%D1%8B%D0%B2%D0%BE%D0%B4%D0%B0%20(%D0%BD%D0%B0%D0%BF%D1%80%D0%B8%D0%BC%D0%B5%D1%80%2C%20%D1%8D%D0%BA%D1%80%D0%B0%D0%BD).
// Работа с окнами на Плюсах WinAPI https://www.youtube.com/watch?v=606YiqwS9WU
// Работает осталось понять че тут происходит https://www.codeproject.com/Articles/3024/Capturing-the-Screen-Image-in-C
// Про прямую трансляцию через Azure, не факт, что это то, что нужно https://docs.microsoft.com/ru-ru/azure/media-services/latest/stream-live-tutorial-with-api
#endregion

namespace kursach
{
    public class CupruteOne
    {
        public static Rectangle SelectedRectangle;
        public static void SelectedRect(IntPtr hwd)
        {
            Rect bounds = default;
            User32.GetWindowRect(hwd, ref bounds); // достает размеры окна
            Size b = new Size(Form1.SelectedRectangle.Width, Form1.SelectedRectangle.Height); // Размер окна
            Point r = new Point(SelectedRectangle.X - bounds.Left, SelectedRectangle.Y - bounds.Top);
            Point botLeft = new Point(bounds.Right - SelectedRectangle.Right, bounds.Bottom - SelectedRectangle.Bottom);
            Form f = new Form();

            var timer = new System.Windows.Forms.Timer() { Interval = 40 };
            timer.Tick += (s, e) =>
            {
                User32.GetWindowRect(hwd, ref bounds); // трэчит размер окна
                b = new Size(SelectedRectangle.Width, SelectedRectangle.Height); // трэчит размеры окна
                f.MinimumSize = b; // фиксирует размеры формы
                f.MaximumSize = b; // фиксирует размеры формы
                Graphics g = f.CreateGraphics(); // создаем объект графикс на основе формы
                g.CopyFromScreen(bounds.Left + r.X, bounds.Top + r.Y, 0, 0, b); // копируем экран bounds.Left + r.X bounds.Top + r.Y
                g.Dispose(); // удаляем объект графикс
            };
            timer.Start();

            Application.Run(f);
        }
    }
    static class Program
    {
        [STAThread]
        static void Main()
        {
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Thread.Sleep(2000);
            IntPtr hwd = User32.GetForegroundWindow();

            Form1 d = new Form1(hwd);
            Application.Run(d);

            CupruteOne.SelectedRect(hwd);
        }
    }
}
