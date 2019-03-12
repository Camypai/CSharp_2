using System;
using System.Drawing;
using CompGame.Interfaces;

namespace CompGame.Models
{
    public class LineCreator : Creator<Line>
    {
        private ILog _log = new ConsoleLog<LineCreator>();
        
        /// <summary>
        /// Создаёт линию со случайными параметрами
        /// </summary>
        /// <param name="rnd">Экземпляр класса Random</param>
        /// <returns>Линия</returns>
        public override Line Create(Random rnd)
        {
            _log.Write("Вызов конструктора линии");
            
            return new Line(new Point(rnd.Next(Scene.Width), rnd.Next(0, Scene.Height)), new Point(-80, 0),
                new Size(20, 0));
        }
    }
}