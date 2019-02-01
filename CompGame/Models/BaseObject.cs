using System;
using System.Drawing;

namespace CompGame.Models
{
    public abstract class BaseObject
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        /// <summary>
        /// Инициализация объекта
        /// </summary>
        /// <param name="pos">Стартовая позиция</param>
        /// <param name="dir">Напрвление</param>
        /// <param name="size">Размер</param>
        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        /// <summary>
        /// Отрисовка объекта
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Обновление положения объекта
        /// </summary>
        public virtual void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Scene.Width + Size.Width;
        }
    }
}