using System;
using OpenCvSharp;
using RestSharp;
using System.Threading;
using System.Drawing;
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
            Rect boundsOfWindow = default;
            User32.GetWindowRect(hwd, ref boundsOfWindow); // достает размеры окна

            System.Drawing.Size b = new System.Drawing.Size(SelectedRectangle.Width, SelectedRectangle.Height); // Размер окна

            System.Drawing.Point movTrackDif = new System.Drawing.Point(SelectedRectangle.X - boundsOfWindow.Left, SelectedRectangle.Y - boundsOfWindow.Top); // разница левого верхнего угла 

            System.Drawing.Point botLeft = new System.Drawing.Point(boundsOfWindow.Right - SelectedRectangle.Right, boundsOfWindow.Bottom - SelectedRectangle.Bottom);

            Form f = new Form();

            var timer = new System.Windows.Forms.Timer() { Interval = 40 };
            timer.Tick += (s, e) =>
            {
                User32.GetWindowRect(hwd, ref boundsOfWindow); // трэчит размер окна
                b = new System.Drawing.Size(SelectedRectangle.Width, SelectedRectangle.Height); // трэчит размеры окна
                                                                                 //f.MinimumSize = b; // фиксирует размеры формы
                                                                                 //f.MaximumSize = b; // фиксирует размеры формы
                                                                                 //Graphics g = f.CreateGraphics(); // создаем объект графикс для формы
                Bitmap bitmap = new Bitmap(SelectedRectangle.Width, SelectedRectangle.Height);
                Graphics g1 = Graphics.FromImage(bitmap);
                //g.CopyFromScreen(boundsOfWindow.Left + movTrackDif.X, boundsOfWindow.Top + movTrackDif.Y, 0, 0, b); // копируем экран bounds.Left + r.X bounds.Top + r.Y
                g1.CopyFromScreen(boundsOfWindow.Left + movTrackDif.X, boundsOfWindow.Top + movTrackDif.Y, 0, 0, b);
                SendToServ(bitmap);
                //g.Dispose(); // удаляем объект графикс
                g1.Dispose();
            };
            timer.Start();

            Application.Run(f);
        }
        static void SendToServ(Bitmap img)
        {
            Bitmap bImage = img;  // Your Bitmap Image
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            var SigBase64 = Convert.ToBase64String(byteImage); // Get Base64

            var client = new RestClient("http://127.0.0.1:5000/base64_img");
            var request = new RestRequest("http://127.0.0.1:5000/base64_img", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = @"{""base64_img"": " + $@"""{SigBase64}""," + @"""format"": ""jpeg""}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
    static class Program
    {
        [STAThread]
        static void Main()
        {
            //Application.SetHighDpiMode(HighDpiMode.SystemAware); // хз и без этой штуки работает 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Thread.Sleep(2000);
            IntPtr hwd = User32.GetForegroundWindow(); // TODO как-то это оформить в форму
                                                       // достает хэндл активного окна 

            Application.Run(new Form1(hwd));

            CupruteOne.SelectedRect(hwd);
        }
    }
}
