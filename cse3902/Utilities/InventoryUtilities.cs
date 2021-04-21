using cse3902.HUD;
using static cse3902.HUD.InventoryManager;
using System.Collections.Generic;

namespace cse3902.Utilities
{
    internal class InventoryUtilities
    {
        private readonly static List<ItemType> swords = new List<ItemType> { ItemType.WoodSword,
            ItemType.WhiteSword, ItemType.MagicalSword };

        private readonly static List<ItemType> equipableItems = new List<ItemType> { ItemType.Boomerang, ItemType.Bomb,
            ItemType.Arrow, ItemType.BlueCandle, ItemType.None, ItemType.None, ItemType.BluePotion, ItemType.MagicalRod };

        private readonly static List<ItemType> collectables = new List<ItemType> { ItemType.BlueRing };

        private readonly static Dictionary<ItemType, int> maxItems = new Dictionary<ItemType, int> { { ItemType.Rupee, 255 }, { ItemType.Bomb, 6 }, { ItemType.Key, 9 } };
        private readonly static Dictionary<ItemType, int> countPerItem = new Dictionary<ItemType, int> { { ItemType.Rupee, 3 }, { ItemType.Bomb, 3 } };

        private readonly static List<ItemType> removableItems = new List<ItemType> { ItemType.Bomb, ItemType.BluePotion, ItemType.Rupee, ItemType.Key };

        public static List<ItemType> SwordsList
        {
            get => swords;
        }

        public static List<ItemType> EquipableItemsList
        {
            get => equipableItems;
        }

        public static List<ItemType> collectablesItemList
        {
            get => collectables;
        }

        public static List<ItemType> decrementableItems
        {
            get => removableItems;
        }

        public static int maxItemCount(ItemType item)
        {
            if (!maxItems.ContainsKey(item))
            {
                return 1;
            }
            else
            {
                return maxItems[item];
            }
        }
        public static int itemsCollectedPerItem(ItemType item)
        {
            if (!countPerItem.ContainsKey(item))
            {
                return 1;
            }
            else
            {
                return countPerItem[item];
            }
        }

        public static int convertSwordToInt(ItemType sword)
        {
            switch (sword)
            {
                case ItemType.WoodSword:
                    return 0;
                case ItemType.WhiteSword:
                    return 1;
                case ItemType.MagicalSword:
                    return 2;
                case ItemType.MagicalRod:
                    return 3;
            }
            return -1;
        }

        public static ItemType convertIntToSword(int sword)
        {
            switch (sword)
            {
                case 0:
                    return ItemType.WoodSword;
                case 1:
                    return ItemType.WhiteSword;
                case 2:
                    return ItemType.MagicalSword;
                case 3:
                    return ItemType.MagicalRod;
            }
            return ItemType.None;
        }
    }
}