using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Items
{
    public class BoomerangItem : ISprite, IItem
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 startingPosition;
        private Vector2 center;

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

        private Direction direction;
        private Orientation orientation;

        private int frameWidth;
        private int frameHeight;

        public BoomerangItem(SpriteBatch batch, Texture2D texture, Vector2 startingPos)
        {
            spriteBatch = batch;
            spriteTexture = texture;
            startingPosition = startingPos;
            direction = Direction.positive;
            orientation = Orientation.horizontal;
        }

        public void Draw()
        {
            Rectangle Destination = new Rectangle(currentX, currentY, spriteTexture.Width, spriteTexture.Height);
            spriteBatch.Begin();
            spriteBatch.Draw(spriteTexture, Destination, null, Color.White);
            spriteBatch.End();
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public void Update(GameTime gameTime)
        {

            if (orientation == Orientation.horizontal)
            {
                if (direction == Direction.positive)
                {
                    currentX += 2;
                    if (currentX > 150)
                    {
                        //currentX = 400;
                        direction = Direction.negative;
                    }
                   
                }
                else
                {
                    currentX -= 2;
                    if (currentX < 100)
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
                    if (currentY > 480)
                    {
                        currentY = 0;
                    }
                }
                else
                {
                    currentY -= 2;
                    if (currentY < 0)
                    {
                        currentY = 480;
                    }
                }
            }
        }

        public Vector2 StartingPosition
        {
            get => startingPosition;

            set
            {
                startingPosition = value;
                center = value;
                currentX = (int)value.X;
                currentY = (int)value.Y;
            }
        }

        public Vector2 Center
        {
            get => center;

            set
            {
                center = value;
            }
        }

        public Texture2D Texture
        {
            get => spriteTexture;
        }
    }
}
