using GeekGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekGame
{
    class Foot : BaseObjectClass
    {
        static Image imgFoot = Resources.foot;

        public Foot(Point pos, Point dir, Size size) : base(pos, dir, size) { }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(new Bitmap(imgFoot, new Size(Sizes.Width, Sizes.Height)), Pos.X, Pos.Y);
        }
        public void Up()
        {
            if (Pos.Y > 20) Pos.Y = Pos.Y - Dir.Y;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height-70) Pos.Y = Pos.Y + Dir.Y;
        }
    }
}
