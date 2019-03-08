using System;
using System.Drawing;
using CompGame.Interfaces;

namespace CompGame.Models
{
    public class BulletCreator : Creator<Bullet>
    {
        /// <summary>
        /// Ссылка на корабль
        /// </summary>
        private readonly Ship _ship;

        
        private readonly ILog _log = new ConsoleLog<BulletCreator>();
        
        /// <summary>
        /// Конструктор фабрики патронов
        /// </summary>
        /// <param name="ship">Корабль</param>
        public BulletCreator(Ship ship)
        {
            _ship = ship;
        }
        
        /// <summary>
        /// Создаёт патрон возле корабля
        /// </summary>
        /// <returns>Патрон</returns>
        public override Bullet Create()
        {
            _log.Write("Вызов конструктора патрона");
            
            return new Bullet(new Point(_ship.Rectangle.X+10, _ship.Rectangle.Y + 9), new Point(6, 0),
                new Size(4,1));
        }
    }
}