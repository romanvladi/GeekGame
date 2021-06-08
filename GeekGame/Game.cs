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

            Timer timer = new Timer();
            timer.Interval = 30;
            timer.Tick += Timer_Tick;
            timer.Start();

            form.KeyDown += OnKeyDown;
        }

        private static void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                _foot.Up();

            if (e.KeyCode == Keys.Down)
                _foot.Down();

            if (e.KeyCode == Keys.Left)
                _foot.Left();

            if (e.KeyCode == Keys.Right)
                _foot.Right();
        }

        public static void Load()
        {
            _ball = new Ball(new Point(30, Height/2), new Point(3, -3), new Size(30, 30));

            _foot = new Foot(new Point(25, Height / 2), new Point(10, 10), new Size(50, 50));

            _team1 = new List<BaseObjectClass>(countPlayer);

            for (int i = 0; i < countPlayer; i++)
            {
                _team1.Add(new Player(new Point(30, i * 5 + 30), new Point(i + 1, -i - 1), new Size(40, 36)));
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.DarkGreen);

            Buffer.Graphics.DrawImage(imgField, pointField);

            _ball.Draw();

            foreach (BaseObjectClass unit in _team1)
            {
                unit.Draw();
            }

            _foot.Draw();

            Buffer.Render();
        }

        public static void Update()
        {
            _ball.Update();
            for (int i = 0; i < _team1.Count; i++)
            {
                _team1[i].Update();
                if (_team1[i].Collision(_ball))
                {
                    _team1.RemoveAt(i);
                    _ball.Clash();
                }
            }            
        }       
    }
}
