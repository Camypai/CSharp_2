using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CompGame.Interfaces;
using CompGame.Models;

namespace CompGame
{
    internal class Game : Scene
    {
        private static BufferedGraphicsContext _context;

        private static readonly ILog Log = new ConsoleLog<Game>();

        private static List<BaseObject> _baseObjects;
        private static readonly List<Bullet> Bullets = new List<Bullet>();
        private static readonly List<Asteroid> Asteroids = new List<Asteroid>();
        private static readonly Timer Timer = new Timer{Interval = 40};
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
        public override void Init(Form form)
        {
            Log.Write("Инициализация");
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
        public override void Draw()
        {
            Log.Write("Отрисовка объектов на сцене");
            Buffer.Graphics.Clear(Color.Black);
            foreach (var baseObject in _baseObjects)
                baseObject.Draw();

            foreach (var bullet in Bullets) bullet.Draw();
            foreach (var asteroid in Asteroids) asteroid.Draw();

            Ship?.Draw();
            
            // Отрисовка интерфейса
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
        public override void Load()
        {
            Log.Write("Загрузка сцены");

            const int _maxObjectsCount = 30;
            const int _kitCount = 5;

            var rnd = new Random();

            var _starsCount = rnd.Next(5, _maxObjectsCount);
            var _linesCount = rnd.Next(3, _maxObjectsCount / 2);

            var _stars = new BaseObject[_starsCount];
            var _lines = new BaseObject[_linesCount];
            var _kits = new BaseObject[_kitCount];

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
            _baseObjects.AddRange(_kits);
        }

        /// <summary>
        /// Обновление отрисованных объектов на сцене
        /// </summary>
        public override void Update()
        {
            Log.Write("Обновление кадра");

            // TODO: Иногда возникает исключение выхода за пределы массива. Необходимо исправить
            
            for (var i = 0; i < Bullets.Count; i++)
            {
                for (var j = 0; j < Asteroids.Count; j++)
                {
                    try
                    {
                        if (!Asteroids[j].Collision(Bullets[i])) continue;
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Log.Write(e.Message);
                        i = i > 0 ? --i : 0;
                    }

                    System.Media.SystemSounds.Hand.Play();
                    Bullets.RemoveAt(i);
                    Ship.ScoreAdd();
                    Asteroids.RemoveAt(j);
                }
            }

            for (var i = 0; i < Asteroids.Count; i++)
            {
                if (!Ship.Collision(Asteroids[i])) continue;
                Ship?.EnergyChange(-Asteroids[i].Power);
                Asteroids[i].Dispose();
                Asteroids.RemoveAt(i);
                System.Media.SystemSounds.Asterisk.Play();
                if (Ship.Energy <= 0) Ship?.Die();
            }

            if (!Asteroids.Any())
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

            foreach (var bullet in Bullets) bullet.Update();
            foreach (var asteroid in Asteroids) asteroid.Update();
        }

        /// <summary>
        /// Конец игры
        /// </summary>
        public override void Finish()
        {
            Log.Write("Конец игры");

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
        private void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Подписка на нажатие клавиш
        /// </summary>
        /// <param name="sender">Источник</param>
        /// <param name="e">Событие нажатия клавиш</param>
        private void Form_KeyDown(object sender, KeyEventArgs e)
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