using GeekGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekGame
{
    class Player : SomeOne
    {
        static Image imgPlayer = Resources.UnitPolo;
        public Player(Point pos, Point dir) : base(pos, dir) { }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(imgPlayer, Pos);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;

            if (Pos.X < 20) Dir.X = -Dir.X;
            if (Pos.X > Game.Width - 70) Dir.X = -Dir.X;

            if (Pos.Y < 17) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height - 60) Dir.Y = -Dir.Y;
        }
    }
}
