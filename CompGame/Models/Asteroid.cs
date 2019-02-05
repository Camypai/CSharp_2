using System;
using System.Drawing;

namespace CompGame.Models
{
    public class Asteroid : BaseObject, ICloneable, IComparable<Asteroid>
    {
        private readonly Image _asteroid;
        public int Power { get; set; } = 3;

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            _asteroid = Image.FromFile(
                @"Images\meteor_color_small.png",
                true).GetThumbnailImage(Size.Width, Size.Height, null, IntPtr.Zero);
            Power = 1;
        }

        public override void Draw()
        {
            Scene.Buffer.Graphics.DrawImage(_asteroid, Pos);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Reload();
        }

        public override void Reload()
        {
            Pos = new Point(Scene.Width, new Random().Next(Scene.Height));
        }

        public object Clone()
        {
            var asteroid = new Asteroid(new Point(Pos.X, Pos.Y), new Point(Dir.X, Dir.Y),
                new Size(Size.Width, Size.Height))
            {
                Power = Power
            };
            return asteroid;
        }

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