using System;
using System.Drawing;
using CompGame.Exceptions;
using CompGame.Interfaces;

namespace CompGame.Models
{
    public abstract class BaseObject : ICollision
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        public Rectangle Rectangle => new Rectangle(Pos, Size);
        public delegate void Message();

        /// <summary>
        /// Инициализация объекта
        /// </summary>
        /// <param name="pos">Стартовая позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        protected BaseObject(Point pos, Point dir, Size size)
        {
            if (pos.X < 0 || pos.Y < 0 || pos.X > Scene.Width || pos.Y > Scene.Height)
            {
                throw new GameObjectException("Не верно задана позиция");
            }

            if (size.Width < 0 || size.Height < 0)
            {
                throw new GameObjectException("Не верно задан размер");
            }

            if (dir.X > 80 || dir.Y > 80 || dir.X < -80 || dir.Y < -80)
            {
                throw new GameObjectException("Не верно задано смещение");
            }

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

        public bool Collision(ICollision collision)
        {
            return collision.Rectangle.IntersectsWith(Rectangle);
        }

        public abstract void Reload();
    }
}