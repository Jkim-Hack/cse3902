using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Interfaces;
using cse3902.Constants;

namespace cse3902.Sprites.DoorSprites
{
    public class PortalSprite : IDoorSprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 center;

        private (int rows, int columns) dimensions;
        private (int frameWidth, int frameHeight, int currentFrame, Rectangle[] frames) frameSize;

        private (float delay, float remainingDelay) delays;
        private const float sizeIncrease = 1f;
        private Rectangle destination;
        private (int currentX, int currentY) currentPos;

        public PortalSprite(SpriteBatch batch, Texture2D texture, Vector2 startingPos)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            delays.delay = SpriteConstants.PortalDelay;

            delays.remainingDelay = delays.delay;
            this.dimensions.rows = SpriteConstants.PortalRows;
            this.dimensions.columns = SpriteConstants.PortalCols;
            frameSize.currentFrame = 0;
            frameSize.frameWidth = spriteTexture.Width / dimensions.columns;
            frameSize.frameHeight = spriteTexture.Height / dimensions.rows;
            frameSize.frames = SpriteUtilities.distributeFrames(dimensions.columns, dimensions.rows, frameSize.frameWidth, frameSize.frameHeight);

            currentPos.currentX = (int)startingPos.X;
            currentPos.currentY = (int)startingPos.Y;
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameSize.frameWidth / 2f, frameSize.frameHeight / 2f);
            Rectangle Destination = new Rectangle(currentPos.currentX, currentPos.currentY, (int)(sizeIncrease * frameSize.frameWidth), (int)(sizeIncrease * frameSize.frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, frameSize.frames[frameSize.currentFrame], Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.BlockLayer);
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

        public void Erase()
        {
            spriteTexture.Dispose();
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
    }
}