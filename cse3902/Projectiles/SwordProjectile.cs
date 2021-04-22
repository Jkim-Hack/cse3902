using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.ParticleSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using cse3902.Sprites;
using cse3902.Constants;
using cse3902.HUD;

namespace cse3902.Projectiles
{
    public class SwordProjectile : IProjectile
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private ISprite collisionTexture;

        private int currentFrame;
        private Rectangle[] frames;
        private (int Width, int Height) frame;

        private float remainingDelay;
        private Rectangle destination;
        private Vector2 current;
        private Vector2 direction;
        private bool collided;

        private ICollidable collidable;

        public SwordProjectile(SpriteBatch batch, Texture2D texture, Vector2 startingPos, Vector2 dir)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            remainingDelay = ItemConstants.SwordBeamDelay;
            int rows = ItemConstants.SwordBeamRows;
            int columns = ItemConstants.SwordBeamCols;
            currentFrame = 0;
            frame.Width = spriteTexture.Width / columns;
            frame.Height = spriteTexture.Height / rows;
            frames = SpriteUtilities.distributeFrames(columns, rows, frame.Width, frame.Height);
            direction = dir;

            current = new Vector2(startingPos.X, startingPos.Y);
            collisionTexture = ProjectileHandler.Instance.CreatePoofAnim(spriteBatch, new Vector2(current.X, current.Y));
            this.collidable = new ProjectileCollidable(this);
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frame.Width / 2f, frame.Height / 2f);
            Rectangle Destination = new Rectangle((int)current.X, (int)current.Y, frame.Width, frame.Height);

            if (!collided)
            {
                float angle = calculateAngle(direction);
                spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White, angle, origin, SpriteEffects.None, SpriteUtilities.ProjectileLayer);
            }
            else
            {
                if (ParticleEngine.Instance.UseParticleEffects)
                {
                    origin = new Vector2(current.X, current.Y) - new Vector2(frame.Width, frame.Height) / 10 +  direction * 5;
                    ParticleEngine.Instance.CreateSwordEffect(origin);
                }
                else
                {
                    collisionTexture.Center = new Vector2(current.X, current.Y);
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
                    if (currentFrame == frames.Length)
                    {
                        currentFrame = 0;
                    }
                    remainingDelay = ItemConstants.SwordBeamDelay;
                }

                current += direction * ItemConstants.SwordBeamSpeed;
                return 0;
            }
            else
            {
                if (remainingDelay >= 0)
                {
                    collisionTexture.Center = current;
                    return collisionTexture.Update(gameTime);
                }
                else
                {
                    return -1;
                }
            }
            
        }
        
        private float calculateAngle(Vector2 dir)
        {
            float angle;
            if (dir.X > 0)
            {
                angle = ItemConstants.Angle90Rad;
            }
            else if (dir.X < 0)
            {
                angle = ItemConstants.Angle270Rad;
            }
            else if (dir.Y > 0)
            {
                angle = ItemConstants.Angle180Rad;
            }
            else
            {
                angle = ItemConstants.Angle0Rad;
            }
            return angle;
        }

        public ref Rectangle Box
        {
            get
            {
                int width = frame.Width;
                int height = frame.Height;
                float angle = calculateAngle(direction);
                double cos = Math.Abs(Math.Cos(angle));
                double sin = Math.Abs(Math.Sin(angle));
                Rectangle Destination = new Rectangle((int) current.X, (int) current.Y, (int)(width * cos + height * sin), (int)(height * cos + width * sin));
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
            }
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(current.X, current.Y);
            }
            set
            {
                current.X = (int)value.X;
                current.Y = (int)value.Y;
            }
        }

        public Texture2D Texture
        {
            get => spriteTexture;
        }

        public bool AnimationComplete
        {
            get => collided && remainingDelay < 0;
            set
            {
                remainingDelay = -1;
                collided = value;
            }
        }

        public int Damage
        {
            get => DamageConstants.SwordDamage;
            
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