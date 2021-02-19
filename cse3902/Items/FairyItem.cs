using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;

namespace cse3902.Items
{
    public class FairyItem : ISprite, IItem
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 center;
        private Vector2 startingPosition;

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

        public FairyItem(SpriteBatch batch, Texture2D texture, Vector2 startingPos)
        {
            spriteBatch = batch;
            spriteTexture = texture;
            startingPosition = startingPos;

            remainingDelay = delay;
            this.rows = 1;
            this.columns = 2;
            currentFrame = 0;
            totalFrames = rows * columns;
            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            frames = new Rectangle[totalFrames];
            distributeFrames();

            currentX = (int)startingPos.X;
            currentY = (int)startingPos.Y;
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

        public Vector2 StartingPosition
        {
            get => startingPosition;
            set
            {
                startingPosition = value;
                Center = value;
            }
        }

        public Vector2 Center
        {
            get => center;
            set => center = value;
        }

        public Texture2D Texture
        {
            get => spriteTexture;
        }

        public void Draw()
        {
            Rectangle Destination = new Rectangle(currentX, currentY, 2*frameWidth, 2*frameHeight);
            spriteBatch.Begin();
            spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime, onAnimCompleteCallback animationCompleteCallback)
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
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }
    }
}
