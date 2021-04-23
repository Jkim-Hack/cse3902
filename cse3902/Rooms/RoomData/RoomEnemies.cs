using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Entities;
using Microsoft.Xna.Framework;
using System.Collections;
using cse3902.Constants;

namespace cse3902.Rooms
{
    public class RoomEnemies
    {
        public IList enemies;
        private static RoomEnemies instance = new RoomEnemies();
        public static RoomEnemies Instance
        {
            get
            {
                return instance;
            }
        }
        private RoomEnemies()
        {
            enemies = new List<IEntity>();
        }

        public void AddEnemy(IEntity enemy)
        {
            enemies.Add(enemy);
        }

        public void RemoveEnemy(IEntity enemy)
        {
            if (!(enemy is Link))
            {
                enemy.Die();
            }
            (enemies as List<IEntity>).RemoveAll(x => x.Center == enemy.Center);
        }

        public void Update(GameTime gameTime)
        {
            if (CloudAnimation.Instance.cloudAnims.Count == 0)
            {
                foreach (IEntity enemy in enemies)
                {
                    enemy.Update(gameTime);
                }
            }
        }
        public void Draw()
        {
            if (CloudAnimation.Instance.cloudAnims.Count == 0)
            {
                foreach (IEntity enemy in enemies)
                {
                    enemy.Draw();
                }
            }
        }

        public void LoadNewRoom(ref List<IEntity> oldList, List<IEntity> newList, Game1 game)
        {
            oldList = new List<IEntity>();

            List<IEntity> enemytemp = enemies as List<IEntity>;
            for (int i = 0; i < enemies.Count; i++)
            {
                enemytemp[i].Stunned = (false, 0);
                oldList.Add(enemytemp[i]);
            }
            enemies.Clear();

            for (int i = 0; i < newList.Count; i++)
            {
                enemies.Add(newList[i]);
            }

            CloudAnimation.Instance.LoadNewRoom(newList, game.SpriteBatch);
        }

        public void KillAll()
        {
            foreach (IEntity enemy in enemies)
            {
                enemy.Die();
            }
            enemies.Clear();
        }
        public void ClockStun()
        {
            foreach (IEntity enemy in enemies)
            {
                enemy.Stunned = (true, DamageConstants.ClockStunDuration);
            }
        }

        public ref IList ListRef { get => ref enemies; }
    }
}
