using System;
using System.Drawing;
using CompGame.Interfaces;

namespace CompGame.Models
{
    public class KitCreator : Creator<Kit>
    {
        private ILog _log = new ConsoleLog<KitCreator>();
        
        /// <summary>
        /// Создаёт аптечку с рандомными параметрами
        /// </summary>
        /// <param name="rnd">Экземпляр класса Random</param>
        /// <returns>Аптечка</returns>
        public override Kit Create(Random rnd)
        {
            _log.Write("Вызов конструктора аптечки");
            
            var constRnd = rnd.Next(2, 30);

            return new Kit(new Point(rnd.Next(Scene.Width), rnd.Next(0, Scene.Height)), new Point(-constRnd, 0),
                new Size(8, 8));
        }
    }
}