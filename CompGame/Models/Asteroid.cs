using System;
using System.Drawing;

namespace CompGame.Models
{
    public class Asteroid : BaseObject
    {
        private readonly Image _asteroid;
        public int Power;
        
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
    }
}