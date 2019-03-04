using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CompGame.Models;

namespace CompGame
{
    internal class Game : Scene
    {
        private static BufferedGraphicsContext _context;

        private static List<BaseObject> _baseObjects;
        private static List<Bullet> _bullets = new List<Bullet>();
        private static List<Asteroid> _asteroids = new List<Asteroid>();
        private static readonly Ship Ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10), Message);
        private static readonly Timer Timer = new Timer();
        private static int _asteroidsCount = 5;
        private static readonly StarCreator StarCreator = new StarCreator();
        private static readonly KitCreator KitCreator = new KitCreator();
        private static readonly AsteroidCreator AsteroidCreator = new AsteroidCreator();
        private static readonly LineCreator LineCreator = new LineCreator();
        private static readonly ShipCreator ShipCreator = new ShipCreator();
        private static readonly Ship Ship = ShipCreator.Create();
        private static readonly BulletCreator BulletCreator = new BulletCreator(Ship);

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
        /// Метод логгирования
        /// </summary>
        /// <param name="o">Источник</param>
        /// <param name="message">Сообщение</param>
        private static void Message(object o, string message)
        {
            var m = $"{o}: {message}";
            Console.WriteLine(m);

            using (var sw = new StreamWriter("log.txt", true))
            {
                sw.WriteLine(m);
            }
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
            
            foreach(var bullet in _bullets) bullet.Draw();
            foreach (var asteroid in _asteroids) asteroid.Draw();
            
            Ship?.Draw();
            if (Ship != null)
            {
                Buffer.Graphics.DrawString("Energy:" + Ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 50);
                Buffer.Graphics.DrawString("Score:" + Ship.Score, SystemFonts.DefaultFont, Brushes.White, 0, 70);
            }
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
            
            var _kitCount = 5;

            var _stars = new Star[_starsCount];
            var _lines = new Line[_linesCount];
            var _kits = new Kit[_kitCount];

//            var _asteroids = new Asteroid[_asteroidsCount];
            
            for (var i = 0; i < _kitCount; i++)
                _kits[i] = KitCreator.Create(rnd);

            for (var i = 0; i < _starsCount; i++)
                _stars[i] = StarCreator.Create(rnd);

            for (var i = 0; i < _asteroidsCount; i++)
                Asteroids.Add(AsteroidCreator.Create(rnd));

            for (var i = 0; i < _linesCount; i++)
                _lines[i] = LineCreator.Create(rnd);

            _baseObjects = new List<BaseObject>();
            _baseObjects.AddRange(_stars);
            _baseObjects.AddRange(_lines);
//            _baseObjects.AddRange(_asteroids);
            _baseObjects.AddRange(_kits);
        }

        /// <summary>
        /// Обновление отрисованных объектов на сцене
        /// </summary>
        private static void Update()
        {   
            for (var i = 0; i < _bullets.Count; i++)
            {
                for (var j = 0; j < _asteroids.Count; j++)
                {
                    if (_bullets[i] == null || !_asteroids[j].Collision(_bullets[i])) continue;
                    
                    System.Media.SystemSounds.Hand.Play();
                    _bullets.RemoveAt(i);
                    Ship.ScoreAdd();
                    _asteroids.RemoveAt(j);
                }
            }

            for (var i = 0; i < _asteroids.Count; i++)
            {
                if (!Ship.Collision(_asteroids[i])) continue;
                var rnd = new Random();
                Ship?.EnergyChange(-rnd.Next(1, 10));
                _asteroids.RemoveAt(i);
                System.Media.SystemSounds.Asterisk.Play();
                if (Ship.Energy <= 0) Ship?.Die();
            }

            if (!_asteroids.Any())
            {
                var rnd = new Random();
                _asteroidsCount++;
                for (var i = 0; i < _asteroidsCount; i++)
                    Asteroids.Add(AsteroidCreator.Create(rnd));
            }
            
            foreach (var baseObject in _baseObjects)
            {
                baseObject.Update();

                if (!(baseObject is Kit kit)) continue;
                
                if (!Ship.Collision(kit)) continue;
                Ship?.EnergyChange(kit.Power);
                kit.Reload();
            }

            foreach(var bullet in _bullets) bullet.Update();
            foreach (var asteroid in _asteroids) asteroid.Update(); 
        }

        /// <summary>
        /// Конец игры
        /// </summary>
        public static void Finish()
        {
            Timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline),
                Brushes.White, 200, 100);
            Buffer.Render();
        }

        /// <summary>
        /// Подписка на каждый тик таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Подписка на нажатие клавиш
        /// </summary>
        /// <param name="sender">Источник</param>
        /// <param name="e">Событие нажатия клавиш</param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    Bullets.Add(BulletCreator.Create());
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