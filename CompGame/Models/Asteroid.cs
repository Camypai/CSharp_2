using System;
using System.Drawing;

namespace CompGame.Models
{
    public class Asteroid : BaseObject
    {
        private readonly Image asteroid;
        
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            asteroid = Image.FromFile(
                @"Images\meteor_color_small.png",
                true);
        }
        
        public override void Draw()
        {
            
            var _asteroid = asteroid.GetThumbnailImage(Size.Width, Size.Height, null, IntPtr.Zero);

            Scene.Buffer.Graphics.DrawImage(_asteroid, Pos);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Scene.Width + Size.Width;
        }
    }
}