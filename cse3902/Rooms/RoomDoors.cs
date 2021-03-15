using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace cse3902.Rooms
{
    public class RoomDoors
    {
        public List<IDoor> doors { get; set; }

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

        public void LoadNewRoom(ref List<IDoor> oldList, List<IDoor> newList)
        {
            oldList = new List<IDoor>();

            for (int i = 0; i < doors.Count; i++)
            {
                oldList[i] = doors[i];
            }

            doors = new List<IDoor>();

            for (int i = 0; i < newList.Count; i++)
            {
                doors[i] = newList[i];
            }

        }
    }
}
