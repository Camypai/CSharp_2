using System;
using System.IO;
using CompGame.Interfaces;

namespace CompGame.Models
{
    public class FileLog<T> : ILog where T : class
    {
        private readonly string _name;
        
        public FileLog(string fileName)
        {
            _name = fileName;
        }
        
        public void Write(string message)
        {
            using (var sw = new StreamWriter(_name, true))
            {
                sw.WriteLine($"{nameof(T)}: {message}");
            }
        }
    }
}