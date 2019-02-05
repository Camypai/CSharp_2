using System;
using System.Drawing;

namespace CompGame.Models
{
    public class Star : BaseObject
    {
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Scene.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Scene.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }

        public override void Update()
        {
            // Заменил минус на плюс, потому что двигались они в другую сторону
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Reload();
        }

        public override void Reload()
        {
            Pos = new Point(Scene.Width, new Random().Next(Scene.Height));
        }
    }
}