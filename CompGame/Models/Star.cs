using System;
using System.Drawing;
using CompGame.Interfaces;

namespace CompGame.Models
{
    public class Star : BaseObject
    {
        private ILog _log = new ConsoleLog<Star>();
        
        /// <summary>
        /// Инициализация звезды
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        /// <param name="log">Метод логгирования</param>
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
//            _log.Write("Создан");
        }

        /// <summary>
        /// Отрисовка звезды
        /// </summary>
        public override void Draw()
        {
            Scene.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Scene.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
//            _log.Write("Отрисован");
        }

        /// <summary>
        /// Изменение положения звезды
        /// </summary>
        public override void Update()
        {
            // Заменил минус на плюс, потому что двигались они в другую сторону
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Reload();
//            _log.Write("Изменилось положение");
        }

        /// <summary>
        /// Перезагрузка объекта звезды
        /// </summary>
        public override void Reload()
        {
            Pos = new Point(Scene.Width, new Random().Next(Scene.Height));
            _log.Write("Перезагрузка");
        }
    }
}