using System.Windows.Forms;

namespace CompGame
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var form = new Form
            {
                Width = 800, Height = 600
            };

            Game.Init(form);
            form.Show();
            Game.Draw();
            Application.Run(form);
        }
    }
}