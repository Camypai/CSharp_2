using System;
using System.Drawing;
using CompGame.Interfaces;

namespace CompGame.Models
{
    public class Bullet : BaseObject, IDisposable
    {

        private bool disposed = false;
        private ILog _log = new ConsoleLog<Bullet>();
        
        /// <inheritdoc />
        /// <summary>
        /// Инициализация пули
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        /// <param name="log">Метод логгирования</param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
//            _log.Write("Создан");
        }

        /// <inheritdoc />
        /// <summary>
        /// Отрисовка пули
        /// </summary>
        public override void Draw()
        {
            Scene.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
//            _log.Write("Отрисован");
        }

        /// <inheritdoc />
        /// <summary>
        /// Изменение положения пули
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + 3;
            if(Pos.X > Scene.Width) Die();
//            _log.Write("изменилось положение");
        }

        /// <inheritdoc />
        /// <summary>
        /// Перезагрузка объекта пули
        /// </summary>
        public override void Reload()
        {
            Pos.X = 0;
            _log.Write("Перезагрузка");
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
                    Dir = Point.Empty;
                    Size = Size.Empty;
                    _log.Write("Отчистка объекта");
                }
                
                disposed = true;
            }
        }

        /// <summary>
        /// Деструктор
        /// </summary>
        ~Bullet()
        {
            _log.Write("Вызов деструктора");
            Dispose(false);
            System.Media.SystemSounds.Beep.Play();
        }
    }
}