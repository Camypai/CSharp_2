using System;
using System.Drawing;

namespace CompGame.Models
{
    public class Kit : BaseObject
    {
        /// <summary>
        /// Колличество восполняемой энергии корабля
        /// </summary>
        public int Power { get; } = 3;
        
        /// <summary>
        /// Инициализация аптечки
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        /// <param name="log">Метод логгирования</param>
        public Kit(Point pos, Point dir, Size size, EventHandler<string> log) : base(pos, dir, size, log)
        {
            Logging(this, "Создан");
        }

        /// <summary>
        /// Отрисовка аптечки
        /// </summary>
        public override void Draw()
        {
            Scene.Buffer.Graphics.DrawRectangle(Pens.PaleGreen, Pos.X, Pos.Y, Size.Width, Size.Height);
            Logging(this, "Отрисован");
        }

        /// <summary>
        /// Изменение положения аптечки
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Reload();
            Logging(this, "Изменилось положение");
        }

        /// <summary>
        /// Перезагрузка объекта аптечки
        /// </summary>
        public override void Reload()
        {
            Pos = new Point(Scene.Width, new Random().Next(Scene.Height));
            Logging(this, "Перезагрузка");
        }
    }
}