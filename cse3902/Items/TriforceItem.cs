using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Items
{
    public class TriforceItem : ISprite, IItem
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private int rows;
        private int columns;
        private int currentFrame;
        private int totalFrames;
        private Rectangle[] frames;
        private int frameWidth;
        private int frameHeight;

        private const float delay = 0.2f;
        private float remainingDelay;

        private int currentX;
        private int currentY;

        private Rectangle destination;

        private const float sizeIncrease = 1f;

        private ICollidable collidable;

        public TriforceItem(SpriteBatch batch, Texture2D texture, Vector2 startingPos)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            remainingDelay = delay;
            this.rows = 2;
            this.columns = 1;
            currentFrame = 0;
            totalFrames = rows * columns;
            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            frames = new Rectangle[totalFrames];
            distributeFrames();

            currentX = (int)startingPos.X;
            currentY = (int)startingPos.Y;

            this.collidable = new ItemCollidable(this);
        }

        private void distributeFrames()
        {
            for (int i = 0; i < totalFrames; i++)
            {
                int Row = (int)((float)i / (float)columns);
                int Column = i % columns;
                frames[i] = new Rectangle(frameWidth * Column, frameHeight * Row, frameWidth, frameHeight);
            }
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle(currentX, currentY, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White, 0, origin, SpriteEffects.None, 0.8f);
        }

        public int Update(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingDelay -= timer;

            if (remainingDelay <= 0)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }
                remainingDelay = delay;
            }
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

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }
    }
}