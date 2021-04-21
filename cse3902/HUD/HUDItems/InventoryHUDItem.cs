using System.Collections.Generic;
using cse3902.Constants;
using cse3902.Interfaces;
using cse3902.SpriteFactory;
using cse3902.Sounds;
using cse3902.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.HUD.InventoryManager;
using cse3902.Sprites;

namespace cse3902.HUD.HUDItems
{
    public class InventoryHUDItem : IHUDItem
    {
        private Vector2 origin;
        private Texture2D inventoryTexture;
        private Texture2D cursorTexture;
        private Rectangle box;
        private SpriteBatch spriteBatch;


        private (ItemType, ISprite) currentBItem;
        private Vector2 BItemOrigin;

        private List<(Vector2 pos, ItemType type, InventoryItemSprite sprite)> inventoryItems;

        private Rectangle cursorBox;
        private ItemType cursorBoxItem;
        private int cursorPos;
        private int fromRight;

        public InventoryHUDItem(Game1 game, Texture2D inventoryTexture, Texture2D cursorTexture, Vector2 origin)
        {
            spriteBatch = game.SpriteBatch;
            this.inventoryTexture = inventoryTexture;
            this.cursorTexture = cursorTexture;
            this.origin = origin;
            Vector2 size = new Vector2(inventoryTexture.Width, inventoryTexture.Height);
            box = new Rectangle((int)origin.X, (int) origin.Y, (int)size.X, (int)size.Y);

            inventoryItems = new List<(Vector2, ItemType, InventoryItemSprite)>();
            initiateInventoryItems();
            Vector2 weaponStartOrigin = this.origin + HUDPositionConstants.WeaponStartHUDPosition;

            BItemOrigin = this.origin + HUDPositionConstants.BItemHUDPosition;

            cursorBox = new Rectangle((int)weaponStartOrigin.X - 4, (int)weaponStartOrigin.Y, cursorTexture.Width, cursorTexture.Height);
            currentBItem.Item1 = ItemType.None;
	        currentBItem.Item2 = null;

            fromRight = 0;
            cursorPos = 0;
        }
        
        private void initiateInventoryItems()
        {
            inventoryItems.Clear();
            int i = 0;
            Vector2 origin = this.origin + HUDPositionConstants.WeaponStartHUDPosition;
            foreach (ItemType type in InventoryUtilities.EquipableItemsList)
            {
                Vector2 current = origin + new Vector2((i % HUDPositionConstants.InventoryItemsCols) * HUDPositionConstants.InventoryGapX, (i / HUDPositionConstants.InventoryItemsCols) * HUDPositionConstants.InventoryGapY);
                Vector2 loc = current;
                if (HUDPositionConstants.InventoryIndicatorPos.ContainsKey(type))
                {
                    loc = HUDPositionConstants.InventoryIndicatorPos[type];
                }
                inventoryItems.Add((current, type, HUDSpriteFactory.Instance.CreateInventoryItemSprite(spriteBatch, loc, type)));
                i++;
            }
            foreach (var pair in HUDPositionConstants.InventoryIndicatorPos)
            {
                if (!InventoryUtilities.EquipableItemsList.Contains(pair.Key))
                {
                    inventoryItems.Add((pair.Value, pair.Key, HUDSpriteFactory.Instance.CreateInventoryItemSprite(spriteBatch, pair.Value, pair.Key)));
                }
            }
        }

        public Vector2 Center
        {
            get => origin + new Vector2(box.Width/2, box.Height/2);
            set
            {
                throw new System.Exception("Cannot set the center of InventoryHUDItem");
            }
        }

        public Texture2D Texture
        {
            get => inventoryTexture;
        }

        public ref Rectangle Box
        {
            get => ref box;
        }

        // Use similarly to ChangeDirection from IEntity but ONLY left and right
        // Call this to move the cursor arround
        public void MoveCursor(Vector2 direction)
        {
            int originalRight = fromRight;
            int originalX = cursorBox.X;

            if (direction.X > 0 && fromRight < 2)
            {
                do
                {
                    cursorBox.X += HUDPositionConstants.InventoryGapX;
                    if (fromRight == 1)
                    {
                        cursorBox.X += 8;
                    }
                    SelectCursorItem();
                    fromRight++;
                } while (fromRight < 2 && InventoryManager.Instance.ItemSlot == ItemType.None);

                if (InventoryManager.Instance.ItemSlot == ItemType.None)
                {
                    cursorBox.X = originalX;
                    fromRight = originalRight;
                    SelectCursorItem();
                }
                else SoundFactory.PlaySound(SoundFactory.Instance.getRupee, 0.25f);
            }
            else if (direction.X < 0 && fromRight > 0)
            {
                do
                {
                    cursorBox.X -= HUDPositionConstants.InventoryGapX;
                    if (fromRight == 2)
                    {
                        cursorBox.X -= 8;
                    }
                    SelectCursorItem();
                    fromRight--;
                } while (fromRight > 0 && InventoryManager.Instance.ItemSlot == ItemType.None);

                if (InventoryManager.Instance.ItemSlot == ItemType.None)
                {
                    cursorBox.X = originalX;
                    fromRight = originalRight;
                    SelectCursorItem();
                } else SoundFactory.PlaySound(SoundFactory.Instance.getRupee, 0.25f);
            }

            //SoundFactory.PlaySound(SoundFactory.Instance.getRupee, 0.25f)
        }

        // Call this when pressed B
        private void SelectCursorItem()
        {
            foreach (var item in inventoryItems)
            {
                if (cursorBox.Contains(item.sprite.Box))
                {
                    cursorBoxItem = item.type;
                    if (item.type == ItemType.Arrow)
                    {
                        cursorBoxItem = ItemType.Bow;
                    }
                    InventoryManager.Instance.ItemSlot = cursorBoxItem;
                    break;
                }
            }
        }

        public void Draw()
        {
            spriteBatch.Draw(inventoryTexture, origin, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, HUDUtilities.InventoryHUDLayer);
            if (GameStateManager.Instance.InMenu(false))
            {
                spriteBatch.Draw(cursorTexture, new Vector2(cursorBox.X, cursorBox.Y), null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, HUDUtilities.InventoryItemLayer);
                foreach (var item in inventoryItems)
                {
                    item.sprite.Draw();
                }
                if (currentBItem.Item2 != null) currentBItem.Item2.Draw();
            }
	    }

        public void Erase()
        {
        }

        public int Update(GameTime gameTime)
        {
            foreach (var item in inventoryItems)
            {
                item.sprite.Update(gameTime);
            }
            CheckSelectedItem();
            return 0;
        }

        private void CheckSelectedItem()
        {
            ItemType itemType = InventoryManager.Instance.ItemSlot;
            if (itemType == ItemType.None)
            {
                currentBItem.Item1 = itemType;
                currentBItem.Item2 = null;
            }
            else if (currentBItem.Item1 != itemType)
            {
                currentBItem.Item1 = itemType;
                currentBItem.Item2 = ItemSpriteFactory.Instance.CreateItemWithType(itemType, BItemOrigin);
            }
        }

    }
}
