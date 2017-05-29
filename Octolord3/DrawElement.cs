using System;

namespace App1
{
    public  class DrawElement
    {
        internal int id;
        internal double x;
        internal double y;
        public double width;
        public double height;
        public string path;
        private static int gId = 0;

        public bool invX;
        public bool invY;

       

        public DrawElement(string path)
        {
            this.path = path;
            this.x = 0;
            this.y = 0;
            this.width = -1;
            this.height = -1;
            this.id = gId++;

            this.invX = false;
            this.invY = false;
        }

        internal void flipX()
        {
            this.invX = !invX;
        }

        internal void flipY()
        {
            this.invY = !invY;
        }

        internal void setPos(float x, float y)
        {
            this.x = (double)x;
            this.y = (double)y;
        }

        internal void setSize(double width, double height)
        {
            this.width = width;
            this.height = height;
        }
    }
}