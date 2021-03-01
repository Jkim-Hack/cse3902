using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using cse3902.Entities;
using System;

namespace cse3902.Rooms
{
    public class RoomHandler
    {
        private List<Room> rooms;

        private int roomIndex = 0;

        public RoomHandler()
        {



        }

        

        public void CycleNext()
        {
            roomIndex++;
            if (roomIndex >= rooms.Count)
            {
                roomIndex = 0;
            }
             
        }

        public void CyclePrev()
        {
            roomIndex--;
            if (roomIndex < 0)
            {
                roomIndex = rooms.Count - 1;
            }
        }
    }
}
