using System;
using System.Collections.Generic;
using cse3902.HUD;
using Microsoft.Xna.Framework;

namespace cse3902.Constants
{
    public class HUDPositionConstants
    {
        private const int HealthHUDWidth = 70;
        private const int HealthHUDHeight = 37;
	    public static Vector2 HealthHUDPosition = new Vector2(DimensionConstants.OriginalWindowDimensions.X - HealthHUDWidth, DimensionConstants.OriginalWindowDimensions.Y - HealthHUDHeight);
        
        public static Vector2 InventoryHUDPosition = new Vector2(0, 0);
        public static Vector2 BItemHUDPosition = new Vector2(72, 56);
        public static Vector2 WeaponStartHUDPosition = new Vector2(136, 56);
        public const int InventoryGapX = 24;
        public const int InventoryGapY = 16;
        public const int InventoryItemsRows = 2;
        public const int InventoryItemsCols = 4;
        public static readonly Dictionary<InventoryManager.ItemType, Vector2> InventoryIndicatorPos = new Dictionary<InventoryManager.ItemType, Vector2> { { InventoryManager.ItemType.Arrow, new Vector2(180, 56) },
            { InventoryManager.ItemType.Bow, new Vector2(188, 56) }, { InventoryManager.ItemType.BlueRing, new Vector2(200, 32) } };

        public static Vector2 CurrentItemsHUDPosition = new Vector2(HealthHUDPosition.X - 80, HealthHUDPosition.Y);
        public static Vector2 SlotB = new Vector2(CurrentItemsHUDPosition.X + 44.5f, CurrentItemsHUDPosition.Y+16);
        public static Vector2 SlotA = new Vector2(CurrentItemsHUDPosition.X + 73f, CurrentItemsHUDPosition.Y+24);
        public static float CountsXPos = CurrentItemsHUDPosition.X + (98/11);

        public const int backgroundOffsetX = 40;
        public const int backgroundOffsetY = 40;
    }
}
