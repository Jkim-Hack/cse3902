using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections;

namespace cse3902.Rooms
{
    public class RoomNPCs
    {
        public IList npcs;

        private static RoomNPCs instance = new RoomNPCs();

        public static RoomNPCs Instance
        {
            get
            {
                return instance;
            }
        }

        private RoomNPCs()
        {
            npcs = new List<IEntity>();
        }

        public void AddNPC(INPC npc)
        {
            npcs.Add(npc);
        }

        public void RemoveEnemy(INPC npc)
        {
            (npcs as List<INPC>).RemoveAll(x => x.Center == npc.Center);
            //enpcs.Remove(enemy);
        }

        public void Update(GameTime gameTime)
        {
            foreach (INPC npc in npcs)
            {
                npc.Update(gameTime);
            }
        }

        public void Draw()
        {
            foreach (INPC npc in npcs)
            {
                npc.Draw();
            }
        }

        public void LoadNewRoom(ref List<INPC> oldList, List<INPC> newList)
        {
            oldList = new List<INPC>();

            List<INPC> npctemp = npcs as List<INPC>;

            for (int i = 0; i < npcs.Count; i++)
            {
                oldList.Add(npctemp[i]);
            }

            npcs.Clear();

            for (int i = 0; i < newList.Count; i++)
            {
                npcs.Add(newList[i]);
            }
        }

        public ref IList ListRef
        {
            get => ref npcs;
        }

    }
}
