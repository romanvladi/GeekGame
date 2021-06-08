using GeekGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekGame
{
    class Ball : BaseObjectClass
    {
        static Image imgBall = Resources.BALL2BLACK;

        public Ball(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(new Bitmap(imgBall, new Size(Sizes.Width, Sizes.Height)), Pos.X, Pos.Y);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;

            if (Pos.X < 20) Dir.X = -Dir.X;
            if (Pos.X > Game.Width - 53) Dir.X = -Dir.X;

            if (Pos.Y < 17) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height - 50) Dir.Y = -Dir.Y;
        }
        public override void Clash()
        {
            Dir = new Point(-Dir.X, Dir.Y);
        }
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }
        public void Left()
        {
            if (Pos.X > 0) Pos.X = Pos.X - Dir.X;
        }
        public void Right()
        {
            if (Pos.X < Game.Width) Pos.X = Pos.X + Dir.X;
        }
    }
}
