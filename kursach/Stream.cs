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
        }
        /// <summary>
        /// Отправляет Bitmap объект на сервер 
        /// </summary>
        /// <param name="img"> Bitmap для отправки</param>
        public static void SendToServ(Bitmap img)
        { 

            WebRequest request = WebRequest.Create("http://127.0.0.1:5000/base64_img"); // сохдание объекта запроса
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
        private static string ConvertToBase64(Bitmap img)
        {
            MemoryStream ms = new MemoryStream(); // поток для сохранения объекта в jpeg
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // сохранение объекта в поток
            byte[] byteImage = ms.ToArray(); // строковое представление объекта 
            return Convert.ToBase64String(byteImage); // формирование base64
        }
    }   
}
