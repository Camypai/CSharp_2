using System;
using System.Drawing;

namespace CompGame.Models
{
    public class Asteroid : BaseObject, IComparable<Asteroid>
    {
        /// <summary>
        /// Изображение
        /// </summary>
        private readonly Image _asteroid;
        
        /// <summary>
        /// Сила
        /// </summary>
        public int Power { get; set; } = 3;

        /// <summary>
        /// Инициализация астероида
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        /// <param name="log">Метод логгирования</param>
        public Asteroid(Point pos, Point dir, Size size, EventHandler<string> log) : base(pos, dir, size, log)
        {
            _asteroid = Image.FromFile(
                @"Images\meteor_color_small.png",
                true).GetThumbnailImage(Size.Width, Size.Height, null, IntPtr.Zero);
            Power = 1;
            Logging(this, "Создан");
        }

        /// <summary>
        /// Отрисовка экземпляра
        /// </summary>
        public override void Draw()
        {
            Scene.Buffer.Graphics.DrawImage(_asteroid, Pos);
            Logging(this, "Отрисован");
        }

        /// <summary>
        /// Обновление положения экземпляра
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Reload();
            Logging(this, "Изменилось положение");
        }

        /// <summary>
        /// Перезагрузка положения экземпляра
        /// </summary>
        public override void Reload()
        {
            Pos = new Point(Scene.Width, new Random().Next(Scene.Height));
            Logging(this, "Перезагрузка");
        }

        /// <summary>
        /// Уничтожение астероида
        /// </summary>
        public override void Die()
        {
            Pos = new Point(Scene.Width, new Random().Next(Scene.Height));
            Logging(this, "Уничтожен");
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
    }
}