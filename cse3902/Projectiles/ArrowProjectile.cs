using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.SpriteFactory;
using cse3902.Sounds;
using cse3902.ParticleSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using cse3902.Sprites;

namespace cse3902.Projectiles
{
    public class ArrowProjectile : IProjectile
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Texture2D collisionTexture;

        private int currentX;
        private int currentY;
        private int frameWidth;
        private int frameHeight;
        private int collTime;

        private float angle = 0;

        private bool collided;
        private bool animationComplete;
        private Rectangle destination;
        private const float sizeIncrease = 1f;

        private Vector2 direction;
        private ICollidable collidable;

        public ArrowProjectile(SpriteBatch batch, Texture2D texture, Vector2 startingPos, Vector2 dir)
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
            collided = false;
            collTime = 5;

            frameWidth = spriteTexture.Width;
            frameHeight = spriteTexture.Height;

            currentX = (int)startingPos.X;
            currentY = (int)startingPos.Y;

            collisionTexture = ProjectileHandler.Instance.CreateStarAnimTexture();
            this.collidable = new ProjectileCollidable(this);

            SoundFactory.PlaySound(SoundFactory.Instance.arrowBoomerang);
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);

            if (!collided)
            {
                Rectangle Destination = new Rectangle(currentX, currentY, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
                spriteBatch.Draw(spriteTexture, Destination, null, Color.White, angle, origin, SpriteEffects.None, SpriteUtilities.ProjectileLayer);
            }
            else
            {
                if (ParticleEngine.Instance.UseParticleEffects)
                {
                    origin = new Vector2(currentX, currentY) - new Vector2(frameWidth, frameHeight) / 5 +  direction * 5;
                    ParticleEngine.Instance.CreateNewEmitter(ParticleEngine.ParticleEmitter.ArrowHit, origin);
                    animationComplete = true;
                }
                else
                {
                    DrawCollisionTexture(origin);
                }
            }
        }

        private void DrawCollisionTexture(Vector2 origin)
        {
            if (collTime >= 0)
            {
                Rectangle Destination = new Rectangle(currentX, currentY, (int)(2 * collisionTexture.Width), (int)(2 * collisionTexture.Width));
                spriteBatch.Draw(collisionTexture, Destination, null, Color.White, angle, origin, SpriteEffects.None, SpriteUtilities.EffectsLayer);
                collTime--;
            }
            else
            {
                animationComplete = true;
            }
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public int Update(GameTime gameTime)
        {
            if (collided)
            {
                return 0;
            }
            else
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

        public bool Collided
        {
            get => collided;
            set => collided = value;
        }
    }
}