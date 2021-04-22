using System;
using cse3902.Constants;
using cse3902.Interfaces;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Projectiles
{
    public class PoofSprite : ISprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private int currentFrame;
        private int totalFrames;
        private Rectangle[] frames;
        private int frameWidth;
        private int frameHeight;

        private const float delay = ItemConstants.PoofDelay;
        private float remainingDelay;
        private (int X, int Y) current;
        private float angle = 0;

        private Rectangle destination;

        public PoofSprite(SpriteBatch batch, Texture2D texture, Vector2 startingPos)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            remainingDelay = delay;
            int rows = ItemConstants.PoofRows;
            int columns = ItemConstants.PoofCols;
            currentFrame = 0;
            totalFrames = rows * columns;
            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            frames = SpriteUtilities.distributeFrames(columns, rows, frameWidth, frameHeight);

            frameWidth = spriteTexture.Width;
            frameHeight = spriteTexture.Height;

            current.X = (int)startingPos.X;
            current.Y = (int)startingPos.Y;
        }


        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle(current.X + ItemConstants.PoofOffsetX, current.Y + ItemConstants.PoofOffsetY, frameWidth, frameHeight);
            spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.EffectsLayer);            
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

        public ref Rectangle Box
        {
            get
            {
                int width = frameWidth;
                int height = frameHeight;
                double cos = Math.Abs(Math.Cos(angle));
                double sin = Math.Abs(Math.Sin(angle));
                Rectangle Destination = new Rectangle(current.X, current.Y, (int)(width * cos + height * sin), (int)(height * cos + width * sin));
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
            }
        }

        public Texture2D Texture
        {
            get => spriteTexture;
        }

    }
}