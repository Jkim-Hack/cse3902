using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.Sounds;
using cse3902.ParticleSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Rooms;
using cse3902.Sprites;
using cse3902.Constants;

namespace cse3902.Projectiles
{
    public class BombProjectile : IProjectile
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private int currentFrame;
        private Rectangle[] frames;
        private (int Width, int Height) frame;

        private (int X, int Y) current;
        private const float sizeIncrease = 1f;

        private Rectangle destination;
        private Rectangle preExplosion;

        private float remainingDelay;

        private bool animationComplete;
        private bool particlesGenerated;

        private ICollidable collidable;

        public BombProjectile(SpriteBatch batch, Texture2D texture, Vector2 startingPos, Game1 game)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            remainingDelay = ItemConstants.BombDelay;
            int rows = ItemConstants.BombRows;
            int columns = ItemConstants.BombCols;
            currentFrame = 0;
            frame.Width = spriteTexture.Width / columns;
            frame.Height = spriteTexture.Height / rows;
            frames = SpriteUtilities.distributeFrames(columns, rows, frame.Width, frame.Height);

            preExplosion = new Rectangle();

            current.X = (int)startingPos.X;
            current.Y = (int)startingPos.Y;

            this.animationComplete = false;
            this.particlesGenerated = false;

            this.collidable = new ProjectileCollidable(this, game);

            SoundFactory.PlaySound(SoundFactory.Instance.bombDrop);
        }

        public ref Rectangle Box
        {
            get
            {
                if (currentFrame == 0) return ref preExplosion;

                int width = (int)(sizeIncrease * frame.Width / 1.5f);
                int height = (int)(sizeIncrease * frame.Height / 1.5f);
                Rectangle Destination = new Rectangle(current.X, current.Y, width, height);
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2 - 1);
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

        public void Draw()
        {
            if (!ParticleEngine.Instance.UseParticleEffects || !particlesGenerated)
            {
                Vector2 origin = new Vector2(frame.Width / 2f, frame.Height / 2f);
                Rectangle Destination = new Rectangle(current.X, current.Y, (int)(sizeIncrease * frame.Width), (int)(sizeIncrease * frame.Height));
                spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White, 0f, origin, SpriteEffects.None, SpriteUtilities.ProjectileLayer);
            }
        }

        public int Update(GameTime gameTime)
        {
            if (animationComplete) return -1;

            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingDelay -= timer;

            if (remainingDelay <= 0)
            {
                if (currentFrame == 0) SoundFactory.PlaySound(SoundFactory.Instance.bombBlow);

                if (ParticleEngine.Instance.UseParticleEffects && !particlesGenerated)
                {
                    ParticleEngine.Instance.CreateBombEffect(Center - new Vector2(5, 5));
                    particlesGenerated = true;
                }

                currentFrame++;
                if (currentFrame == frames.Length)
                {
                    currentFrame = 0;
                    animationComplete = true;
                }
                remainingDelay = ItemConstants.BombDelay;
            }
            return 0;
        }

        public bool AnimationComplete
        {
            get => animationComplete;

            set => animationComplete = value;
        }

        public int Damage
        {
            get => DamageConstants.BombDamage;
        }

        public Vector2 Direction
        {
            get => new Vector2(0,0);
            set => this.Direction = value;

        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }

        public bool Collided
        {
            get => animationComplete;
            set => animationComplete = value;
        }
    }
}