using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekGame
{
    abstract class BaseObjectClass : ICollision
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Sizes;

        public Rectangle Rect => new Rectangle(Pos, Sizes);

        public BaseObjectClass(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Sizes = size;
        }
        public abstract void Draw();
        public virtual void Update() { }
        public virtual void Clash() { }
        public bool Collision(BaseObjectClass obj)
        {
            return obj.Rect.IntersectsWith(Rect);
        }
    }
}
