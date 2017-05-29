using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public enum CatState {
        ALIVE,
        DYING,
        DEAD
    }

    class Cat
    {
        Controller c;
        CatState state;
        PhysicsObject physics;
        
        private static float speed = 0.3f;
        private DrawElement drawElementAlive;
        private DrawElement drawElementDying;
        private DrawElement drawElementDead;

        private static int screenY = 1080;
        private static int screenX = 1920;

        private static float radius = 60;

        private System.Media.SoundPlayer boing;

        public Cat(System.Media.SoundPlayer boing)
        {
            c = new Controller();
            state = CatState.ALIVE;
            physics = new PhysicsObject(new Position(100, 100), radius);
            physics.addAccel(speed*10, speed*10);
            drawElementAlive = new DrawElement("Assets/catAlive.png");
            drawElementDying = new DrawElement("Assets/catDying.png");
            drawElementDead = new DrawElement("Assets/catDead.png");
            this.boing = boing;
        }
   
        public bool isDead()
        {
            return state == CatState.DEAD;
        }

        public void updateControls()
        {

            c.update();

            int sign = Math.Sign(physics.vx);
            physics.addAccel(sign*speed*0.01f, 0);

            if (c.isDown()) moveDown();
            if (c.isUp()) moveUp();
        }

        private void moveUp()
        {
            physics.addAccel(0, -speed);
        }

        private void moveDown()
        {
            physics.addAccel(0, speed);
        }

        public PhysicsObject getPhysics()
        {
            return physics;
        }

        public void die()
        {
            this.state = CatState.DYING;
        }

        internal Position getPosition()
        {
            return new Position(physics.x, physics.y);
        }

        public Action<PhysicsObject> getPhysicsCallback()
        { 
            Action<PhysicsObject> f = (physics) => {
                if (physics.x >= screenX)
                {
                    flipX();
                    physics.x = screenX;
                    physics.vx *= -1;
                }

                if (physics.x <= 0)
                {
                    flipX();
                    physics.x = 0;
                    physics.vx *= -1;
                }

                if (physics.y >= screenY)
                {
                    flipY();
                    physics.y = screenY;
                    physics.vy *= -1;
                }

                if (physics.y <= 0)
                {
                    flipY();
                    physics.y = 0;
                    physics.vy *= -1;
                }

            
                this.physics = physics;
            };
            return f;
        }

        private void flipX()
        {
            this.drawElementAlive.flipX();
            boing.Play();
        }

        private void flipY()
        {
            this.drawElementAlive.flipY();
            boing.Play();
        }

        internal DrawElement getDrawElement()
        {
            drawElementAlive.setPos(physics.x, physics.y);
            drawElementDying.setPos(physics.x, physics.y);
            drawElementDead.setPos(physics.x, physics.y);
            if (state == CatState.ALIVE) return drawElementAlive;
            if (state == CatState.DYING) return drawElementDying;
            else return drawElementDead;
        }
    }
}
