using System.Collections.Generic;
using cse3902.Interfaces;
using System.Linq;

namespace cse3902.HUD
{
    public class InventoryManager
    {
        public enum ItemType
        {
            Boomerang,
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
            Triforce,
        }

        public Dictionary<ItemType, int> inventory;
        private static InventoryManager instance = new InventoryManager();

        public static InventoryManager Instance
        {
            get
            {
                return instance;
            }
        }

        private InventoryManager()
        {
            inventory = new Dictionary<ItemType, int>();
            inventory.Add(ItemType.Heart, 0);
            inventory.Add(ItemType.HeartContainer, 0);
            inventory.Add(ItemType.Bomb, 0);
            inventory.Add(ItemType.Key, 0);
            inventory.Add(ItemType.Rupee, 0);
            inventory.Add(ItemType.Map, 0);
            inventory.Add(ItemType.Compass, 0);
            inventory.Add(ItemType.Boomerang, 0);
            inventory.Add(ItemType.Bow, 0);
            inventory.Add(ItemType.Clock, 0);
            inventory.Add(ItemType.Fairy, 0);
            inventory.Add(ItemType.Triforce, 0);
        }

        public void AddToInventory(IItem item)
        {
            inventory[item.ItemType]++;
        }

        public void RemoveFromInventory(ItemType type)
        {
            inventory[type]--;
        }

        public void Reset()
        {
            for (int index = 0; index < inventory.Count; index++)
            {
                KeyValuePair<ItemType, int> entry = inventory.ElementAt(index);
                inventory[entry.Key] = 0;
            }
        }
    }
}
