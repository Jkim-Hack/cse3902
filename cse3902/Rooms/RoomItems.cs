using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace cse3902.Rooms
{
    public class RoomItems
    {
        private List<IItem> items { get; set; }

        private static RoomItems instance = new RoomItems();

        public static RoomItems Instance
        {
            get
            {
                return instance;
            }
        }

        private RoomItems()
        {

        }

        public void Update(GameTime gameTime)
        {
            foreach (IItem item in items)
            {
                item.Update(gameTime);
            }

        }

        public void Draw()
        {
            foreach (IItem item in items)
            {
                item.Draw();
            }
        }


        public void LoadNewRoom(ref List<IItem> oldList, List<IItem> newList)
        {
            oldList = new List<IItem>();

            for (int i = 0; i < items.Count; i++)
            {
                oldList[i] = items[i];
            }

            items = new List<IItem>();

            for (int i = 0; i < newList.Count; i++)
            {
                items[i] = newList[i];
            }

        }

    }
}
