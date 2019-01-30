using System;
using System.Drawing;

namespace CompGame.Models
{
    public class BaseObject
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        public virtual void Draw()
        {
            var meteorOrig = Image.FromFile(@"C:\Users\HP\RiderProjects\CSharp_2\CompGame\Images\meteor_color_small.png",
                true);
            var meteor = meteorOrig.GetThumbnailImage(Size.Width, Size.Height, null, IntPtr.Zero);
//            Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
            Game.Buffer.Graphics.DrawImage(meteor, Pos);
        }

        public virtual void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
//            Pos.X = Pos.X + Dir.X;
//            Pos.Y = Pos.Y + Dir.Y;
//            if (Pos.X < 0) Dir.X = -Dir.X;
//            if (Pos.X > Game.Width) Dir.X = -Dir.X;
//            if (Pos.Y < 0) Dir.Y = -Dir.Y;
//            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
    }
}