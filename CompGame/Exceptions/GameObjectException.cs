using System;
using System.Windows.Forms;

namespace CompGame.Exceptions
{
    public class GameObjectException : Exception
    {
        public GameObjectException()
        {
            var result = MessageBox.Show("Ошибка создания игрового объекта", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if(result == DialogResult.OK)
                Application.Exit();
        }
        
        public GameObjectException(string message)
        {
            var result = MessageBox.Show($"Ошибка создания игрового объекта: {message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if(result == DialogResult.OK)
                Application.Exit();
        }
    }
}