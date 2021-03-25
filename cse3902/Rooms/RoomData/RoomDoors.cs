using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections;

namespace cse3902.Rooms
{
    public class RoomDoors
    {
        public IList doors;
        private IList oldDoors;

        private static RoomDoors instance = new RoomDoors();

        public static RoomDoors Instance
        {
            get
            {
                return instance;
            }
        }

        private RoomDoors()
        {
            doors = new List<IDoor>();
            oldDoors = new List<IDoor>();
        }

        public void AddDoor(IDoor door)
        {
            doors.Add(door);
        }

        public void Draw()
        {
            foreach (IDoor door in doors)
            {
                door.Draw();
            }
        }

        public void DrawOld()
        {
            foreach (IDoor door in oldDoors)
            {
                door.Draw();
            }
        }

        public void LoadNewRoom(ref List<IDoor> oldList, List<IDoor> newList)
        {
            oldList = new List<IDoor>();
            oldDoors = oldList;

            List<IDoor> doorsCast = doors as List<IDoor>;

            for (int i = 0; i < doors.Count; i++)
            {
                oldList.Add(doorsCast[i]);
            }

            doors.Clear();

            for (int i = 0; i < newList.Count; i++)
            {
                doors.Add(newList[i]);
            }

        }

        public void Reset()
        {
            foreach (IDoor door in doors)
            {
                door.Reset();
            }
        }

        public ref IList ListRef
        {
            get => ref doors;
        }
    }
}
