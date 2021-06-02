using GeekGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeekGame
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 

        [STAThread]
        static void Main()
        {
            var form = new Form();
            form.MinimumSize = new System.Drawing.Size(656, 550);
            form.MaximumSize = new System.Drawing.Size(656, 550);
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.Icon = Resources.F;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = "EVRO 2020";

            Game.Init(form);
            //form.Show();
            //Game.Draw();
            Application.Run(form);
        }
    }
}
