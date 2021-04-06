using System;
using System.Collections.Generic;
using cse3902.Constants;
using cse3902.Interfaces;
using cse3902.SpriteFactory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Utilities;
using cse3902.Sprites;

namespace cse3902.HUD.HUDItems
{
    public class CurrentItemsHUDItem : IHUDItem
    {
        private Vector2 origin;
        private Vector2 center;
        private Texture2D uiSpriteTexture;
        private Texture2D numbersTexture;

        private ISprite slotA;
        private ISprite slotB;

        private ISprite rupeeCount;
        private ISprite keyCount;
        private ISprite bombCount;

        private Rectangle box;
        private Vector2 size;
        private SpriteBatch spriteBatch;
        private IPlayer player;


        public CurrentItemsHUDItem(Game1 game, Texture2D UITexture, Texture2D numbersTexture, Vector2 origin)
        {
            this.origin = origin;
            center = new Vector2(origin.X / 2f, origin.Y / 2f);

            uiSpriteTexture = UITexture;
            this.numbersTexture = numbersTexture;

            //always has sword in slot a
            slotA = ItemSpriteFactory.Instance.CreateSwordItem(spriteBatch, origin);

            size = new Vector2(uiSpriteTexture.Bounds.Width, uiSpriteTexture.Bounds.Height);
            box = new Rectangle((int)size.X, (int)size.Y, (int)size.X, (int)size.Y);
            spriteBatch = game.SpriteBatch;

            player = game.Player;
        }

        public Vector2 Center
        {
            get => center;
            set => center = value;
        }

        public Texture2D Texture
        {
            get => uiSpriteTexture;
        }

        public ref Rectangle Box
        {
            get => ref box;
        }

        public void Draw()
        {
            //todo: change layering on this
            spriteBatch.Draw(uiSpriteTexture, origin, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, HUDUtilities.HealthHUDLayer);

            //todo: change location of these counts using hud constants
            DrawCount(InventoryManager.Instance.inventory[InventoryManager.ItemType.Rupee], origin);
            DrawCount(InventoryManager.Instance.inventory[InventoryManager.ItemType.Key], origin);
            DrawCount(InventoryManager.Instance.inventory[InventoryManager.ItemType.Bomb], origin);
            DrawItems();

        }

        public void Erase()
        {
            uiSpriteTexture.Dispose();
        }

        public int Update(GameTime gameTime)
        {
            
            return 0;
        }

        private void DrawCount(int count, Vector2 location)
        {
            int rightDigit = InventoryManager.Instance.inventory[InventoryManager.ItemType.Rupee] % 10;
            int leftDigit = (int)(InventoryManager.Instance.inventory[InventoryManager.ItemType.Rupee] / 10);

            if (leftDigit == 0 && count < 10)
            {
                //todo: don't draw the right digit in this case
                leftDigit = rightDigit;
            }

            DrawDigit(leftDigit, location);
            Vector2 rightLocation = new Vector2(location.X + (numbersTexture.Width / 11), location.Y);
            DrawDigit(rightDigit, rightLocation);

        }

        private void DrawDigit(int digit, Vector2 location)
        {
            Vector2 origin = new Vector2((numbersTexture.Width/11) / 2f, numbersTexture.Height / 2f);
            Rectangle Destination = new Rectangle((int)location.X, (int)location.Y, (int)(numbersTexture.Width/11), (int)(numbersTexture.Height));
            switch (digit)
            {
                case 0:
                    Rectangle source = new Rectangle(0, 0, numbersTexture.Width / 11, numbersTexture.Height);
                    spriteBatch.Draw(numbersTexture, Destination, source, Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.ProjectileLayer);
                    break;
                default:
                    break;
            }
        }

        private void DrawItems()
        {
            //get proper constant for where b items should go in relation to ui
            slotB = ItemSpriteFactory.Instance.CreateItemWithType(InventoryManager.Instance.ItemSlot, origin);
            slotB.Draw();
            slotA.Draw();
        }
    }
}
