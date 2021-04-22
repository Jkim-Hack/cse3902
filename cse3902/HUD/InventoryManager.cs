using cse3902.Sounds;
using cse3902.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using cse3902.Rooms;

namespace cse3902.HUD
{
    public class InventoryManager
    {
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
            BlueCandle,
            BluePotion,
            BlueRing,
            WoodSword,
            WhiteSword,
            MagicalSword,
            MagicalRod,
            None,
            MagicBook,
        }

        public Dictionary<ItemType, int> inventory;
        private static InventoryManager instance = new InventoryManager();
        private ItemType slotA = ItemType.WoodSword;
        private ItemType slotB = ItemType.None;

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
            foreach (int i in Enum.GetValues(typeof(ItemType)))
            {
                inventory.Add((ItemType)i, 0);
            }
        }
        public bool canEquip(ItemType type)
        {
            if (type != ItemType.None && inventory[type] > 0 && InventoryUtilities.EquipableItemsList.Contains(type))
            {
                if (type == ItemType.Arrow)
                {
                    if (inventory[ItemType.Arrow] > 0 && inventory[ItemType.Bow] > 0 && inventory[ItemType.Rupee] > 0)
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public void AddToInventory(ItemType type)
        {
            if (type == ItemType.Clock)
            {
                RoomEnemies.Instance.KillAll(); 
            }
            inventory[type] += InventoryUtilities.itemsCollectedPerItem(type);
            if (inventory[type] > InventoryUtilities.maxItemCount(type)) inventory[type] = InventoryUtilities.maxItemCount(type);
            playItemSounds(type);
            if (ItemSlot == ItemType.None)
            {
                slotB = ItemType.None;
                foreach (ItemType possibleNext in InventoryUtilities.EquipableItemsList)
                {
                    if (canEquip(possibleNext))
                    {
                        slotB = possibleNext;
                        break;
                    }
                }
            }
            if (InventoryUtilities.SwordsList.Contains(type) && InventoryUtilities.convertSwordToInt(type) > InventoryUtilities.convertSwordToInt(slotA))
            {
                slotA = type;
            }
        }

        public void RemoveFromInventory(ItemType type)
        {
            if (inventory[type] > 0 && InventoryUtilities.decrementableItems.Contains(type))
            {
                inventory[type]--;
                if (inventory[type] == 0 && ItemSlot == type || (ItemSlot == ItemType.Arrow) && inventory[ItemType.Rupee] <= 0)
                {
                    slotB = ItemType.None;
                    foreach (ItemType possibleNext in InventoryUtilities.EquipableItemsList)
                    {
                        if (canEquip(possibleNext))
                        {
                            slotB = possibleNext;
                            break;
                        }
                    }
                }
            }
        }

        public int ItemCount(ItemType type)
        {
            return inventory[type];
        }

        private void playItemSounds(ItemType type)
        {
            if (type == InventoryManager.ItemType.Heart || type == InventoryManager.ItemType.Key)
            {
                SoundFactory.PlaySound(SoundFactory.Instance.getHeart);
            }
            else if (type == InventoryManager.ItemType.Rupee)
            {
                SoundFactory.PlaySound(SoundFactory.Instance.getRupee);
            }
            else
            {
                SoundFactory.PlaySound(SoundFactory.Instance.getItem);
            }
        }

        public void Reset()
        {
            for (int index = 0; index < inventory.Count; index++)
            {
                KeyValuePair<ItemType, int> entry = inventory.ElementAt(index);
                inventory[entry.Key] = 0;
            }
        }

        public ItemType SwordSlot
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