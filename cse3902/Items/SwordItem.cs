using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Items
{
    public class SwordItem : ISprite, IItem
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

        private enum Orientation
        {
            horizontal,
            vertical
        }

        private enum Direction
        {
            positive,
            negative
        }

        private float angle;
        private Direction direction;
        private Orientation orientation;
        bool active;

        public SwordItem(SpriteBatch batch, Texture2D texture, Vector2 startingPos, Vector2 dir)
        {
            spriteBatch = batch;
            spriteTexture = texture;
            startingPosition = startingPos;

            remainingDelay = delay;
            this.rows = 2;
            this.columns = 2;
            currentFrame = 0;
            totalFrames = rows * columns;
            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            frames = new Rectangle[totalFrames];
            distributeFrames();
            active = true;

            if ((int)dir.X == 1 && (int)dir.Y == 0)
            {
                direction = Direction.positive;
                orientation = Orientation.horizontal;
                angle = 1.57f;
            }
            else if ((int)dir.X == -1 && (int)dir.Y == 0)
            {
                direction = Direction.negative;
                orientation = Orientation.horizontal;
                angle = 4.71f;
            }
            else if ((int)dir.X == 0 && (int)dir.Y == -1)
            {
                direction = Direction.positive;
                orientation = Orientation.vertical;
                angle = 3.14f;
            }
            else
            {
                direction = Direction.negative;
                orientation = Orientation.vertical;
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
            if (active)
            {
                Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
                //Rectangle Destination = new Rectangle(currentX, currentY, 2 * frameWidth, 2 * frameHeight);
                spriteBatch.Begin();
                spriteBatch.Draw(spriteTexture, new Vector2(currentX, currentY), frames[currentFrame], Color.White, angle, origin, 2.0f, SpriteEffects.None, 1);
                //spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White);
                spriteBatch.End();
            }
        }

        public void Update(GameTime gameTime)
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

            if (orientation == Orientation.horizontal)
            {
                if (direction == Direction.positive)
                {
                    currentX += 2;
                    if (currentX > 800)
                    {
                        active = false;
                    }
                    if (currentX > startingPosition.X + 50)
                    {
                        //currentX = 400;
                        direction = Direction.negative;
                    }
                }
                else
                {
                    currentX -= 2;
                    if (currentX < 0)
                    {
                        active = false;
                    }
                    if (currentX < startingPosition.X)
                    {
                        //currentY = 400;
                        direction = Direction.positive;
                    }
                }
            }
            else
            {
                if (direction == Direction.positive)
                {
                    currentY += 2;
                    if (currentY > 600)
                    {
                        active = false;
                    }
                    if (currentY > startingPosition.Y + 50)
                    {
                        direction = Direction.negative;
                    }
                }
                else
                {
                    currentY -= 2;
                    if (currentY < 0)
                    {
                        active = false;
                    }
                    if (currentY < startingPosition.Y)
                    {
                        direction = Direction.positive;
                    }
                }
            }
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }
        
        public bool Active
        {
            get => active;
        }
    }
}