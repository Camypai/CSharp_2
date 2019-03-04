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

        private static ILog _log = new ConsoleLog<Game>();
        
        private static List<BaseObject> _baseObjects;
        private static readonly List<Bullet> Bullets = new List<Bullet>();
        private static readonly List<Asteroid> Asteroids = new List<Asteroid>();
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
            _log.Write("Инициализация");
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
            _log.Write("Отрисовка объектов на сцене");
            Buffer.Graphics.Clear(Color.Black);
            foreach (var baseObject in _baseObjects)
                baseObject.Draw();

            foreach (var bullet in Bullets) bullet.Draw();
            foreach (var asteroid in Asteroids) asteroid.Draw();

            Ship?.Draw();
            Buffer.Render();
        }

        /// <summary>
        /// Загрузка сцены игры
        /// </summary>
        private static void Load()
        {
            _log.Write("Загрузка сцены");
            
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
        private static void Update()
        {
            _log.Write("Обновление кадра");
            
            // TODO: Иногда возникает исключение выхода за пределы массива. Необходимо исправить

            try
            {
                if (Bullets.Any())
                    for (var i = 0; i < Bullets.Count; i++)
                    {
                        for (var j = 0; j < Asteroids.Count; j++)
                        {
                            if (Bullets[i] == null || !Asteroids[j].Collision(Bullets[i])) continue;

                            System.Media.SystemSounds.Hand.Play();
                            Bullets.RemoveAt(i);
                            Ship.ScoreAdd();
                            Asteroids.RemoveAt(j);
                        }
                    }
            }
            catch (IndexOutOfRangeException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (ArgumentOutOfRangeException e)
            {
                MessageBox.Show(e.Message);
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
        public static void Finish()
        {
            _log.Write("Конец игры");
            
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