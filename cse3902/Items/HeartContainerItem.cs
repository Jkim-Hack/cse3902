using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;

namespace cse3902.Items
{
    public class HeartContainerItem : ISprite, IItem
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private int frameWidth;
        private int frameHeight;

        private int currentX;
        private int currentY;

        private const float sizeIncrease = 2f;

        public HeartContainerItem(SpriteBatch batch, Texture2D texture, Vector2 startingPos)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            frameWidth = spriteTexture.Width;
            frameHeight = spriteTexture.Height;

            currentX = (int)startingPos.X;
            currentY = (int)startingPos.Y;
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle(currentX, currentY, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, null, Color.White, 0, origin, SpriteEffects.None, 0.8f);
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public int Update(GameTime gameTime)
        {
            return 0;
        }

        public Rectangle Box
        {
            get
            {
                int width = (int)(sizeIncrease * frameWidth);
                int height = (int)(sizeIncrease * frameHeight);
                Rectangle Destination = new Rectangle(currentX, currentY, width, height);
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                return Destination;
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
    }
}
