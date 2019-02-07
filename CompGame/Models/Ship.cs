using System;
using System.Drawing;

namespace CompGame.Models
{
    public class Ship : BaseObject
    {
        /// <summary>
        /// Энергия корабля (Здоровье)
        /// </summary>
        private int _energy = 100;

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
        public Ship(Point pos, Point dir, Size size, EventHandler<string> log) : base(pos, dir, size, log)
        {
            Logging(this, "Создан");
        }

        /// <summary>
        /// Отрисовка корабля
        /// </summary>
        public override void Draw()
        {
            Scene.Buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, Size.Width, Size.Height);
            Logging(this, "Отрисован");
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
            Logging(this, $"Изменился запас энергии: {n}");
        }
        
        /// <summary>
        /// Пополнение счётчика очков
        /// </summary>
        public void ScoreAdd()
        {
            Score++;
            Logging(this, $"Астероид сбит");
        }
        
        /// <summary>
        /// Взлёт корабля
        /// </summary>
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
            Logging(this, "Поднялся");
        }
        
        /// <summary>
        /// Снижение корабля
        /// </summary>
        public void Down()
        {
            if (Pos.Y < Scene.Height) Pos.Y = Pos.Y + Dir.Y;
            Logging(this, "Опустился");
        }
        
        /// <summary>
        /// Смерть корабля
        /// </summary>
        public override void Die()
        {
            MessageDie?.Invoke();
            Logging(this, "Умер");
        }
    }
}