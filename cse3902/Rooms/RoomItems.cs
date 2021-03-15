using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections;

namespace cse3902.Rooms
{
    public class RoomItems
    {
        public IList items;

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
            items = new List<IItem>();
        }

        public void AddItem(IItem item)
        {
            items.Add(item);
        }

        public void RemoveItem(IItem item)
        {
            items.RemoveAll(x => x.Center == item.Center);
            //items.Remove(item);
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

            List<IItem> castedItems = items as List<IItem>;
            
	        for (int i = 0; i < items.Count; i++)
            {
                oldList.Add(castedItems[i]);
            }

            items = new List<IItem>();

            for (int i = 0; i < newList.Count; i++)
            {
                items.Add(newList[i]);
            }

        }

        public ref IList ListRef
        {
            get => ref items;
        }

    }
}
