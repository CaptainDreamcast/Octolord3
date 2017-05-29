using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public enum EnemyState{
         ALIVE,
         DYING,
         DEAD
    }

    public enum EnemyType
    {
        ONE = 1,
        TWO = 2
    }

    class Enemy
    {
        PhysicsObject physics;
        EnemyType type;

        DrawElement drawElement;
        EnemyState state;

        static float screenX = 1920;
        static float screenY = 1080;

        static float speed = 4;
        static float radius = 60;

        public Enemy(Position p, Position aim)
        {
            Random gen = new Random(this.GetHashCode());

            physics = new PhysicsObject(p, radius);

            var x = aim.x;
            var y = aim.y;

            x *= speed;
            y *= speed;
            physics.addAccel(x, y);

            var cls = gen.Next() % 15;

            if(cls == 0) this.type = EnemyType.TWO;
            else this.type = EnemyType.ONE;
            this.state = EnemyState.ALIVE;
            if(this.type == EnemyType.ONE) this.drawElement = new DrawElement("Assets/enemy1.png");
            else this.drawElement = new DrawElement("Assets/enemy2.png");
        }

        public void updateAI()
        {
            if (physics.x < -200 || physics.x > screenX+200 || physics.y < -200 || physics.y > screenY+200)
            {
                state = EnemyState.DEAD;
            }
        }

        public void die()
        {
            state = EnemyState.DYING;
        }

        public PhysicsObject getPhysics()
        {
            return physics;
        }

        public Action<PhysicsObject> getPhysicsCallback()
        {
            Action<PhysicsObject> f = (physics) => {
                this.physics = physics;
            };

            return f;
        }

        public bool isAlive()
        {
            return state != EnemyState.DEAD;
        }

        public DrawElement getDrawElement()
        {
            drawElement.setPos(physics.x, physics.y);
            return drawElement;
        }
    }
}
