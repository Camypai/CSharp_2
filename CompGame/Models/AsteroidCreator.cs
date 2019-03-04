using System;
using System.Drawing;
using CompGame.Interfaces;

namespace CompGame.Models
{
    public class AsteroidCreator : Creator<Asteroid>
    {
        private ILog _log = new ConsoleLog<AsteroidCreator>();
        
        /// <summary>
        /// Создаёт астероид с рандомными параметрами
        /// </summary>
        /// <param name="rnd">Экземпляр класса Random</param>
        /// <returns>Астероид</returns>
        public override Asteroid Create(Random rnd)
        {
            _log.Write("Вызов конструктора астероида");
            
            var constRnd = rnd.Next(5, 30);

            return new Asteroid(new Point(rnd.Next(Scene.Width), rnd.Next(0, Scene.Height)), new Point(-constRnd, 0),
                new Size(10 + constRnd, 10 + constRnd));
        }
    }
}