using System.Drawing;

namespace CompGame.Interfaces
{
    public interface ICollision
    {
        /// <summary>
        /// Проверка на столкновение
        /// </summary>
        /// <param name="collision">Объект для проверки</param>
        /// <returns>true/false</returns>
        bool Collision(ICollision collision);
        
        /// <summary>
        /// Объект, который проверяется
        /// </summary>
        Rectangle Rectangle { get; }
    }
}