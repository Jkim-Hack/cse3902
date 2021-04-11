using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.ParticleSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using cse3902.Sprites;

namespace cse3902.Projectiles
{
    public class SwordProjectile : IProjectile
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private ISprite collisionTexture;

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
        private bool collided;

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
            frames = SpriteUtilities.distributeFrames(columns, rows, frameWidth, frameHeight);
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
            collisionTexture = ProjectileHandler.Instance.CreatePoofAnim(spriteBatch, new Vector2(currentX, currentY));
            this.collidable = new ProjectileCollidable(this);
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle(currentX, currentY, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));

            if (!collided)
            {
                spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White, angle, origin, SpriteEffects.None, SpriteUtilities.ProjectileLayer);
            }
            else
            {
                if (ParticleEngine.Instance.UseParticleEffects)
                {
                    origin = new Vector2(currentX, currentY) - new Vector2(frameWidth, frameHeight) / 10 +  direction * 5;
                    ParticleEngine.Instance.CreateNewEmitter(ParticleEngine.ParticleEmitter.SwordHit, origin);
                    animationComplete = true;
                }
                else
                {
                    collisionTexture.Center = new Vector2(currentX, currentY);
                    collisionTexture.Draw();
                }
                
            }
        }

        public int Update(GameTime gameTime)
        {
            if (!collided)
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
                }
                else if (direction.X == -1)
                {
                    currentX -= 2;
                }
                else if (direction.Y == 1)
                {
                    currentY += 2;
                }
                else //sword is traveling up
                {
                    currentY -= 2;
                }
                return 0;
            }
            else
            {
                collisionTexture.Center = new Vector2(currentX, currentY);
                return collisionTexture.Update(gameTime);
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
            get => 6;
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

        public bool Collided
        {
            get => collided;
            set => collided = value;
        }
    }
}