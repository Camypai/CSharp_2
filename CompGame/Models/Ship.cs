using System.Drawing;

namespace CompGame.Models
{
    public class Ship : BaseObject
    {
        public int Energy { get; private set; } = 100;
        public static event  Message MessageDie;

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Scene.Buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        public override void Reload()
        {
            throw new System.NotImplementedException();
        }
        
        public void EnergyLow(int n)
        {
            Energy -= n;
        }
        
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        
        public void Down()
        {
            if (Pos.Y < Scene.Height) Pos.Y = Pos.Y + Dir.Y;
        }
        
        public void Die()
        {
            MessageDie?.Invoke();
        }
    }
}