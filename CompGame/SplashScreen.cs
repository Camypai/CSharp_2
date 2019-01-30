using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CompGame.Models;

namespace CompGame
{
    internal class SplashScreen : Scene
    {
        private static BufferedGraphicsContext _context;

        private static List<BaseObject> _BaseObjects;

        public static void Init(Form form)
        {
            _context = BufferedGraphicsManager.Current;
            var graphics = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
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

        private static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (var baseObject in _BaseObjects)
                baseObject.Draw();
            Buffer.Render();
        }

        private static void Load()
        {
            const int _maxObjectsCount = 30;

            var rnd = new Random();

            var _starsCount = rnd.Next(5, _maxObjectsCount);
            var _linesCount = rnd.Next(3, _maxObjectsCount / 2);
            var _baseObjectsCount = rnd.Next(5, _maxObjectsCount);

            var _stars = new Star[_starsCount];
            var _lines = new Line[_linesCount];

            var _baseObjects = new BaseObject[_baseObjectsCount];

            for (var i = 0; i < _starsCount; i++)
            {
                var r = rnd.Next(2, 30);
                _stars[i] = new Star(new Point(rnd.Next(Width), rnd.Next(0, Height)), new Point(-1 * r, 0),
                    new Size(2 + r, 2 + r));
            }

            for (var i = 0; i < _baseObjectsCount; i++)
            {
                var r = rnd.Next(2, 30);
                _baseObjects[i] = new BaseObject(new Point(rnd.Next(Width), rnd.Next(0, Height)), new Point(-1 * r, 0),
                    new Size(10 + r, 10 + r));
            }

            for (var i = 0; i < _linesCount; i++)
                _lines[i] = new Line(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(-80, -i),
                    new Size(20, 0));

            _BaseObjects = new List<BaseObject>();
            _BaseObjects.AddRange(_stars);
            _BaseObjects.AddRange(_lines);
            _BaseObjects.AddRange(_baseObjects);
        }

        private static void Update()
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