using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekGame
{
    interface ICollision
    {
        bool Collision(BaseObjectClass obj);
        Rectangle Rect { get; }
    }
}
