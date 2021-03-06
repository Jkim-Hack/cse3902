﻿using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
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
            (items as List<IItem>).RemoveAll(x => x.Equals(item));
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
                if (castedItems[i].IsKept)
                {
                    oldList.Add(castedItems[i]);
                }
            }

            items.Clear();

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
