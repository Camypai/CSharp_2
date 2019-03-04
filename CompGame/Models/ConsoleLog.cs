using System;
using CompGame.Interfaces;

namespace CompGame.Models
{
    public class ConsoleLog<T> : ILog where T : class
    {
        public void Write(string message)
        {
            Console.WriteLine($"{(typeof(T).FullName)}: {message}");
        }
    }
}