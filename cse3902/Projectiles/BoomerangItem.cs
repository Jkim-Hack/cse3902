using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Sprites;
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
        private float angle;
        private const float sizeIncrease = 1f;

        private int travelDistance;

        private int frameWidth;
        private int frameHeight;

        private Rectangle destination;

        private Vector2 direction;
        private Vector2 center;

        private bool animationComplete;

        private ICollidable collidable;
        private LinkSprite link;

        public BoomerangItem(SpriteBatch batch, Texture2D texture, LinkSprite link, Vector2 dir)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            this.link = link;
            center = link.Center;

            frameWidth = spriteTexture.Width;
            frameHeight = spriteTexture.Height;

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

            animationComplete = false;

            travelDistance = 70;

            this.collidable = new ProjectileCollidable(this);
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, null, Color.White, angle, origin, SpriteEffects.None, 0.8f);
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public int Update(GameTime gameTime)
        {

            if (animationComplete) return -1;

            center += direction;
            travelDistance--;
            if (travelDistance == 0) direction = -direction;

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
    }
}