using System.Windows.Forms;

namespace CompGame
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var form = new Form
            {
                Width = 800, Height = 600, FormBorderStyle = FormBorderStyle.FixedSingle
            };

            SplashScreen.Init(form);
            form.Show();
            Application.Run(form);
        }
    }
}