using GeekGame.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace GeekGame.Scenes
{
    class MenuScene : BaseScene
    {
        readonly Image _imgF = Resources.stMenu;
        private Point _pointField = new Point(0, 0);

        public override void Draw()
        {
            Buffer.Graphics.Clear(Color.DarkGreen);
            Buffer.Graphics.DrawImage(new Bitmap(_imgF, new Size(656, 550)), _pointField);
            Buffer.Graphics.DrawString("Меню", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 70);
            Buffer.Graphics.DrawString("<Enter> - игра", new Font(FontFamily.GenericSansSerif, 40, FontStyle.Underline), Brushes.White, 150, 200);
            Buffer.Graphics.DrawString("<Esc> - выход", new Font(FontFamily.GenericSansSerif, 40, FontStyle.Underline), Brushes.White, 150, 300);
            Buffer.Render();
        }
        public override void SceneKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _form.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                SceneManager
                    .Get()
                    .Init<Game>(_form)
                    .Draw();
            }
        }
    }
}
