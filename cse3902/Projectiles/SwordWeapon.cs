using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cse3902.Items
{
    public class SwordWeapon : ISprite, IItem, IProjectile
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 center;

        private int rows;
        private int columns;
        private int currentFrame;
        private int totalFrames;
        private Rectangle[] frames;
        private int frameWidth;
        private int frameHeight;

        private float remainingDelay;
        private readonly float[] delaySequence = { 0.1f, 0.15f, 0.05f, 0.05f };

        private float angle;
        private bool animationComplete;
        private int swordType;

        private const float sizeIncrease = 2f;

        public SwordWeapon(SpriteBatch batch, Texture2D texture, Vector2 startingPos, Vector2 dir, int swordType)
        {
            spriteBatch = batch;
            spriteTexture = texture;
            center = startingPos;

            remainingDelay = delaySequence[0];
            this.rows = 4;
            this.columns = 4;
            currentFrame = swordType * rows;
            this.swordType = swordType;
            totalFrames = rows * columns;
            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            frames = new Rectangle[totalFrames];
            distributeFrames();
            animationComplete = false;

            if (dir.X > 0)
            {
                angle = (float)(Math.PI * 1.0 / 2.0);
            }
            else if (dir.X < 0)
            {
                angle = (float)(Math.PI * 3.0 / 2.0);
            }
            else if (dir.Y > 0)
            {
                angle = (float)Math.PI;
            }
            else
            {
                angle = 0;
            }
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
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White, angle, origin, SpriteEffects.None, 0.8f);
        }

        public int Update(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingDelay -= timer;

            if (remainingDelay <= 0)
            {
                currentFrame++;
                if (currentFrame % columns == 0)
                {
                    currentFrame -= rows;
                    animationComplete = true;
                }
                remainingDelay = delaySequence[currentFrame % columns];
            }
            return 0;
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public Rectangle Box
        {
            get
            {
                int width = (int)(sizeIncrease * frameWidth);
                int height = (int)(sizeIncrease * frameHeight);
                double cos = Math.Abs(Math.Cos(angle));
                double sin = Math.Abs(Math.Sin(angle));
                Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(width * cos + height * sin), (int)(height * cos + width * sin));
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                return Destination;
            }
        }

        public Vector2 Center
        {
            get
            {
                return center;
            }
            set
            {
                center = value;
            }
        }

        public Texture2D Texture
        {
            get => spriteTexture;
        }

        public bool AnimationComplete
        {
            get => animationComplete;
            set => animationComplete = value;
        }
    }
}