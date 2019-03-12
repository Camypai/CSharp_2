using System;
using System.Drawing;
using CompGame.Interfaces;

namespace CompGame.Models
{
    public class StarCreator : Creator<Star>
    {
        private ILog _log = new ConsoleLog<StarCreator>();
        
        /// <summary>
        /// Создаёт звезду со случайными параметрами
        /// </summary>
        /// <param name="rnd">Экземпляр класса Random</param>
        /// <returns>Звезда</returns>
        public override Star Create(Random rnd)
        {
            _log.Write("Вызов конструктора звезды");
            
            var constRnd = rnd.Next(2, 30);

            return new Star(new Point(rnd.Next(Scene.Width), rnd.Next(0, Scene.Height)), new Point(-constRnd, 0),
                new Size(2 + constRnd, 2 + constRnd));
        }
    }
}