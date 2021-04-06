using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.HUD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Sprites;

namespace cse3902.Items
{
    public class TriforceItem : ISprite, IItem
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private (int rows, int columns) dimensions;
        private (int frameWidth, int frameHeight, int currentFrame, Rectangle[] frames) frameSize;

        private (float delay, float remainingDelay) delays;
        private const float sizeIncrease = 1f;
        private Rectangle destination;
        private (int currentX, int currentY) currentPos;

        private ICollidable collidable;
        private InventoryManager.ItemType itemType;
        private bool isKept;
        private bool isResetKept;

        private bool gameWon;

        public TriforceItem(SpriteBatch batch, Texture2D texture, Vector2 startingPos, bool kept, bool resetKept)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            delays.delay = 0.2f;

            delays.remainingDelay = delays.delay;
            this.dimensions.rows = 2;
            this.dimensions.columns = 1;
            frameSize.currentFrame = 0;
            frameSize.frameWidth = spriteTexture.Width / dimensions.columns;
            frameSize.frameHeight = spriteTexture.Height / dimensions.rows;
            frameSize.frames = SpriteUtilities.distributeFrames(dimensions.columns, dimensions.rows, frameSize.frameWidth, frameSize.frameHeight);

            currentPos.currentX = (int)startingPos.X;
            currentPos.currentY = (int)startingPos.Y;

            this.collidable = new ItemCollidable(this);
            itemType = InventoryManager.ItemType.Triforce;
            isKept = kept;
            isResetKept = resetKept;

            gameWon = false;
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameSize.frameWidth / 2f, frameSize.frameHeight / 2f);
            Rectangle Destination = new Rectangle(currentPos.currentX, currentPos.currentY, (int)(sizeIncrease * frameSize.frameWidth), (int)(sizeIncrease * frameSize.frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, frameSize.frames[frameSize.currentFrame], Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.ItemLayer);
        }

        public int Update(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            delays.remainingDelay -= timer;

            if (delays.remainingDelay <= 0)
            {
                frameSize.currentFrame++;
                if (frameSize.currentFrame == (dimensions.rows * dimensions.columns))
                {
                    frameSize.currentFrame = 0;
                }
                delays.remainingDelay = delays.delay;
            }
            return 0;
        }

        public ref Rectangle Box
        {
            get
            {
                int width = (int)(sizeIncrease * frameSize.frameWidth);
                int height = (int)(sizeIncrease * frameSize.frameHeight);
                Rectangle Destination = new Rectangle(currentPos.currentX, currentPos.currentY, width, height);
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
            }
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(currentPos.currentX, currentPos.currentY);
            }
            set
            {
                currentPos.currentX = (int)value.X;
                currentPos.currentY = (int)value.Y;
            }
        }


        public Texture2D Texture
        {
            get => spriteTexture;
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }

        public InventoryManager.ItemType ItemType
        {
            get => itemType;
        }

        public bool IsKept
        {
            get => isKept;
        }

        public bool IsResetKept
        {
            get => isResetKept;
        }

        public bool GameWon
        {
            get => gameWon;
            set => gameWon = value;
        }
    }
}