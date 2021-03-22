using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using cse3902.Sprites;

namespace cse3902.Projectiles
{
    public class ArrowItem : IItem, IProjectile
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private int currentX;
        private int currentY;
        private int frameWidth;
        private int frameHeight;

        private float angle = 0;

        private bool animationComplete;
        private Rectangle destination;
        private const float sizeIncrease = 1f;

        private Vector2 direction;

        private ICollidable collidable;

        public ArrowItem(SpriteBatch batch, Texture2D texture, Vector2 startingPos, Vector2 dir)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            direction = dir;

            if (dir.X > 0)
            {
                direction = new Vector2(1,0);
                angle = (float)(Math.PI * 1.0 / 2.0);
            }
            else if (dir.X < 0)
            {
                direction = new Vector2(-1, 0);
                angle = (float)(Math.PI * 3.0 / 2.0);
            }
            else if (dir.Y > 0)
            {
                direction = new Vector2(0, 1);
                angle = (float)Math.PI;
            }
            else
            {
                direction = new Vector2(0, -1);
                angle = 0;
            }

            animationComplete = false;

            frameWidth = spriteTexture.Width;
            frameHeight = spriteTexture.Height;

            currentX = (int)startingPos.X;
            currentY = (int)startingPos.Y;

            this.collidable = new ProjectileCollidable(this);
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle(currentX, currentY, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, null, Color.White, angle, origin, SpriteEffects.None, SpriteUtilities.ProjectileLayer);
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public int Update(GameTime gameTime)
        {
            if (direction.X == 1)
            {
                currentX += 2;
            }
            else if (direction.X == -1)
            {
                currentX -= 2;
            }
            else if (direction.Y == 1)
            {
                currentY += 2;
            }
            else
            {
                currentY -= 2;
            }
            return 0;
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
            get => 2;
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