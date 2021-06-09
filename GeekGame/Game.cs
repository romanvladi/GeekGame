using GeekGame.Properties;
using GeekGame.Scenes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeekGame
{
    class Game : BaseScene
    {
        private Ball _ball;
        private Foot _foot;
        private List<BaseObjectClass> _team1;
        private int _countPlayer = 4;

        readonly Image _imgField = Resources.football_field_1;
        private Point _pointField = new Point(0, 0);

        private Timer timer;
        readonly Rectangle _rect = new Rectangle( 20, 200, 2, 110 );
        private int _goals = 0;
        private int _countBalls = 10;

        public event EventHandler GameOver;

        public override void Init(Form form)
        {
            base.Init(form);

            Load();

            timer = new Timer();
            timer.Interval = 30;
            timer.Start();
            timer.Tick += Timer_Tick;

            this.GameOver += Game_GameOver;
        }

        private void Game_GameOver(object sender, EventArgs e)
        {
            timer.Stop();
            Buffer.Graphics.DrawString("Game Over", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold), Brushes.White, 100, 50);
            Buffer.Graphics.DrawString("<Backspace> - в меню", new Font(FontFamily.GenericSansSerif, 40, FontStyle.Underline), Brushes.White, 40, 400);
            Buffer.Render();
        }

        public void Load()
        {
            _foot = new Foot(new Point(Width-75, Height/2), new Point(10, 10), new Size(50, 50));

            _team1 = new List<BaseObjectClass>(_countPlayer);

            for (int i = 0; i < _countPlayer; i++)
            {
                _team1.Add(new Unit(new Point(30, i * 5 + 30), new Point(i + 1, -i - 1), new Size(40, 36)));
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public override void SceneKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                _foot.Up();

            if (e.KeyCode == Keys.Down)
                _foot.Down();

            if (e.KeyCode == Keys.Space)
            {
                _ball = new Ball(new Point(_foot.Rect.X, _foot.Rect.Y + 10), new Point(-10, 0), new Size(30, 30));
                _ball.Goal += Ball_Goal;
                _countBalls -= 1;
            }
            if (e.KeyCode == Keys.Back)
            {
                SceneManager
                    .Get()
                    .Init<MenuScene>(_form)
                    .Draw();
            }
        }

        private static void Ball_Goal(object sender, GoalEventArgs e)
        {
            Buffer.Graphics.DrawString("GOAL!", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold), Brushes.White, 200, 100);
            Buffer.Render();
        }

        public void Draw()
        {
            Buffer.Graphics.Clear(Color.DarkGreen);

            Buffer.Graphics.DrawImage(_imgField, _pointField);

            Buffer.Graphics.DrawRectangle(new Pen(Color.Red), _rect);

            _foot.Draw();

            foreach (BaseObjectClass unit in _team1)
            {
                unit.Draw();
            }

            if (_ball != null)
                _ball.Draw();

            Buffer.Graphics.DrawString($"Goals: {_goals}", SystemFonts.DefaultFont, Brushes.White, 20, 0, StringFormat.GenericTypographic);
            Buffer.Graphics.DrawString($"BalLs: {_countBalls}", SystemFonts.DefaultFont, Brushes.White, Width-70, 0, StringFormat.GenericTypographic);

            Buffer.Render();
        }

        public void Update()
        {
            for (int i = 0; i < _team1.Count; i++)
            {
                _team1[i].Update();

                if (_ball != null && _ball.Collision(_team1[i]))
                {
                    _team1[i].Clash();
                    _ball = null;
                    continue;
                }                                
            }

            if (_ball != null)
            {
                _ball.Update();
                if (_ball.Rect.IntersectsWith(_rect))
                {
                    _ball.Clash();
                    _goals += 1;
                }
            }
            if (_countBalls == -1)
            {
                GameOver.Invoke(null, new EventArgs());
            }

        }

        public override void Dispose()
        {
            base.Dispose();
            timer.Stop();
        }
    }
}
