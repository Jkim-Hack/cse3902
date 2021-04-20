using cse3902.Constants;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Sprites
{
    public class RickRollSprite : ISprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private int currentFrame;
        private Rectangle[] frames;
        private int frameWidth;
        private int frameHeight;

        private const float delay = SpriteConstants.RickDelay;
        private float remainingDelay;

        private const float sizeIncrease = 1f;

        private Rectangle destination;

        private (int X, int Y) current;

        public RickRollSprite(SpriteBatch batch, Texture2D texture, Vector2 startingPos)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            remainingDelay = delay;
            int rows = SpriteConstants.RickRows;
            int columns = SpriteConstants.RickCols;
            currentFrame = 0;
            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            frames = SpriteUtilities.distributeFrames(rows, columns, frameWidth, frameHeight);

            current.X = (int)startingPos.X;
            current.Y = (int)startingPos.Y;
        }


        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle(current.X, current.Y, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White, 0, origin, SpriteEffects.None, 0.8f);
        }

        public int Update(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingDelay -= timer;

            if (remainingDelay <= 0)
            {
                currentFrame++;
                if (currentFrame == frames.Length)
                {
                    currentFrame = 0;
                }
                remainingDelay = delay;
            }
            return 0;
        }

        public Texture2D Texture
        {
            get => spriteTexture;
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public ref Rectangle Box
        {
            get
            {
                int width = (int)(sizeIncrease * frameWidth);
                int height = (int)(sizeIncrease * frameHeight);
                Rectangle Destination = new Rectangle(current.X, current.Y, width, height);
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
            }
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(current.X, current.Y);
            }
            set
            {
                current.X = (int)value.X;
                current.Y = (int)value.Y;
            }
        }
    }
}