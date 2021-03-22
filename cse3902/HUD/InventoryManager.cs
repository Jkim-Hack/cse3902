using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Items;

namespace cse3902.HUD
{
    public class InventoryManager
    {
        public enum ItemType
        {
            Bow,
            Clock,
            Compass,
            Fairy,
            Map,
            Heart,
            HeartContainer,
            Bomb,
            Key,
            Rupee,
            Triforce
        }

        public Dictionary<ItemType, int> inventory;

        public InventoryManager()
        {
            inventory = new Dictionary<ItemType, int>();
            inventory.Add(ItemType.Heart, 0);
            inventory.Add(ItemType.HeartContainer, 0);
            inventory.Add(ItemType.Bomb, 0);
            inventory.Add(ItemType.Key, 0);
            inventory.Add(ItemType.Rupee, 0);

        }

        public void AddToInventory(IItem item)
        {
            if (item is HeartItem)
            {
                inventory[ItemType.Heart]++;
            }
        }
    }
}
