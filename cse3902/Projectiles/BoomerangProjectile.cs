using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Sprites;
using cse3902.Collision.Collidables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace cse3902.Projectiles
{
    public class BoomerangProjectile : IProjectile
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private float angle;
        private const float sizeIncrease = 1f;

        private int travelDistance;

        private int frameWidth;
        private int frameHeight;

        private Rectangle destination;

        private Vector2 direction;
        private Vector2 center;

        private bool animationComplete;
        private bool collided;

        private ICollidable collidable;
        private LinkSprite link;

        public BoomerangProjectile(SpriteBatch batch, Texture2D texture, LinkSprite link, Vector2 dir)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            this.link = link;
            center = link.Center;

            frameWidth = spriteTexture.Width;
            frameHeight = spriteTexture.Height;

            direction = dir;
            angle = 0;
            travelDistance = 35;

            animationComplete = false;

            this.collidable = new ProjectileCollidable(this);
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, null, Color.White, angle, origin, SpriteEffects.None, SpriteUtilities.ProjectileLayer);
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public int Update(GameTime gameTime)
        {

            if (animationComplete) return -1;

            center += 1.25f * direction;
            if (direction.Y == 0) center.Y = link.Center.Y;
            else center.X = link.Center.X;

            travelDistance--;
            if (travelDistance == 0) direction = -direction;

            angle += (float)(Math.PI / 8);
            if (angle >= 2 * Math.PI) angle = 0;

            /* Animation is done if boomerang is travelling back to Link and collides with him */
            if (travelDistance < 0 && link.Box.Intersects(this.Box)) animationComplete = true;

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

        public bool Collided
        {
            get => collided;
            set => collided = value;
        }
    }
}