using System.Windows.Forms;

namespace GeekGame.Scenes
{
    public interface IScene
    {
        void Init(Form form);
        void Draw();
    }
}
