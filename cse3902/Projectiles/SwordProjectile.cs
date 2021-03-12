using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cse3902.Projectiles
{
    public class SwordProjectile : IItem, IProjectile
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

        private Rectangle destination;

        private const float sizeIncrease = 1f;

        private Vector2 direction;

        private float angle;
        private bool animationComplete;

        private ICollidable collidable;

        public SwordProjectile(SpriteBatch batch, Texture2D texture, Vector2 startingPos, Vector2 dir)
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
            direction = dir;
            
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

            currentX = (int)startingPos.X;
            currentY = (int)startingPos.Y;

            this.collidable = new ProjectileCollidable(this);
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
            Rectangle Destination = new Rectangle(currentX, currentY, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
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

            if (direction.X == 1)
            {
                currentX += 2;
                if (currentX > 800)
                {
                    currentX = 0;
                    animationComplete = true;
                }
            }
            else if (direction.X == -1)
            {
                currentX -= 2;
                if (currentX < 0)
                {
                    currentX = 800;
                    animationComplete = true;
                }
            }
            else if (direction.Y == 1)
            {
                currentY += 2;
                if (currentY > 480)
                {
                    currentY = 0;
                    animationComplete = true;
                }
            }
            else //sword is traveling up
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

        public void Erase()
        {
            spriteTexture.Dispose();
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

        public Vector2 Direction
        {
            get => this.direction;
            set => this.direction = value;
            
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }
    }
}