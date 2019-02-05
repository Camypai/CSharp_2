using System.Drawing;

namespace CompGame.Models
{
    public class Bullet : BaseObject
    {
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Scene.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X = Pos.X + 3;
            if(Pos.X > Scene.Width) Reload();
        }

        public override void Reload()
        {
            Pos.X = 0;
        }
    }
}