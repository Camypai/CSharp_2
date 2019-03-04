using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CompGame.Models;

namespace CompGame
{
    internal class SplashScreen : Scene
    {
        private static BufferedGraphicsContext _context;

        private static List<BaseObject> _BaseObjects;
        
        private static readonly StarCreator StarCreator = new StarCreator();
        private static readonly AsteroidCreator AsteroidCreator = new AsteroidCreator();
        private static readonly LineCreator LineCreator = new LineCreator();

        /// <summary>
        /// Инициализация сцены на форме
        /// </summary>
        /// <param name="form">Форма, на которой происходит инициализация</param>
        public static void Init(Form form)
        {
            _context = BufferedGraphicsManager.Current;
            var graphics = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            if(Width > 1000 || Height > 1000 || Width < 0 || Height < 0)
                throw new ArgumentOutOfRangeException();

            Buffer = _context.Allocate(graphics, new Rectangle(0, 0, Width, Height));

            var timer = new Timer {Interval = 100};
            timer.Start();

            var buttonStart = new Button
            {
                Text = "Начать игру",
                Width = 100
            };

            buttonStart.Location = new Point(Width / 2 - buttonStart.Width / 2, Height / 2);
            buttonStart.Click += (sender, args) =>
            {
                foreach (Control control in form.Controls)
                {
                    if (control is Button)
                        control.Visible = false;
                }

                Game.Init(form);
                timer.Tick -= Timer_Tick;
            };

            var buttonRecords = new Button
            {
                Text = "Таблица рекордов",
                Width = 150
            };

            buttonRecords.Location = new Point(Width / 2 - buttonRecords.Width / 2,
                buttonStart.Location.Y + buttonRecords.Height * 2);
            buttonRecords.Click += (sender, args) =>
            {
                MessageBox.Show("В разработке", "Таблица рекордов", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };

            var buttonExit = new Button
            {
                Text = "Выход",
                Width = 100
            };

            buttonExit.Location = new Point(Width / 2 - buttonExit.Width / 2,
                buttonRecords.Location.Y + buttonExit.Height * 2);
            buttonExit.Click += (sender, args) => Application.Exit();


            form.Controls.Add(buttonStart);
            form.Controls.Add(buttonRecords);
            form.Controls.Add(buttonExit);

            Load();

            timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// Отрисовка объектов на сцене
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (var baseObject in _BaseObjects)
                baseObject.Draw();
            Buffer.Render();
        }

        /// <summary>
        /// Загрузка сцены игры
        /// </summary>
        public static void Load()
        {
            const int _maxObjectsCount = 30;

            var rnd = new Random();

            var _starsCount = rnd.Next(5, _maxObjectsCount);
            var _linesCount = rnd.Next(3, _maxObjectsCount / 2);
            var _baseObjectsCount = rnd.Next(5, _maxObjectsCount);

            var _stars = new Star[_starsCount];
            var _lines = new Line[_linesCount];

            var _asteroids = new Asteroid[_baseObjectsCount];

            for (var i = 0; i < _starsCount; i++)
            {
                var r = rnd.Next(2, 30);
                _stars[i] = StarCreator.Create(rnd);
            }

            for (var i = 0; i < _baseObjectsCount; i++)
            {
                var r = rnd.Next(2, 30);
                _asteroids[i] = AsteroidCreator.Create(rnd);
            }

            for (var i = 0; i < _linesCount; i++)
                _lines[i] = LineCreator.Create(rnd);

            _BaseObjects = new List<BaseObject>();
            _BaseObjects.AddRange(_stars);
            _BaseObjects.AddRange(_lines);
            _BaseObjects.AddRange(_asteroids);
        }

        /// <summary>
        /// Обновление отрисованных объектов на сцене
        /// </summary>
        private static void Update()
        {
            foreach (var baseObject in _BaseObjects)
                baseObject.Update();
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
    }
}