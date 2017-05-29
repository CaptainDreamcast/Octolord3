using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class CollisionDetector
    {
        public bool detect(PhysicsObject a, PhysicsObject b)
        {
            var dx = b.x - a.x;
            var dy = b.y - a.y;
            var l = Math.Sqrt(dx * dx + dy * dy);

            return l < a.rad + b.rad;
        }
    }
}
