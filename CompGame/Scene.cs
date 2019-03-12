using System.Drawing;
using System.Windows.Forms;

namespace CompGame
{
    public abstract class Scene
    {
        public static BufferedGraphics Buffer;

        public static int Width { get; set; }
        public static int Height { get; set; }

        public abstract void Init(Form form);
        public abstract void Draw();
        public abstract void Load();
        public abstract void Update();

        public virtual void Finish()
        {
            
        }
    }
}