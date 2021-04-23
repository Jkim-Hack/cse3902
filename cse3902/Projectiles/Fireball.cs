using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.Constants;
using cse3902.Interfaces;
using cse3902.ParticleSystem;
using cse3902.Sprites;
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

        private float speed;
        private bool animationComplete;
        private bool collided;

        private float fireballCounter;
        private const float fireballDelay = ItemConstants.FireballDelay;

        private Rectangle destination;

        private ICollidable collidable;
        private IDependentParticleEmmiter fireballEmitter;

        public Fireball(SpriteBatch spriteBatch, Texture2D texture, Vector2 startingPosition, Vector2 direction, Game1 game)
        {
            this.spriteBatch = spriteBatch;
            this.spriteTexture = texture;
            this.direction = direction;
            this.center = startingPosition;
            animationComplete = false;
            fireballCounter = fireballDelay;
            speed = ItemConstants.FireballSpeed;

            if (ParticleEngine.Instance.UseParticleEffects) fireballEmitter = ParticleEngine.Instance.CreateFireballEffect(this);

            this.collidable = new ProjectileCollidable(this, game);
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
            if (collided) return -1;
            if (fireballCounter < 0)
            {
                KillParticles();
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
            if (!ParticleEngine.Instance.UseParticleEffects)
            {
                Vector2 origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
                Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, Texture.Width, Texture.Height);
                spriteBatch.Draw(spriteTexture, Destination, null, Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.ProjectileLayer);
            }
        }

        public void KillParticles()
        {
            if (ParticleEngine.Instance.UseParticleEffects) fireballEmitter.Kill = true;
        }

        public ref Rectangle Box
        {
            get
            {
                int width = Texture.Width;
                int height = Texture.Height;
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
        public float Speed
        {
            get => this.speed;
            set => this.speed = value;
        }

        public int Damage
        {
            get => SettingsValues.Instance.GetValue(SettingsValues.Variable.FireballDamage);
        }

        public Vector2 Direction
        {
            get => this.direction * speed;
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