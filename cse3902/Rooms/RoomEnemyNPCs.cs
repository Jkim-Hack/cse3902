using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
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

        }

        public void AddENPC(IEntity enemy)
        {
            enpcs.Add(enemy);
        }

        public void RemoveENPC(IEntity enemy)
        {
            enpcs.Remove(enemy);
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

            List<IEntity> entitiesENPCs = enpcs as List<IEntity>;

            for (int i = 0; i < enpcs.Count; i++)
            {
                oldList.Add(entitiesENPCs[i]);
            }

            enpcs = new List<IEntity>();

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
