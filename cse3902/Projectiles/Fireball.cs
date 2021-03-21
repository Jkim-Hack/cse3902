using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Projectiles

{
    public class Fireball : IProjectile
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 center;
        private Vector2 direction;

        private const float speed = 1.3f;
        private bool animationComplete;
        private bool collided;

        private float fireballCounter;
        private const float fireballDelay = 3f;

        private Rectangle destination;

        private const float sizeIncrease = 1f;

        private ICollidable collidable;

        public Fireball(SpriteBatch spriteBatch, Texture2D texture, Vector2 startingPosition, Vector2 direction)
        {
            this.spriteBatch = spriteBatch;
            this.spriteTexture = texture;
            this.direction = direction;
            this.center = startingPosition;
            animationComplete = false;
            fireballCounter = fireballDelay;

            this.collidable = new ProjectileCollidable(this);
        }

        public Vector2 Center
        {
            get => center;
            set
            {
                center = value;
            }
        }

        public Texture2D Texture
        {
            get => spriteTexture;
        }

        public int Update(GameTime gameTime)
        {

            if(fireballCounter < 0)
            {
                animationComplete = true;
                return -1;
            }
            else
            {
                fireballCounter -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            center.X += (float)(speed * direction.X);
            center.Y += (float)(speed * direction.Y);
            return 0;
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(sizeIncrease * Texture.Width), (int)(sizeIncrease * Texture.Height));
            spriteBatch.Draw(spriteTexture, Destination, null, Color.White, 0, origin, SpriteEffects.None, 0.6f);
        }

        public ref Rectangle Box
        {
            get
            {
                int width = (int)(sizeIncrease * Texture.Width);
                int height = (int)(sizeIncrease * Texture.Height);
                Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, width, height);
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
            }
        }

        public bool AnimationComplete
        {
            get => animationComplete;
            set => animationComplete = value;
        }

        public void Erase()
        {
            spriteTexture.Dispose();
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