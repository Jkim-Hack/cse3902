using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.HUD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Sprites;

namespace cse3902.Items
{
    public class BluePotionItem : ISprite, IItem
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private int frameWidth;
        private int frameHeight;

        private int currentX;
        private int currentY;

        private Rectangle destination;

        private const float sizeIncrease = 1f;

        private ICollidable collidable;
        private bool isKept;
        private bool isResetKept;

        public BluePotionItem(SpriteBatch batch, Texture2D texture, Vector2 startingPos, bool kept, bool resetKept)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            frameWidth = texture.Width;
            frameHeight = texture.Height;

            currentX = (int)startingPos.X;
            currentY = (int)startingPos.Y;

            this.collidable = new ItemCollidable(this);
            isKept = kept;
            isResetKept = resetKept;
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle(currentX, currentY, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, null, Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.ItemLayer);
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public int Update(GameTime gameTime)
        {
            return 0;
        }

        public ref Rectangle Box
        {
            get
            {
                int width = (int)(sizeIncrease * frameWidth);
                int height = (int)(sizeIncrease * frameHeight);
                Rectangle Destination = new Rectangle(currentX, currentY, width, height);
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
            }
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(currentX, currentY);
            }
            set
            {
                currentX = (int)value.X;
                currentY = (int)value.Y;
            }
        }

        public Texture2D Texture
        {
            get => spriteTexture;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }

        public InventoryManager.ItemType ItemType
        {
            get => InventoryManager.ItemType.BluePotion;
        }

        public bool IsKept
        {
            get => isKept;
        }

        public bool IsResetKept
        {
            get => isResetKept;
        }
    }
}