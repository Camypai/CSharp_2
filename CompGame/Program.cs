﻿using System.Drawing;
using System.Windows.Forms;

namespace CompGame
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var form = new Form
            {
                Width = 800,
                Height = 600, 
                FormBorderStyle = FormBorderStyle.FixedSingle,
                Controls =
                {
                    new Label
                    {
                        Text = "Горшков Илья; C# level 2",
                        BackColor = Color.Black,
                        ForeColor = Color.White
                    }
                }
            };

            SplashScreen.Init(form);
            form.Show();
            SplashScreen.Load();
            Application.Run(form);
        }
    }
}