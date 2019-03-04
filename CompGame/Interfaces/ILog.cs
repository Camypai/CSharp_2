namespace CompGame.Interfaces
{
    public interface ILog
    {
        /// <summary>
        /// Записывает лог
        /// </summary>
        /// <param name="message">Сообщение для записи в лог</param>
        void Write(string message);
    }
}