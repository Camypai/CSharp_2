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
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        protected BaseObject(Point pos, Point dir, Size size)
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
        public abstract void Update();
    }
}