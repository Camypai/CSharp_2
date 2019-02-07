using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CompGame.Models;

namespace CompGame
{
    internal class Game : Scene
    {
        private static BufferedGraphicsContext _context;

        private static List<BaseObject> _baseObjects;
        private static Bullet _bullet;
        private static readonly Ship Ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));
        private static readonly Timer Timer = new Timer();

        public static Random Rnd = new Random();

        /// <summary>
        /// Инициализация сцены на форме
        /// </summary>
        /// <param name="form">Форма, на которой происходит инициализация</param>
        public static void Init(Form form)
        {
            _context = BufferedGraphicsManager.Current;
            var graphics = form.CreateGraphics();
            Buffer.Dispose();
            Buffer = _context.Allocate(graphics, new Rectangle(0, 0, Width, Height));

            Load();
            
            form.Focus();
            form.KeyPreview = true;
            form.KeyDown += Form_KeyDown;
            Timer.Tick += Timer_Tick;
            Ship.MessageDie += Finish;
            Timer.Start();
        }

        /// <summary>
        /// Отрисовка объектов на сцене
        /// </summary>
        private static void Draw()
        {
            #region Подлянка

//            Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
//            Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
//            Buffer.Render();

            #endregion

            Buffer.Graphics.Clear(Color.Black);
            foreach (var baseObject in _baseObjects)
                baseObject.Draw();
            _bullet?.Draw();
            Ship?.Draw();
            if (Ship != null)
                Buffer.Graphics.DrawString("Energy:" + Ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            Buffer.Render();
        }

        /// <summary>
        /// Загрузка сцены игры
        /// </summary>
        private static void Load()
        {
            const int _maxObjectsCount = 30;

            var rnd = new Random();

            var _starsCount = rnd.Next(5, _maxObjectsCount);
            var _linesCount = rnd.Next(3, _maxObjectsCount / 2);
            var _asteroidsCount = rnd.Next(5, _maxObjectsCount);
//            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));

            var _stars = new Star[_starsCount];
            var _lines = new Line[_linesCount];

            var _asteroids = new Asteroid[_asteroidsCount];

            for (var i = 0; i < _starsCount; i++)
            {
                var r = rnd.Next(2, 30);
                _stars[i] = new Star(new Point(rnd.Next(Width), rnd.Next(0, Height)), new Point(-r, 0),
                    new Size(2 + r, 2 + r));
            }

            for (var i = 0; i < _asteroidsCount; i++)
            {
                var r = rnd.Next(2, 30);
                _asteroids[i] = new Asteroid(new Point(rnd.Next(Width), rnd.Next(0, Height)), new Point(-r, 0),
                    new Size(10 + r, 10 + r));
            }

            for (var i = 0; i < _linesCount; i++)
                _lines[i] = new Line(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(-80, 0),
                    new Size(20, 0));

            _baseObjects = new List<BaseObject>();
            _baseObjects.AddRange(_stars);
            _baseObjects.AddRange(_lines);
            _baseObjects.AddRange(_asteroids);
        }

        /// <summary>
        /// Обновление отрисованных объектов на сцене
        /// </summary>
        private static void Update()
        {
            foreach (var baseObject in _baseObjects)
            {
                baseObject.Update();

                if (_bullet == null || !(baseObject is Asteroid) | !baseObject.Collision(_bullet)) continue;

                System.Media.SystemSounds.Hand.Play();
                _bullet = null;
                baseObject.Reload();

                if (!Ship.Collision((baseObject as Asteroid))) continue;
                var rnd = new Random();
                Ship?.EnergyLow(rnd.Next(1, 10));
                System.Media.SystemSounds.Asterisk.Play();
                if (Ship.Energy <= 0) Ship?.Die();
            }

            _bullet?.Update();
        }

        public static void Finish()
        {
            Timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline),
                Brushes.White, 200, 100);
            Buffer.Render();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    _bullet = new Bullet(new Point(Ship.Rectangle.X + 10, Ship.Rectangle.Y + 4), new Point(4, 0),
                        new Size(4, 1));
                    break;
                case Keys.Up:
                    Ship.Up();
                    break;
                case Keys.Down:
                    Ship.Down();
                    break;
            }
        }
    }
}