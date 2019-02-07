using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using CompGame.Exceptions;
using CompGame.Interfaces;

namespace CompGame.Models
{
    public abstract class BaseObject : ICollision
    {
        /// <summary>
        /// Позиция объекта на экране
        /// </summary>
        protected Point Pos;
        /// <summary>
        /// Смещение объекта
        /// </summary>
        protected Point Dir;
        /// <summary>
        /// Размер объекта
        /// </summary>
        protected Size Size;

        /// <summary>
        /// Область взаимодействия объекта
        /// </summary>
        public Rectangle Rectangle => new Rectangle(Pos, Size);
        
        /// <summary>
        /// Делегат сообщения на экране
        /// </summary>
        public delegate void Message();

        /// <summary>
        /// Событие логгирования
        /// </summary>
        public static event EventHandler<string> Log; 

        /// <summary>
        /// Инициализация объекта
        /// </summary>
        /// <param name="pos">Стартовая позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        protected BaseObject(Point pos, Point dir, Size size, EventHandler<string> log)
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
            Log = log;
        }

        /// <summary>
        /// Отрисовка объекта
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Обновление положения объекта
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Провека на соллизию
        /// </summary>
        /// <param name="collision">Объект проверки</param>
        /// <returns></returns>
        public bool Collision(ICollision collision)
        {
            return collision.Rectangle.IntersectsWith(Rectangle);
        }

        /// <summary>
        /// Перезагрузка объекта
        /// </summary>
        public abstract void Reload();

        /// <summary>
        /// Уничтожение объекта
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void Die()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Логгирование
        /// </summary>
        /// <param name="o">Источник</param>
        /// <param name="message">Сообщение</param>
        public static void Logging(object o,string message)
        {
            Log?.Invoke(o, message);
        }
    }
}