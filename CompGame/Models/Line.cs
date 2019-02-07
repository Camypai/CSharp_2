using System;
using System.Drawing;

namespace CompGame.Models
{
    public class Line : BaseObject
    {
        /// <summary>
        /// Инициализация линии
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        /// <param name="log">Метод логгирования</param>
        public Line(Point pos, Point dir, Size size, EventHandler<string> log) : base(pos, dir, size, log)
        {
            Logging(this, "Создан");
        }

        /// <summary>
        /// Отрисовка линии
        /// </summary>
        public override void Draw()
        {
            Scene.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y);
            Logging(this, "Отрисован");
        }

        /// <summary>
        /// Изменение положения линии
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Reload();
            Logging(this, "Изменилось положение");
        }

        /// <summary>
        /// Перезагрузка объекта линии
        /// </summary>
        public override void Reload()
        {
            Pos = new Point(Scene.Width, new Random().Next(Scene.Height));
            Logging(this, "Перегазрузка");
        }
    }
}