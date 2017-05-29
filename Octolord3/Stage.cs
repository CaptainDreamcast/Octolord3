using System;
using System.Collections.Generic;

namespace App1
{
    internal class Stage
    {
        DrawElement drawElement;
        Random gen;

        static float screenX = 1980;
        static float screenY = 1080;

        public Stage() {
            gen = new Random(this.GetHashCode());
            this.drawElement = new DrawElement("Assets/stage.png");
            this.drawElement.setPos(screenX, screenY);
        }

        public bool isOver()
        {
            return false;
        }

        public void move(float timeDelta, float stageSpeed)
        {
            

        }

        public List<Enemy> getNewEnemies(Position aim)
        {
            List<Enemy> nEnemies = new List<Enemy>();

            var poss = gen.NextDouble();

            if (poss < 0.01)
            {
                var val = gen.Next() % 4;
                var other = (float)gen.NextDouble();

                float x, y;

                if (val == 0 || val == 2)
                {
                    y = other * screenY;
                    if (val == 0) x = screenX;
                    else x = 0;
                }
                else
                {
                    x = other * screenX;
                    if (val == 0) y = screenY;
                    else y = 0;
                }

                var pos = new Position(x, y);

                x = aim.x - x;
                y = aim.y - y;

                var l = (float)Math.Sqrt(x * x + y * y);
                if (l == 0) { l = 1; x = 1; }
                x /= l;
                y /= l;


                if(l > 200)  nEnemies.Add(new Enemy(pos, new Position(x, y)));
            }

            return nEnemies;
        }

        internal DrawElement getDrawElement()
        {
            return drawElement;
        }
    }
}