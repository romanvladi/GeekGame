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

        static Ball[] _balls;
        static Player[] _team1;

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
        }

        public static void Load()
        {
            _balls = new Ball[4];

            for (int i = 0; i < _balls.Length; i++)
            {
                _balls[i] = new Ball(new Point(21, i*5+21), new Point(i + 1, -i - 1));
            }

            _team1 = new Player[4];

            for (int i = 0; i < _team1.Length; i++)
            {
                _team1[i] = new Player(new Point(100, i * 5 + 100), new Point(i + 1, -i - 1));
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

            foreach (var ball in _balls)
            {
                ball.Draw();
            }

            foreach (var unit in _team1)
            {
                unit.Draw();
            }

            Buffer.Render();
        }

        public static void Update()
        {
            foreach (var ball in _balls)
            {
                ball.Update();
            }

            foreach (var unit in _team1)
            {
                unit.Update();
            }
        }       
    }
}
