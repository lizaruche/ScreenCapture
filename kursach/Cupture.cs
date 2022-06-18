using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Windows.Forms;

namespace kursach
{
    public class CupruteOne
    {
        // Выбранная пользователем область
        public static Rectangle SelectedRectangle;
        /// <summary>
        /// Копирование изображения с области окна приложения 
        /// </summary>
        /// <param name="hwd"> хэндл окна приложения </param>
        public static void SelectedRect(IntPtr hwd)
        {
            Rect boundsOfWindow = default; // объект структуры для метода getwindowrect (с Rectangle забирает лишнюю область, поэтому Rect)
            User32.GetWindowRect(hwd, ref boundsOfWindow); // достает размеры окна

            Point topLeftDif = new Point(SelectedRectangle.X - boundsOfWindow.Left, SelectedRectangle.Y - boundsOfWindow.Top); // разница левого верхнего угла 

            Size botRightDif = new Size(boundsOfWindow.Right - boundsOfWindow.Left - SelectedRectangle.Width, boundsOfWindow.Bottom - boundsOfWindow.Top - SelectedRectangle.Height); // разница в ширине и высоте окна и выбранной области 



            var timer = new Timer() { Interval = 40 }; // создаем объект Timer и устанавливаем интервал на 40 мс
            timer.Tick += (s, e) => // на тик таймера сохраняем изображение и вызываем метод для отправки его на сервер
            {
                User32.GetWindowRect(hwd, ref boundsOfWindow); // трэчит размер окна
                int windowWidth = boundsOfWindow.Right - boundsOfWindow.Left; // ширина окна
                int windowHeight = boundsOfWindow.Bottom - boundsOfWindow.Top; // высота окна
                Size b = new Size(windowWidth - botRightDif.Width, windowHeight - botRightDif.Height); // трэчит размеры окна

                if (b.Width > 0 && b.Height > 0)  
                {
                    Bitmap bitmap = new Bitmap(b.Width, b.Height); // формируем объект 
                    Graphics g1 = Graphics.FromImage(bitmap); // формируем поверхность для рисования на объекте Bitmap
                    g1.CopyFromScreen(boundsOfWindow.Left + topLeftDif.X, boundsOfWindow.Top + topLeftDif.Y, 0, 0, b); // копируем изображение с экрана
                    try
                    {
                        SendToServ(bitmap); // отправляем на сервер
                    }
                    catch
                    {

                    }
                    g1.Dispose(); // удаляем средство рисования
                } // else вывести уедомление, чтобы пользователь сделал окно побольше, а то захватывать нечего
            };
            timer.Start(); // запускаем таймер

        }
        /// <summary>
        /// Отправляет Bitmap объект на сервер 
        /// </summary>
        /// <param name="img"> Bitmap для отправки</param>
        static void SendToServ(Bitmap img)
        { 
            MemoryStream ms = new MemoryStream(); // поток для сохранения объекта в jpeg
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // сохранение объекта в поток
            byte[] byteImage = ms.ToArray(); // строковое представление объекта 
            string imgBase64 = Convert.ToBase64String(byteImage); // формирование base64

            WebRequest request = WebRequest.Create("http://127.0.0.1:5000/base64_img"); // сохдание объекта запроса
            request.ContentType = "application/json"; // тип контента в запросе
            request.Method = "POST"; // метод запроса

            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = "{\"base64_img\":" + $"\"{imgBase64}\"," +
                              "\"format\":\"jpeg\"}";

                streamWriter.Write(json); // записывает json в поток запроса
                streamWriter.Flush(); // чиска буферов средства записи
                streamWriter.Close(); // закрытие потока
            }

            WebResponse httpResponse = request.GetResponse(); // возвращает ответ на запрос 
            using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd(); // сохраняем ответ на запрос в переменную result
            }
        }
    }
}
