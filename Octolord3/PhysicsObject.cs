using System;

namespace App1
{
    public class PhysicsObject
    {
        internal float ax;
        internal float ay;
        internal float rad;
        internal float vx;
        internal float vy;
        internal float x;
        internal float y;

        public PhysicsObject(PhysicsObject inp)
        {
            this.x = inp.x;
            this.y = inp.y;
            this.vx = inp.vx;
            this.vy = inp.vy;
            this.ax = inp.ax;
            this.ay = inp.ay;
            this.rad = inp.rad;
        }

        public PhysicsObject(Position p, float rad)
        {
            x = p.x;
            y = p.y;
            vx = 0;
            vy = 0;
            ax = 0;
            ay = 0;
            this.rad = rad;
        }

        public void addAccel(float dx, float dy)
        {
            ax += dx;
            ay += dy;
        }

        public float getAccelX()
        {
            return ax;
        }

        public float getAccelY()
        {
            return ay;
        }
    }
}