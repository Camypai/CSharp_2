using System;
using CompGame.Interfaces;

namespace CompGame.Models
{
    /// <summary>
    /// Фабрика создания элементов игры
    /// </summary>
    public abstract class Creator<T> where T : BaseObject
    {
        /// <summary>
        /// Создаёт объект с рандомными параметрами
        /// </summary>
        /// <param name="rnd">экземпляр класса Random</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual T Create(Random rnd)
        {
            throw new System.NotImplementedException();
        }
        
        /// <summary>
        /// Создаёт объект со статичными параметрами
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual T Create()
        {
            throw new System.NotImplementedException();
        }
    }
}