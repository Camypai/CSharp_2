using System;
using System.Drawing;

namespace CompGame.Models
{
    public class Bullet : BaseObject, IDisposable
    {

        private bool disposed = false;
        /// <inheritdoc />
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

        /// <inheritdoc />
        /// <summary>
        /// Отрисовка пули
        /// </summary>
        public override void Draw()
        {
            Scene.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
            Logging(this, "Отрисован");
        }

        /// <inheritdoc />
        /// <summary>
        /// Изменение положения пули
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + 3;
            if(Pos.X > Scene.Width) Die();
            Logging(this, "изменилось положение");
        }

        /// <inheritdoc />
        /// <summary>
        /// Перезагрузка объекта пули
        /// </summary>
        public override void Reload()
        {
            Pos.X = 0;
            Logging(this, "Перезагрузка");
        }

        /// <inheritdoc />
        /// <summary>
        /// Уничтожение объекта
        /// </summary>
        public override void Die()
        {
            Dispose();
        }

        /// <summary>
        /// Помечает объект, как освобождаемый
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        /// <summary>
        /// Проверка на готовность объекта к уничтожению
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Dir.X = 0;
                }
                
                disposed = true;
            }
        }

        /// <summary>
        /// Деструктор
        /// </summary>
        ~Bullet()
        {
            Dispose(false);
            System.Media.SystemSounds.Beep.Play();
        }
    }
}