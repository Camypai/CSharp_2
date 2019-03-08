using System;
using System.Drawing;
using CompGame.Interfaces;

namespace CompGame.Models
{
    public class Kit : BaseObject
    {
        private ILog _log = new ConsoleLog<Kit>();

        private readonly Image _kit;
        
        /// <summary>
        /// Колличество восполняемой энергии корабля
        /// </summary>
        public int Power { get; } = 3;
        
        /// <summary>
        /// Инициализация аптечки
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        /// <param name="log">Метод логгирования</param>
        public Kit(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            _kit = Image.FromFile(
                @"Images\heal.png",
                true).GetThumbnailImage(Size.Width, Size.Height, null, IntPtr.Zero);
//            _log.Write("Создан");
        }

        /// <summary>
        /// Отрисовка аптечки
        /// </summary>
        public override void Draw()
        {
            Scene.Buffer.Graphics.DrawImage(_kit, Pos);
//            _log.Write("Отрисован");
        }

        /// <summary>
        /// Изменение положения аптечки
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Reload();
//            _log.Write("Изменилось положение");
        }

        /// <summary>
        /// Перезагрузка объекта аптечки
        /// </summary>
        public override void Reload()
        {
            Pos = new Point(Scene.Width, new Random().Next(Scene.Height));
            _log.Write("Перезагрузка");
        }
    }
}