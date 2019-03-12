using System;
using System.Drawing;
using CompGame.Interfaces;

namespace CompGame.Models
{
    public class Asteroid : BaseObject, IComparable<Asteroid>, IDisposable
    {
        /// <summary>
        /// Изображение
        /// </summary>
        private readonly Image _asteroid;
        
        /// <summary>
        /// Сила
        /// </summary>
        public int Power { get; set; } = 3;
        
        private ILog _log = new ConsoleLog<Asteroid>();

        /// <summary>
        /// Инициализация астероида
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        /// <param name="log">Метод логгирования</param>
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            _asteroid = Image.FromFile(
                @"Images\meteor_color_small.png",
                true).GetThumbnailImage(Size.Width, Size.Height, null, IntPtr.Zero);
            _log.Write("Создан");
        }

        /// <summary>
        /// Отрисовка экземпляра
        /// </summary>
        public override void Draw()
        {
            Scene.Buffer.Graphics.DrawImage(_asteroid, Pos);
//            _log.Write("Отрисован");
        }

        /// <summary>
        /// Обновление положения экземпляра
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Reload();
//            _log.Write("Изменилось положение");
        }

        /// <summary>
        /// Перезагрузка положения экземпляра
        /// </summary>
        public override void Reload()
        {
            Pos = new Point(Scene.Width, new Random().Next(Scene.Height));
            _log.Write("Перезагрузка");
        }

        /// <summary>
        /// Уничтожение астероида
        /// </summary>
        public override void Die()
        {
            Pos = new Point(Scene.Width, new Random().Next(Scene.Height));
            _log.Write("Уничтожен");
        }

        /// <summary>
        /// Сравнение экземпляра
        /// </summary>
        /// <param name="asteroid">Объект сравнения</param>
        /// <returns></returns>
        public int CompareTo(Asteroid asteroid)
        {
            if (Power > asteroid.Power)
                return 1;
            if (Power < asteroid.Power)
                return -1;
            else
                return 0;

        }

        public void Dispose()
        {
            _asteroid?.Dispose();
        }
    }
}