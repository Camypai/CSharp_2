using System;
using System.Drawing;

namespace CompGame.Models
{
    public class Asteroid : BaseObject, ICloneable, IComparable<Asteroid>
    {
        /// <summary>
        /// Изображение
        /// </summary>
        private readonly Image _asteroid;
        
        /// <summary>
        /// Сила
        /// </summary>
        public int Power { get; set; } = 3;

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            _asteroid = Image.FromFile(
                @"Images\meteor_color_small.png",
                true).GetThumbnailImage(Size.Width, Size.Height, null, IntPtr.Zero);
            Power = 1;
        }

        /// <summary>
        /// Отрисовка экземпляра
        /// </summary>
        public override void Draw()
        {
            Scene.Buffer.Graphics.DrawImage(_asteroid, Pos);
        }

        /// <summary>
        /// Обновление положения экземпляра
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Reload();
        }

        /// <summary>
        /// Перезагрузка положения экземпляра
        /// </summary>
        public override void Reload()
        {
            Pos = new Point(Scene.Width, new Random().Next(Scene.Height));
        }

        /// <summary>
        /// Клонирование экземпляра
        /// </summary>
        /// <returns>Новый экземпляр</returns>
        public object Clone()
        {
            var asteroid = new Asteroid(new Point(Pos.X, Pos.Y), new Point(Dir.X, Dir.Y),
                new Size(Size.Width, Size.Height))
            {
                Power = Power
            };
            return asteroid;
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