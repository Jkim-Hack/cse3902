using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Sprites;
using cse3902.Sounds;
using cse3902.Collision.Collidables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using cse3902.Constants;

namespace cse3902.Projectiles
{
    public class BoomerangProjectile : IProjectile
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private float angle;
        private int travelDistance;

        private (int Width, int Height) frame;

        private Rectangle destination;

        private (Vector2 direction, bool alreadyReversed) direction;
        private Vector2 center;

        private bool animationComplete;
        private bool collided;

        private ICollidable collidable;
        private ISprite entity;

        public BoomerangProjectile(SpriteBatch batch, Texture2D texture, ISprite entity, Vector2 dir, Game1 game, int travelDistance)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            this.entity = entity;
            center = entity.Center;

            frame.Width = spriteTexture.Width;
            frame.Height = spriteTexture.Height;

            direction.direction = dir;
            direction.alreadyReversed = false;
            angle = 0;
            this.travelDistance = travelDistance;

            animationComplete = false;

            this.collidable = new ProjectileCollidable(this, game);
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frame.Width / 2f, frame.Height / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, frame.Width, frame.Height);
            spriteBatch.Draw(spriteTexture, Destination, null, Color.White, angle, origin, SpriteEffects.None, SpriteUtilities.ProjectileLayer);
        }

        public int Update(GameTime gameTime)
        {

            if (animationComplete) return -1;

            center += ItemConstants.BoomerangSpeed * direction.direction;
            if (direction.direction.Y == 0) center.Y = entity.Center.Y;
            else center.X = entity.Center.X;

            travelDistance--;
            if (travelDistance == 0)
            {
                direction.alreadyReversed = true;
                direction.direction = -direction.direction;
            }

            angle += ItemConstants.AnglePiOver8;
            if (angle >= 2 * ItemConstants.Angle180Rad) angle = 0;
            if (Math.Abs(angle - ItemConstants.AnglePiOver8) <= ItemConstants.epsilon) SoundFactory.PlaySound(SoundFactory.Instance.arrowBoomerang);

            /* Animation is done if boomerang is travelling back to the throwing entity and collides with it */
            if (travelDistance < 0 && entity.Box.Intersects(this.Box))
            {
                return -1;
            }
            return 0;
        }

        public void ReverseDirectionIfNecessary()
        {
            if (!direction.alreadyReversed)
            {
                direction.alreadyReversed = true;
                direction.direction = -direction.direction;
                travelDistance = -1;
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
                Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(width * cos + height * sin), (int)(height * cos + width * sin));
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
            }
        }

        public Vector2 Center
        {
            get
            {
                return center;
            }
            set
            {
                center.X = value.X;
                center.Y = value.Y;
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
            get => DamageConstants.BoomerangDamage; //stuns if Link throws
        }

        public Vector2 Direction
        {
            get => this.direction.direction;
            set => this.direction.direction = value;

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