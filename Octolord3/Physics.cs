using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class Physics
    {

        public void update(PhysicsObject inp, Action<PhysicsObject> f)
        {
            var work = new PhysicsObject(inp);

            work.vx += work.ax;
            work.vy += work.ay;

            work.ax = 0;
            work.ay = 0;

            work.x += work.vx;
            work.y += work.vy;

            

            f(work);
        }
    }
}
