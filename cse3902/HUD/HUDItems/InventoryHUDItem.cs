using System;
using System.Collections.Generic;
using cse3902.Constants;
using cse3902.Interfaces;
using cse3902.SpriteFactory;
using cse3902.Sprites;
using cse3902.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.HUD.InventoryManager;

namespace cse3902.HUD.HUDItems
{
    public class InventoryHUDItem : IHUDItem
    {
        private Vector2 origin;
        private Vector2 center;
        private Texture2D inventoryTexture;
        private Texture2D cursorTexture;
        private Rectangle box;
        private Vector2 size;
        private SpriteBatch spriteBatch;

        private int furthestWeaponX;
        private int furthestItemX;
        private Vector2 weaponStartOrigin;
        private Vector2 itemStartOrigin;

        private (ItemType, ISprite) currentBItem;
        private Vector2 BItemOrigin;

        private Dictionary<ItemType, ISprite> drawList;

        private Rectangle cursorBox;
        private ItemType cursorBoxItem;

        public InventoryHUDItem(Game1 game, Texture2D inventoryTexture, Texture2D cursorTexture, Vector2 origin)
        {
            drawList = new Dictionary<ItemType, ISprite>();
            spriteBatch = game.SpriteBatch;
            this.inventoryTexture = inventoryTexture;
            this.cursorTexture = cursorTexture;
            this.origin = origin;
            size = new Vector2(inventoryTexture.Width, inventoryTexture.Height);
            box = new Rectangle(0, 0, (int)size.X, (int)size.Y);

            weaponStartOrigin = this.origin + new Vector2(132, 48);
            furthestWeaponX = (int)weaponStartOrigin.X;
            itemStartOrigin = this.origin + new Vector2(132, 64);
            furthestItemX = (int)itemStartOrigin.X;

            BItemOrigin = this.origin + new Vector2(68 + 4, 48 + 8);

            cursorBox = new Rectangle((int)weaponStartOrigin.X - 4, (int)weaponStartOrigin.Y, cursorTexture.Width, cursorTexture.Height);
            currentBItem.Item1 = ItemType.None;
	        currentBItem.Item2 = null;
        }

        public Vector2 Center
        {
            get => center;
            set => center = value;
        }

        public Texture2D Texture
        {
            get => inventoryTexture;
        }

        public ref Rectangle Box
        {
            get => ref box;
        }

        // Use similarly to ChangeDirection from IEntity
        public void MoveCursor(Vector2 direction)
        {
            if (direction.X > 0)
            {
                cursorBox.X += 20;
            }
            else if (direction.Y > 0)
            {
                cursorBox.Y += 16;
            }
            else if (direction.X < 0)
            {
                cursorBox.X -= 20;
            }
            else if (direction.Y < 0)
            {
                cursorBox.Y -= 16;
            }
        }

        public void SelectCursorItem()
        {
            foreach (var item in drawList)
            {
                if (cursorBox.Contains(item.Value.Box))
                {
                    cursorBoxItem = item.Key;
                    break;
                }
            }
            InventoryManager.Instance.ItemSlot = cursorBoxItem;
        }

        public void Draw()
        {
            spriteBatch.Draw(inventoryTexture, origin, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, HUDUtilities.HealthHUDLayer);
            spriteBatch.Draw(cursorTexture, new Vector2(cursorBox.X, cursorBox.Y), null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, HUDUtilities.HealthHUDLayer);
	        foreach (var sprite in drawList)
            {
                sprite.Value.Draw();
	        }
            if (currentBItem.Item2 != null) currentBItem.Item2.Draw();
	    }

        public void Erase()
        {
        }

        public int Update(GameTime gameTime)
        {
            foreach (var item in InventoryManager.Instance.inventory)
            {
                if (item.Value > 0)
                {
                    if (!drawList.ContainsKey(item.Key))
                    {
                        AddToDrawList(item.Key);
                    }
                }
            }
            CheckSelectedItem();
            return 0;
        }

        private void CheckSelectedItem()
        {
            ItemType itemType = InventoryManager.Instance.ItemSlot;
            if (currentBItem.Item1 != itemType)
            {
                currentBItem.Item1 = itemType;
                currentBItem.Item2 = ItemSpriteFactory.Instance.CreateItemWithType(itemType, BItemOrigin);
            }
        }

        private void AddToDrawList(ItemType itemType)
        {
            if (itemType is ItemType.Bow)
            {
                ISprite spriteBow = ItemSpriteFactory.Instance.CreateItemWithType(itemType, new Vector2(weaponStartOrigin.X + 44 + 8 + 4, weaponStartOrigin.Y + 8));
                ISprite spriteArrow = ItemSpriteFactory.Instance.CreateItemWithType(ItemType.Arrow, new Vector2(weaponStartOrigin.X + 44 + 4, weaponStartOrigin.Y + 8));
                drawList.Add(ItemType.Arrow, spriteArrow);
                drawList.Add(ItemType.Bow, spriteBow);
            }
            else if (itemType is ItemType.Boomerang || itemType is ItemType.Bomb) // There's a candle but we can ignore that because its not in the dungeons
            {
                if (furthestWeaponX > (weaponStartOrigin.X + 79)) return; // Weapons are full
                if ((furthestWeaponX + 12) == (weaponStartOrigin.X + 44))
                {
                    furthestWeaponX += 16 + 12;
                }
                ISprite sprite = ItemSpriteFactory.Instance.CreateItemWithType(itemType, new Vector2(furthestWeaponX + 4, weaponStartOrigin.Y + 8));
                furthestWeaponX += 20;
                drawList.Add(itemType, sprite);
            }
        }

    }
}
