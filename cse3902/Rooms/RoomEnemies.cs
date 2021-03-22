using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Entities;
using Microsoft.Xna.Framework;
using System.Collections;

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
            //enpcs.Remove(enemy);
        }

        public void Update(GameTime gameTime)
        {
            foreach (IEntity enemy in enemies)
            {
                enemy.Update(gameTime);
            }
        }

        public void Draw()
        {
            foreach (IEntity enemy in enemies)
            {
                enemy.Draw();
            }
        }

        public void LoadNewRoom(ref List<IEntity> oldList, List<IEntity> newList)
        {
            oldList = new List<IEntity>();

            List<IEntity> enemytemp = enemies as List<IEntity>;

            for (int i = 0; i < enemies.Count; i++)
            {
                oldList.Add(enemytemp[i]);
            }

            enemies.Clear();

            for (int i = 0; i < newList.Count; i++)
            {
                enemies.Add(newList[i]);
            }
        }

        public ref IList ListRef
        {
            get => ref enemies;
        }

    }
}
