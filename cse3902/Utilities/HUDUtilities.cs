using cse3902.HUD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace cse3902.Utilities
{
    public class HUDUtilities
    {
        public const float BackgroundLayer = 1.0f;

        /* Minimap layers */
        public const float MinimapLabelLayer = 0.9f;
        public const float MinimapLayer = 0.9f;
        public const float MinimapTriforceLayer = 0.8f;
        public const float MinimapCurrentRoomLayer = 0.7f;

        /* Map/compass label layers */
        public const float MapCompassLabelLayer = 0.9f;
        public const float MapCoverLayer = 0.8f;
        public const float CompassLayer = 0.8f;

        /* Orange map layers */
        public const float OrangeMapLayer = 0.9f;
        public const float OrangeMapRoomLayer = 0.8f;
        public const float OrangeMapCurrentRoomLayer = 0.7f;

        /* Health layers */
        public const float HealthHUDLayer = 0.8f;
        public const float HeartsLayer = 0.7f;

        /* Inventory layers */
        public const float InventoryHUDLayer = 0.8f;
        public const float InventoryItemLayer = 0.7f;

        private static readonly Dictionary<InventoryManager.ItemType, int> HUDItemTexturePositions = new Dictionary<InventoryManager.ItemType, int>() { { InventoryManager.ItemType.None, 12 },
            { InventoryManager.ItemType.WoodSword, 0 }, { InventoryManager.ItemType.WhiteSword, 1 }, { InventoryManager.ItemType.MagicalSword, 2 }, { InventoryManager.ItemType.Boomerang, 3 },
            { InventoryManager.ItemType.Bomb, 4 }, { InventoryManager.ItemType.Arrow, 5 }, { InventoryManager.ItemType.Bow, 6 },{ InventoryManager.ItemType.BlueCandle, 7 },
            { InventoryManager.ItemType.BluePotion, 8 }, { InventoryManager.ItemType.MagicalRod, 9 }, { InventoryManager.ItemType.BlueRing, 10 }, { InventoryManager.ItemType.MagicBook, 11 }};

        public static Dictionary<InventoryManager.ItemType, int> HUDItemIndexDict
        {
            get => HUDItemTexturePositions;
        }

        public static void DrawRectangle(Game1 game, Rectangle rec, Color color, int offsetX, int offsetY, float layer)
        {
            Texture2D texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });

            game.SpriteBatch.Draw(texture, new Rectangle(rec.X + offsetX, rec.Y + offsetY, rec.Width, rec.Height), null, color, 0, new Vector2(), SpriteEffects.None, layer);
        }

        public static void DrawTexture(Game1 game, Texture2D texture, Rectangle rec, int offsetX, int offsetY, float layer, Rectangle? frame = null)
        {
            game.SpriteBatch.Draw(texture, new Rectangle(rec.X + offsetX, rec.Y + offsetY, rec.Width, rec.Height), frame, Color.White, 0, new Vector2(), SpriteEffects.None, layer);
        }
    }
}