using System;
using CompGame.Interfaces;

namespace CompGame.Models
{
    public class ConsoleLog<T> : ILog where T : class
    {
        /// <summary>
        /// Записывает лог в консоль
        /// </summary>
        /// <param name="message">Сообщение для записи</param>
        public void Write(string message)
        {
            Console.WriteLine($"{(typeof(T).FullName)}: {message}");
        }
    }
}