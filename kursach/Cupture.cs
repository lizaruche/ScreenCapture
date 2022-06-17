using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Windows.Forms;

namespace kursach
{
    public class CupruteOne
    {
        public static Rectangle SelectedRectangle;
        public static void SelectedRect(IntPtr hwd)
        {
            Rect boundsOfWindow = default;
            User32.GetWindowRect(hwd, ref boundsOfWindow); // достает размеры окна

            Size b = new Size(SelectedRectangle.Width, SelectedRectangle.Height); // Размер окна

            Point movTrackDif = new Point(SelectedRectangle.X - boundsOfWindow.Left, SelectedRectangle.Y - boundsOfWindow.Top); // разница левого верхнего угла 

            Point botLeft = new Point(boundsOfWindow.Right - SelectedRectangle.Right, boundsOfWindow.Bottom - SelectedRectangle.Bottom);


            var timer = new Timer() { Interval = 40 }; // создаем объект Timer и устанавливаем интервал на 40 мс
            timer.Tick += (s, e) => // на тик таймера сохраняем изображение и вызываем метод для отправки его на сервер
            {
                User32.GetWindowRect(hwd, ref boundsOfWindow); // трэчит размер окна
                b = new Size(SelectedRectangle.Width, SelectedRectangle.Height); // трэчит размеры окна

                Bitmap bitmap = new Bitmap(SelectedRectangle.Width, SelectedRectangle.Height); // формируем объект 
                Graphics g1 = Graphics.FromImage(bitmap); // формируем поверхность для рисования на объекте Bitmap
                g1.CopyFromScreen(boundsOfWindow.Left + movTrackDif.X, boundsOfWindow.Top + movTrackDif.Y, 0, 0, b); // копируем изображение с экрана
                try
                {
                    SendToServ(bitmap); // отправляем на сервер
                }
                catch
                {

                }
                g1.Dispose(); // удаляем средство рисования
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
