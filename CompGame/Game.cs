using System;
using System.Drawing;
using System.Windows.Forms;
using CompGame.Models;

namespace CompGame
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        public static BaseObject[] _BaseObjects;

        public static int Width { get; set; }
        public static int Height { get; set; }

        public static void Init(Form form)
        {
            _context = BufferedGraphicsManager.Current;
            var graphics = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            Buffer = _context.Allocate(graphics, new Rectangle(0, 0, Width, Height));
            
            Load();
            
            var timer = new Timer {Interval = 100};
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        public static void Draw()
        {
            #region Подлянка

//            Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
//            Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
//            Buffer.Render();
            
            #endregion
            
            Buffer.Graphics.Clear(Color.Black);
            foreach (var baseObject in _BaseObjects)
                baseObject.Draw();
            Buffer.Render();
        }

        public static void Load()
        {
            _BaseObjects = new BaseObject[30];
            for (var i = 0; i < _BaseObjects.Length/2; i++)
                _BaseObjects[i] = new Star(new Point(600, i * 20), new Point(-i, 0), new Size(5, 5));
            
            for (var i = _BaseObjects.Length / 2; i < _BaseObjects.Length; i++)
                _BaseObjects[i] = new BaseObject(new Point(600, i * 20), new Point(-i, -i), new Size(10, 10));
        }
        
        public static void Update()
        {
            foreach (var baseObject in _BaseObjects)
                baseObject.Update();
        }
        
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
    }
}