using System.Collections.Generic;
using System.Linq;

namespace cse3902.HUD
{
    public class InventoryManager
    {
        public enum SwordType
        {
            Wood,
            White,
            Magical,
            MagicalRod,
        }

        public enum ItemType
        {
            Arrow,
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
            None
        }

        public Dictionary<ItemType, int> inventory;
        private static InventoryManager instance = new InventoryManager();
        private SwordType slotA = SwordType.Wood;
        private ItemType slotB;

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
            inventory.Add(ItemType.Arrow, 0);
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

        public void AddToInventory(ItemType type)
        {
            inventory[type]++;
            if (ItemSlot == ItemType.None)
            {
                if (type == ItemType.Boomerang || type == ItemType.Bomb || (inventory[ItemType.Bow] > 0 && inventory[ItemType.Arrow] > 0))
                {
                    if (ItemType.Arrow == type)
                    {
                        ItemSlot = ItemType.Bow;
                    }
                    else
                    {
                        ItemSlot = type;
                    }
                }
            }
        }

        public void RemoveFromInventory(ItemType type)
        {
            if (inventory[type] > 0)
            {
                inventory[type]--;
                if (inventory[type] == 0 && ItemSlot == type)
                {
                    if (inventory[ItemType.Boomerang] > 0)
                    {
                        ItemSlot = ItemType.Boomerang;
                    }
                    else if (inventory[ItemType.Bow] > 0 && inventory[ItemType.Arrow] > 0)
                    {
                        ItemSlot = ItemType.Boomerang; //should this be bow or arrow?
                    }
                    else if (inventory[ItemType.Bomb] > 0)
                    {
                        ItemSlot = ItemType.Bomb;
                    }
                    else
                    {
                        ItemSlot = ItemType.None;
                    }
                }
            }
        }

        public int ItemCount(ItemType type)
        {
            return inventory[type];
        }

        public void Reset()
        {
            for (int index = 0; index < inventory.Count; index++)
            {
                KeyValuePair<ItemType, int> entry = inventory.ElementAt(index);
                inventory[entry.Key] = 0;
            }
        }

        public SwordType SwordSlot
        {
            get => slotA;
            set => slotA = value;
        }

        public ItemType ItemSlot
        {
            get => slotB;
            set => slotB = value;
        }
    }
}