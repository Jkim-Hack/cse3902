using cse3902.Constants;
using cse3902.SpriteFactory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Utilities;
using cse3902.Sprites;

namespace cse3902.HUD.HUDItems
{
    public class CurrentItemsHUDItem : IHUDItem
    {
        private Vector2 position;
        private Vector2 center;
        private Texture2D uiSpriteTexture;
        private Texture2D numbersTexture;

        private InventoryItemSprite slotA;
        private InventoryItemSprite slotB;

        private Vector2 rupeeCountPosition;
        private Vector2 keyCountPosition;
        private Vector2 bombCountPosition;

        private Rectangle box;
        private Vector2 size;
        private SpriteBatch spriteBatch;


        public CurrentItemsHUDItem(Game1 game, Texture2D UITexture, Texture2D numbersTexture, Vector2 position)
        {
            this.position = position;
            center = new Vector2(position.X / 2f, position.Y / 2f);

            uiSpriteTexture = UITexture;
            this.numbersTexture = numbersTexture;

            size = new Vector2(uiSpriteTexture.Bounds.Width, uiSpriteTexture.Bounds.Height);
            box = new Rectangle((int)size.X, (int)size.Y, (int)size.X, (int)size.Y);
            spriteBatch = game.SpriteBatch;

            rupeeCountPosition = new Vector2(HUDPositionConstants.CountsXPos, position.Y);
            keyCountPosition = new Vector2(HUDPositionConstants.CountsXPos, position.Y+numbersTexture.Height*2);
            bombCountPosition = new Vector2(HUDPositionConstants.CountsXPos, position.Y+numbersTexture.Height*3);

            slotA = HUDSpriteFactory.Instance.CreateInventoryItemSprite(game.SpriteBatch, HUDPositionConstants.SlotA, InventoryManager.ItemType.None);
            slotB = HUDSpriteFactory.Instance.CreateInventoryItemSprite(game.SpriteBatch, HUDPositionConstants.SlotB, InventoryManager.ItemType.None);
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
            spriteBatch.Draw(uiSpriteTexture, position, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, HUDUtilities.InventoryHUDLayer);

            DrawCount(InventoryManager.Instance.inventory[InventoryManager.ItemType.Rupee], rupeeCountPosition);
            DrawCount(InventoryManager.Instance.inventory[InventoryManager.ItemType.Key], keyCountPosition);
            DrawCount(InventoryManager.Instance.inventory[InventoryManager.ItemType.Bomb], bombCountPosition);

            if (!GameStateManager.Instance.InMenu(true))
            {
                DrawItems();
            }

        }

        public int Update(GameTime gameTime)
        {
            slotA.changeItem(InventoryManager.Instance.SwordSlot);
            slotB.changeItem(InventoryManager.Instance.ItemSlot);
            slotA.Update(gameTime);
            slotB.Update(gameTime);
            return 0;
        }

        private void DrawCount(int count, Vector2 location)
        {
            int rightDigit = count % HUDPositionConstants.CountDiv;
            int leftDigit = (int)(count / HUDPositionConstants.CountDiv);

            if (leftDigit == 0 && count < HUDPositionConstants.CountDiv)
            {
                //todo: don't draw the right digit in this case
                leftDigit = rightDigit;
                DrawDigit(-1, location);
                DrawDigit(leftDigit, new Vector2(location.X + (numbersTexture.Width / HUDPositionConstants.DigitDrawDiv), location.Y));
            } else
            {
                DrawDigit(-1, location);

                DrawDigit(leftDigit, new Vector2(location.X + (numbersTexture.Width / HUDPositionConstants.DigitDrawDiv), location.Y));
                Vector2 rightLocation = new Vector2(location.X + 2 * (numbersTexture.Width / HUDPositionConstants.DigitDrawDiv), location.Y);
                DrawDigit(rightDigit, rightLocation);
            }
        }

        private void DrawDigit(int digit, Vector2 location)
        {
            int digitWidth = HUDPositionConstants.digitWidth;
            Vector2 origin = new Vector2((numbersTexture.Width/11) / 2f, numbersTexture.Height / 2f);
            Rectangle Destination = new Rectangle((int)location.X, (int)location.Y, (int)(numbersTexture.Width/11), (int)(numbersTexture.Height));
            Rectangle source = new Rectangle((digitWidth * (digit+1)) + ((digit+1)*1), 0, digitWidth, numbersTexture.Height);
            if (digit == -1)
            {
                source = new Rectangle(0, 0, digitWidth, numbersTexture.Height);
            }
            spriteBatch.Draw(numbersTexture, Destination, source, Color.White, 0, new Vector2(0,0), SpriteEffects.None, HUDUtilities.InventoryItemLayer);
        }

        private void DrawItems()
        {
            //get proper constant for where b items should go in relation to ui
            slotB.Draw();
            slotA.Draw();
        }
    }
}
