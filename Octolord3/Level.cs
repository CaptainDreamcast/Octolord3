using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public enum GameState {
        ALIVE,
        WIN,
        LOSS
    }

    class Level
    {

      

        Cat cat;
        List<Enemy> enemies;
        Physics physics;
        CollisionDetector collisionDetector;
        Stage stage;
        private static float stageSpeed = 0.5f;

        public Level(System.Media.SoundPlayer boing) {
            stage = new Stage();
            cat = new Cat(boing);
            enemies = new List<Enemy>();
            physics = new Physics();
            collisionDetector = new CollisionDetector();
            
        }

        private void updateStage(float timeDelta) {
            stage.move(timeDelta, stageSpeed);

            List<Enemy> nEnemy = new List<Enemy>();
            foreach (var enemy in enemies)
            {
                var alive = enemy.isAlive();
                if (alive)
                {
                    nEnemy.Add(enemy);
                }
            }
            enemies = nEnemy;

            var newEnemies = stage.getNewEnemies(cat.getPosition());
            enemies.AddRange(newEnemies);
        }

        public void update(float timeDelta) {
            updateStage(timeDelta);
            updatePhysics(cat, enemies);
            updateCollisions(cat, enemies);
            updateControls(cat);
            updateAI(enemies);
            var state = checkGameState();
        }

        private GameState checkGameState()
        {
            if (cat.isDead()) return GameState.LOSS;
            if (stage.isOver()) return GameState.WIN;
            return GameState.ALIVE; 
        }

        private void updateAI(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.updateAI();
            }
        }

        private void updateControls(Cat cat)
        {
            cat.updateControls();
        }

        private void updateCollisions(Cat cat, List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                var coll = collisionDetector.detect(cat.getPhysics(), enemy.getPhysics());
                if (coll) {
                    cat.die();
                }
            }
        }

        private void updatePhysics(Cat cat, List<Enemy> enemies)
        {
            physics.update(cat.getPhysics(), cat.getPhysicsCallback());

            foreach (var enemy in enemies)
            {
                physics.update(enemy.getPhysics(), enemy.getPhysicsCallback());
            }
        }

        public List<DrawElement> getDrawElements() {
            var r = new List<DrawElement>();
            r.Add(stage.getDrawElement());
            r.Add(cat.getDrawElement());
            
            foreach (var enemy in enemies)         
            {
                r.Add(enemy.getDrawElement());
            }

            return r;
        }

    }
}
