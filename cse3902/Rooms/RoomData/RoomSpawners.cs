using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections;

namespace cse3902.Rooms
{
    public class RoomSpawners
    {
        public IList spawners;

        private static RoomSpawners instance = new RoomSpawners();

        public static RoomSpawners Instance
        {
            get
            {
                return instance;
            }
        }

        private RoomSpawners()
        {
            spawners = new List<ISpawner>();
        }

        public void AddSpawner(ISpawner Spawner)
        {
            spawners.Add(Spawner);
        }

        public void LoadNewRoom(ref List<ISpawner> oldList, List<ISpawner> newList)
        {
            oldList = new List<ISpawner>();

            List<ISpawner> spawnersCast = spawners as List<ISpawner>;

            for (int i = 0; i < spawners.Count; i++)
            {
                oldList.Add(spawnersCast[i]);
            }

            spawners.Clear();

            for (int i = 0; i < newList.Count; i++)
            {
                spawners.Add(newList[i]);
            }

        }

        public void Reset()
        {
            foreach (ISpawner spawner in spawners)
            {
                spawner.Reset();
            }
        }

        public ref IList ListRef
        {
            get => ref spawners;
        }
    }
}
