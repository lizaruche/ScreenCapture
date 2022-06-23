using System;
using System.IO;
using System.Net;
using System.Drawing;

namespace kursach
{
    public class Stream
    {
        /// <summary>
        /// таймер для отправки кадров на сервер с интервалом 40 мс
        /// </summary>
        readonly static FrameTimer timer = new FrameTimer() { Interval = 40 };
        
        /// <summary>
        /// Начать стрим
        /// </summary>
        /// <param name="hwd"> хэндл окна для захвата </param>
        public static void Start(IntPtr hwd)
        {
            FrameTimer.SelectedRect(hwd);
            timer.Start();   
        }
        /// <summary>
        /// Остановаить стрим
        /// </summary>
        public static void Stop()
        {
            timer.Stop();
            Form2.StreamIsRunning = false;
            ClearFrames();
        }
        /// <summary>
        /// Отправляет Bitmap объект на сервер 
        /// </summary>
        /// <param name="img"> Bitmap для отправки</param>
        public static void SendToServ(Bitmap img)
        { 

            WebRequest request = WebRequest.Create("http://127.0.0.1:8080/base64_img"); // сохдание объекта запроса
            request.ContentType = "application/json"; // тип контента в запросе
            request.Method = "POST"; // метод запроса

            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = "{\"base64_img\":" + $"\"{ConvertToBase64(img)}\"," +
                              "\"format\":\"jpeg\"}";

                streamWriter.Write(json); // записывает json в поток запроса
                streamWriter.Flush(); // чистка буферов средства записи
                streamWriter.Close(); // закрытие потока
            }

            WebResponse response = request.GetResponse(); // возвращает ответ на запрос 
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd(); // сохраняем ответ на запрос в переменную result
            }
        }
        private static void ClearFrames()
        {
            try
            {
                WebRequest request = WebRequest.Create("http://127.0.0.1:8080/clear_frames"); // сохдание объекта запроса
                request.Method = "GET"; // метод запроса

                WebResponse response = request.GetResponse(); // возвращает ответ на запрос 
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd(); // сохраняем ответ на запрос в переменную result
                }
            }
            catch (WebException) { }
            
        }
        private static string ConvertToBase64(Bitmap img)
        {
            MemoryStream ms = new MemoryStream(); // поток для сохранения объекта в jpeg
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // сохранение объекта в поток
            byte[] byteImage = ms.ToArray(); // строковое представление объекта 
            return Convert.ToBase64String(byteImage); // формирование base64
        }
        public static Bitmap PrintWindow(IntPtr hwnd, Rectangle rc, bool fullWindow = true)
        {
            User32.Rect rect;
            User32.GetWindowRect(hwnd, out rect); // получить Rect от размера всего приложения
            Bitmap newBitmap = default; // Битмап для области приложения
            var rectangle = User32.RectToRectangle(rect); // перевод из Rect в Rectangle

            if (User32.CheckSize(rc)) // если окно существует
            {
                if (fullWindow)
                {
                    newBitmap = new Bitmap(rectangle.Width, rectangle.Height);
                    Graphics gfxBmp = Graphics.FromImage(newBitmap);
                    IntPtr hdcBitmap = gfxBmp.GetHdc();

                    User32.PrintWindow(hwnd, hdcBitmap, 0);

                    gfxBmp.ReleaseHdc(hdcBitmap);
                    gfxBmp.Dispose();

                    return newBitmap;
                }
                else
                {
                    Bitmap bmp = new Bitmap(rectangle.Width, rectangle.Height);
                    Graphics gfxBmp = Graphics.FromImage(bmp);
                    IntPtr hdcBitmap = gfxBmp.GetHdc();
                    User32.PrintWindow(hwnd, hdcBitmap, 0);
                    gfxBmp.ReleaseHdc(hdcBitmap);
                    gfxBmp.Dispose();

                    if (bmp.Width <= rc.X || bmp.Height <= rc.Y)
                    {
                        return new Bitmap(1, 1);
                    }
                    else if (bmp.Width <= rc.Width + rc.X || bmp.Height <= rc.Height + rc.Y)
                    {
                        rc.Width = bmp.Width - rc.X;
                        rc.Height = bmp.Height - rc.Y;
                        newBitmap = bmp.Clone(rc, bmp.PixelFormat);
                    }
                    else
                    {
                        newBitmap = bmp.Clone(rc, bmp.PixelFormat); // вырезание части из bitmap
                    }
                    return newBitmap;
                }
            }
            else // если окно не существует
            {
                Stop();
                return new Bitmap(1, 1);
            }
        }
    }   
}
