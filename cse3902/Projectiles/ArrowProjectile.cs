using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.Constants;
using cse3902.Interfaces;
using cse3902.ParticleSystem;
using cse3902.Sounds;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cse3902.Projectiles
{
    public class ArrowProjectile : IProjectile
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Texture2D collisionTexture;

        private (int X, int Y) current;
        private (int Width, int Height) frame;
        private int collTime;

        private Vector2 direction;
        private float angle = 0;

        private bool animationComplete;
        private Rectangle destination;

        private ICollidable collidable;

        public ArrowProjectile(SpriteBatch batch, Texture2D texture, Vector2 startingPos, Vector2 dir)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            direction = dir;

            if (dir.X > 0)
            {
                direction = new Vector2(1, 0);
                angle = ItemConstants.Angle90Rad;
            }
            else if (dir.X < 0)
            {
                direction = new Vector2(-1, 0);
                angle = ItemConstants.Angle270Rad;
            }
            else if (dir.Y > 0)
            {
                direction = new Vector2(0, 1);
                angle = ItemConstants.Angle180Rad;
            }
            else
            {
                direction = new Vector2(0, -1);
                angle = ItemConstants.Angle0Rad;
            }

            animationComplete = false;
            collTime = -1;

            frame.Width = spriteTexture.Width;
            frame.Height = spriteTexture.Height;

            current.X = (int)startingPos.X;
            current.Y = (int)startingPos.Y;

            collisionTexture = ProjectileHandler.Instance.CreateStarAnimTexture();
            this.collidable = new ProjectileCollidable(this);

            SoundFactory.PlaySound(SoundFactory.Instance.arrowBoomerang);
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frame.Width / 2f, frame.Height / 2f);

            if (collTime <0)
            {
                Rectangle Destination = new Rectangle(current.X, current.Y, frame.Width, frame.Height);
                spriteBatch.Draw(spriteTexture, Destination, null, Color.White, angle, origin, SpriteEffects.None, SpriteUtilities.ProjectileLayer);
            }
            else
            {
                if (ParticleEngine.Instance.UseParticleEffects)
                {
                    origin = new Vector2(current.X, current.Y) - new Vector2(frame.Width, frame.Height) / ItemConstants.ArrowCollisionFrames + direction * ItemConstants.ArrowCollisionFrames;
                    ParticleEngine.Instance.CreateArrowEffect(origin);
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
            if (collTime > 0)
            {
                Rectangle Destination = new Rectangle(current.X, current.Y, (int)(2 * collisionTexture.Width), (int)(2 * collisionTexture.Width));
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
            if (collTime >= 0)
            {
                return 0;
            }
            else
            {
                if (direction.X > 0)
                {
                    current.X += ItemConstants.ArrowSpeed;
                }
                else if (direction.X < 0)
                {
                    current.X -= ItemConstants.ArrowSpeed;
                }
                else if (direction.Y > 0)
                {
                    current.Y += ItemConstants.ArrowSpeed;
                }
                else
                {
                    current.Y -= ItemConstants.ArrowSpeed;
                }
            }
            return 0;
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

        public ref Rectangle Box
        {
            get
            {
                int width = frame.Width;
                int height = frame.Height;
                double cos = Math.Abs(Math.Cos(angle));
                double sin = Math.Abs(Math.Sin(angle));
                Rectangle Destination = new Rectangle(current.X, current.Y, (int)(width * cos + height * sin), (int)(height * cos + width * sin));
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
            get => DamageConstants.ArrowDamage;
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
            get => collTime >= 0;
            set {
                if (collTime < 0)
                {
                    collTime = ItemConstants.ArrowCollisionFrames;
                }
            }
        }
    }
}