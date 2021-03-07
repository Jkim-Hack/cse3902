using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cse3902.Projectiles
{
    public class BoomerangItem : IItem, IProjectile
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 startingPosition;
        private float angle;
        private const float sizeIncrease = 2f;

        private int currentX;
        private int currentY;

        private int frameWidth;
        private int frameHeight;

        private Rectangle destination;

        private int turns;

        private enum Direction
        {
            Up, Down, Left, Right
        }

        private Direction direction;

        private bool animationComplete;

        private ICollidable collidable;

        public BoomerangItem(SpriteBatch batch, Texture2D texture, Vector2 startingPos, Vector2 dir)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            startingPosition = startingPos;

            frameWidth = spriteTexture.Width;
            frameHeight = spriteTexture.Height;

            if (dir.X > 0)
            {
                direction = Direction.Right;
                angle = (float)(Math.PI * 1.0 / 2.0);
            }
            else if (dir.X < 0)
            {
                direction = Direction.Left;
                angle = (float)(Math.PI * 3.0 / 2.0);
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

            turns = 0;

            currentX = (int)startingPos.X;
            currentY = (int)startingPos.Y;

            animationComplete = false;

            this.collidable = new ProjectileCollidable(this);
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle(currentX, currentY, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, null, Color.White, angle, origin, SpriteEffects.None, 0.8f);
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public int Update(GameTime gameTime)
        {
            int offset = 0;
            if (turns % 2 == 0)
            {
                offset = 50;
            }
            if (direction == Direction.Right)
            {
                currentX += 2;
                if (currentX > startingPosition.X + offset)
                {
                    direction = Direction.Left;
                    turns++;
                }
            }
            else if (direction == Direction.Left)
            {
                currentX -= 2;
                if (currentX < startingPosition.X - offset)
                {
                    direction = Direction.Right;
                    turns++;
                }
            }
            else
            if (direction == Direction.Down)
            {
                currentY += 2;
                if (currentY > startingPosition.Y + offset)
                {
                    direction = Direction.Up;
                    turns++;
                }
            }
            else
            {
                currentY -= 2;
                if (currentY < startingPosition.Y - offset)
                {
                    direction = Direction.Down;
                    turns++;
                }
            }

            if (turns == 2)
            {
                animationComplete = true;
            }
            return 0;
        }

        public ref Rectangle Box
        {
            get
            {
                int width = (int)(sizeIncrease * frameWidth);
                int height = (int)(sizeIncrease * frameHeight);
                double cos = Math.Abs(Math.Cos(angle));
                double sin = Math.Abs(Math.Sin(angle));
                Rectangle Destination = new Rectangle(currentX, currentY, (int)(width * cos + height * sin), (int)(height * cos + width * sin));
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
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

        public bool AnimationComplete
        {
            get => animationComplete;

            set => animationComplete = value;
        }

        public int Damage
        {
            get => 3;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }
    }
}