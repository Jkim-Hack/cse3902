using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections;

namespace cse3902.Rooms
{
    public class RoomEnemyNPCs
    {
        public IList enpcs;

        private static RoomEnemyNPCs instance = new RoomEnemyNPCs();

        public static RoomEnemyNPCs Instance
        {
            get
            {
                return instance;
            }
        }

        private RoomEnemyNPCs()
        {
            enpcs = new List<IEntity>();
        }

        public void AddENPC(IEntity enemy)
        {
            enpcs.Add(enemy);
        }

        public void RemoveENPC(IEntity enemy)
        {
            (enpcs as List<IEntity>).RemoveAll(x => x.Center == enemy.Center);
            //enpcs.Remove(enemy);
        }

        public void Update(GameTime gameTime)
        {
            foreach (IEntity enemy in enpcs)
            {
                enemy.Update(gameTime);
            }

        }

        public void Draw()
        {
            foreach (IEntity enemy in enpcs)
            {
                enemy.Draw();
            }
        }

        public void LoadNewRoom(ref List<IEntity> oldList, List<IEntity> newList)
        {
            oldList = new List<IEntity>();

            List<IEntity> entitiesNPCs = enpcs as List<IEntity>;

            for (int i = 0; i < enpcs.Count; i++)
            {
                oldList.Add(entitiesNPCs[i]);
            }

            enpcs.Clear();

            for (int i = 0; i < newList.Count; i++)
            {
                enpcs.Add(newList[i]);
            }

        }

        public ref IList ListRef
        {
            get => ref enpcs;
        }

    }
}
