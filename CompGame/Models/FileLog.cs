using System;
using System.IO;
using CompGame.Interfaces;

namespace CompGame.Models
{
    public class FileLog<T> : ILog, IDisposable where T : class
    {
        private StreamWriter _streamWriter;

        /// <summary>
        /// Конструктор для записы лога в файл
        /// </summary>
        /// <param name="fileName">Путь или имя файла, в который записывать лог</param>
        public FileLog(string fileName)
        {
            _streamWriter = new StreamWriter(fileName, true);
        }

        /// <summary>
        /// Записывает лог в файл
        /// </summary>
        /// <param name="message">Сообщение для записи</param>
        public void Write(string message)
        {
            _streamWriter.WriteLine($"{nameof(T)}: {message}");
        }

        public void Dispose()
        {
            _streamWriter.Close();
            _streamWriter.Dispose();
        }
    }
}