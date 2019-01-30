using System.Drawing;

namespace CompGame
{
    public class Scene
    {
        public static BufferedGraphics Buffer;

        public static int Width { get; protected set; }
        protected static int Height { get; set; }
    }
}