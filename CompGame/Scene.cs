using System.Drawing;

namespace CompGame
{
    public abstract class Scene
    {
        public static BufferedGraphics Buffer;

        public static int Width { get; set; }
        public static int Height { get; set; }
    }
}