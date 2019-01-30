using System.Drawing;

namespace CompGame.Models
{
    public class Line : BaseObject
    {
        public Line(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Scene.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Scene.Width + Size.Width;
        }
    }
}