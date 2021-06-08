using GeekGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeekGame
{
    class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        static BaseObjectClass _ball;
        static Foot _foot;
        static List<BaseObjectClass> _team1;
        static int countPlayer = 6;

        static Image imgField = Resources.football_field_1;
        static Point pointField = new Point(0,0);

        static Timer timer;


        public static int Width { get; set; }
        public static int Height { get; set; }

        public static void Init(Form form)
        {
            _context = BufferedGraphicsManager.Current;
            Graphics g = form.CreateGraphics();

            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();

            timer = new Timer();
            timer.Interval = 30;
            timer.Start();
            timer.Tick += Timer_Tick;

            form.KeyDown += OnKeyDown;
        }

       
        public static void Load()
        {
            _foot = new Foot(new Point(Width-75, Height/2), new Point(10, 10), new Size(50, 50));
            _foot.FootDieExtended += OnFootDieExtended;

            _team1 = new List<BaseObjectClass>(countPlayer);

            for (int i = 0; i < countPlayer; i++)
            {
                _team1.Add(new Unit(new Point(30, i * 5 + 30), new Point(i + 1, -i - 1), new Size(40, 36)));
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        private static void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                _foot.Up();

            if (e.KeyCode == Keys.Down)
                _foot.Down();

            if (e.KeyCode == Keys.Space)
                _ball = new Ball(new Point(_foot.Rect.X, _foot.Rect.Y + 10), new Point(-10, 0), new Size(30, 30));
        }

        private static void OnFootDieExtended(object sender, FootDieEventArgs e)
        {
            timer.Stop();
            Buffer.Graphics.DrawString("Game Over!", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 100, 50);
            Buffer.Render();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.DarkGreen);

            Buffer.Graphics.DrawImage(imgField, pointField);

            _foot.Draw();

            foreach (BaseObjectClass unit in _team1)
            {
                unit.Draw();
            }

            if (_ball != null)
                _ball.Draw();


            Buffer.Render();
        }

        public static void Update()
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
                //------------------------------------------
                if (_foot.Collision(_team1[i]))
                {
                    _team1.RemoveAt(i);
                    _foot.Clash();
                }
                //-----------------------------------------
            }

            if (_ball != null)
                _ball.Update();
        }       
    }
}
