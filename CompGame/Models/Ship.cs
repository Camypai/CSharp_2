using System;
using System.Drawing;
using CompGame.Interfaces;

namespace CompGame.Models
{
    public class Ship : BaseObject
    {
        /// <summary>
        /// Энергия корабля (Здоровье)
        /// </summary>
        private int _energy = 100;

        private ILog _log = new ConsoleLog<Ship>();

        /// <summary>
        /// Изображение корабля
        /// </summary>
        private readonly Image _ship;

        public int Energy
        {
            get => _energy;
            private set { _energy = value > 100 ? 100 : value; }
        }

        /// <summary>
        /// Количество очков за сбитые астероиды
        /// </summary>
        public int Score = 0;
        /// <summary>
        /// Собитые уничтожения корабля
        /// </summary>
        public static event  Message MessageDie;

        /// <summary>
        /// Инициализация корабля
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        /// <param name="log">Метод логгирования</param>
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            _ship = Image.FromFile(
                @"Images\Spaceship.png",
                true).GetThumbnailImage(Size.Width, Size.Height, null, IntPtr.Zero);
//            _log.Write("Создан");
        }

        /// <summary>
        /// Отрисовка корабля
        /// </summary>
        public override void Draw()
        {
            Scene.Buffer.Graphics.DrawImage(_ship, Pos);
//            _log.Write("Отрисован");
        }

        /// <summary>
        /// Автоматическое изменение положения корабля
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Перезагрузка корабля
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Reload()
        {
            throw new System.NotImplementedException();
        }
        
        /// <summary>
        /// Изменение количества энергии
        /// </summary>
        /// <param name="n">Количество энергии</param>
        public void EnergyChange(int n)
        {
            Energy += n;
            _log.Write($"Изменился запас энергии: {n}");
        }
        
        /// <summary>
        /// Пополнение счётчика очков
        /// </summary>
        public void ScoreAdd()
        {
            Score++;
            _log.Write($"Астероид сбит");
        }
        
        /// <summary>
        /// Взлёт корабля
        /// </summary>
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
            _log.Write("Поднялся");
        }
        
        /// <summary>
        /// Снижение корабля
        /// </summary>
        public void Down()
        {
            if (Pos.Y < Scene.Height) Pos.Y = Pos.Y + Dir.Y;
            _log.Write("Опустился");
        }
        
        /// <summary>
        /// Смерть корабля
        /// </summary>
        public override void Die()
        {
            MessageDie?.Invoke();
            _log.Write("Умер");
        }
    }
}