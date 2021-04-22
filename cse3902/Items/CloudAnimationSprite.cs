using cse3902.Constants;
using cse3902.Interfaces;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Items
{
    public class CloudAnimationSprite : ISprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private int currentFrame;
        private int totalFrames;
        private Rectangle[] frames;
        private int frameWidth;
        private int frameHeight;

        private const float delay = ItemConstants.CloudDelay;
        private float remainingDelay;

        private (int currentX, int currentY) location;

        private Rectangle destination;

        private const float sizeIncrease = 1f;

        public CloudAnimationSprite(SpriteBatch batch, Texture2D texture, Vector2 startingPos)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            remainingDelay = delay;
            int rows = ItemConstants.CloudRows;
            int columns = ItemConstants.CloudCols;
            currentFrame = 0;
            totalFrames = rows * columns;
            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            frames = SpriteUtilities.distributeFrames(columns, rows, frameWidth, frameHeight);

            location.currentX = (int)startingPos.X;
            location.currentY = (int)startingPos.Y;
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle(location.currentX, location.currentY, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.ItemLayer);
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
                    return -1;
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
                Rectangle Destination = new Rectangle(location.currentX, location.currentY, width, height);
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
            }
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(location.currentX, location.currentY);
            }
            set
            {
                location.currentX = (int)value.X;
                location.currentY = (int)value.Y;
            }
        }

        public Texture2D Texture
        {
            get => spriteTexture;
        }
    }
}