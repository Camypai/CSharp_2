using System;
using System.Drawing;
using CompGame.Interfaces;

namespace CompGame.Models
{
    public class ShipCreator : Creator<Ship>
    {
        private ILog _log = new ConsoleLog<ShipCreator>();
        
        /// <summary>
        /// Создаёт корабль со статичными параметрами
        /// </summary>
        /// <returns>Корабль</returns>
        public override Ship Create()
        {
            _log.Write("Вызов конструктора корабля");
            
            return new Ship(new Point(10,400), new Point(5,5),
                new Size(10,10));
        }
    }
}