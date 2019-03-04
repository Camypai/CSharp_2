using System;
using System.IO;
using CompGame.Interfaces;

namespace CompGame.Models
{
    public class FileLog<T> : ILog where T : class
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        private readonly string _name;
        
        /// <summary>
        /// Конструктор для записы лога в файл
        /// </summary>
        /// <param name="fileName">Путь или имя файла, в который записывать лог</param>
        public FileLog(string fileName)
        {
            _name = fileName;
        }
        
        /// <summary>
        /// Записывает лог в файл
        /// </summary>
        /// <param name="message">Сообщение для записи</param>
        public void Write(string message)
        {
            using (var sw = new StreamWriter(_name, true))
            {
                sw.WriteLine($"{nameof(T)}: {message}");
            }
        }
    }
}