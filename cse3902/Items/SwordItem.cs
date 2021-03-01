using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;

namespace cse3902.Items
{
    public class SwordItem : ISprite, IItem, IProjectile
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

        private const float sizeIncrease = 2f;

        private enum Direction
        {
            Up, Down, Left, Right
        }

        private float angle;
        private Direction direction;
        bool animationComplete;

        public SwordItem(SpriteBatch batch, Texture2D texture, Vector2 startingPos, Vector2 dir)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            remainingDelay = delay;
            this.rows = 2;
            this.columns = 2;
            currentFrame = 0;
            totalFrames = rows * columns;
            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            frames = new Rectangle[totalFrames];
            distributeFrames();
            animationComplete = false;

            if (dir.X > 0)
            {
                direction = Direction.Right;
                angle = (float)(Math.PI * 1.0/ 2.0);
            }
            else if (dir.X < 0)
            {
                direction = Direction.Left;
                angle = (float)(Math.PI * 3.0 / 2.0) ;
            }
            else if (dir.Y > 0)
            {
                direction = Direction.Down;
                angle = (float)Math.PI;
            }
            else
            {
                direction = Direction.Up;
                angle = 0;
            }

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

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle(currentX, currentY, (int) (sizeIncrease * frameWidth), (int) (sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White, angle, origin, SpriteEffects.None, 0.8f);
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


            if (direction == Direction.Right)
            {
                currentX += 2;
                if (currentX > 800)
                {
                    currentX = 0;
                    animationComplete = true;
                }
            }
            else if (direction == Direction.Left)
            {
                currentX -= 2;
                if (currentX < 0)
                {
                    currentX = 800;
                    animationComplete = true;
                }
            }
            else if (direction == Direction.Down)
            {
                currentY += 2;
                if (currentY > 480)
                {
                    currentY = 0;
                    animationComplete = true;
                }
            }
            else
            {
                currentY -= 2;
                if (currentY < 0)
                {
                    currentY = 480;
                    animationComplete = true;
                }
            }
            return 0;
        }

        public Rectangle Box
        {
            get
            {
                int width = (int)(sizeIncrease * frameWidth);
                int height = (int)(sizeIncrease * frameHeight);
                double cos = Math.Abs(Math.Cos(angle));
                double sin = Math.Abs(Math.Sin(angle));
                Rectangle Destination = new Rectangle(currentX, currentY, (int)(width * cos + height * sin), (int)(height * cos + width * sin));
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
            set {
                currentX = (int) value.X;
                currentY = (int) value.Y;
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
        
        public bool AnimationComplete
        {
            get => animationComplete;
            set => animationComplete = value;
        }
    }
}