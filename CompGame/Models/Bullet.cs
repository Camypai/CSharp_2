using System;
using System.Drawing;

namespace CompGame.Models
{
    public class Bullet : BaseObject
    {
        /// <summary>
        /// Инициализация пули
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        /// <param name="log">Метод логгирования</param>
        public Bullet(Point pos, Point dir, Size size, EventHandler<string> log) : base(pos, dir, size, log)
        {
            Logging(this, "Создан");
        }

        /// <summary>
        /// Отрисовка пули
        /// </summary>
        public override void Draw()
        {
            Scene.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
            Logging(this, "Отрисован");
        }

        /// <summary>
        /// Изменение положения полу
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + 3;
            if(Pos.X > Scene.Width) Reload();
            Logging(this, "изменилось положение");
        }

        /// <summary>
        /// Перезагрузка объекта пули
        /// </summary>
        public override void Reload()
        {
            Pos.X = 0;
            Logging(this, "Перезагрузка");
        }
    }
}