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
        private float angle;

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

        public BoomerangItem(SpriteBatch batch, Texture2D texture, Vector2 startingPos, Vector2 dir)
        {
            spriteBatch = batch;
            spriteTexture = texture;
            startingPosition = startingPos;

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


            currentX = (int) startingPos.X;
            currentY = (int) startingPos.Y;
    }

        public void Draw()
        {
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            spriteBatch.Begin();
            spriteBatch.Draw(spriteTexture, new Vector2(currentX, currentY), null, Color.White, angle, origin, 2.0f, SpriteEffects.None, 1);
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
                    if (currentX > startingPosition.X + 50)
                    {
                        //currentX = 400;
                        direction = Direction.negative;
                    }
                   
                }
                else
                {
                    currentX -= 2;
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
                    if (currentY > startingPosition.Y + 50)
                    {
                        direction = Direction.negative;
                    }
                }
                else
                {
                    currentY -= 2;
                    if (currentY < startingPosition.Y)
                    {
                        direction = Direction.positive;
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
