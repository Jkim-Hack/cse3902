using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Rooms;
using cse3902.Sprites;

namespace cse3902.Projectiles
{
    public class BombProjectile : IProjectile
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private int rows;
        private int columns;
        private int currentFrame;
        private int totalFrames;
        private Rectangle[] frames;
        private int frameWidth;
        private int frameHeight;

        private int currentX;
        private int currentY;
        private const float sizeIncrease = 1f;

        private Rectangle destination;
        private Rectangle preExplosion;

        private const float delay = 0.8f;
        private float remainingDelay;

        private bool animationComplete;

        private ICollidable collidable;

        public BombProjectile(SpriteBatch batch, Texture2D texture, Vector2 startingPos)
        {
            spriteBatch = batch;
            spriteTexture = texture;

            remainingDelay = delay;
            this.rows = 2;
            this.columns = 1;
            currentFrame = 0;
            totalFrames = rows * columns;
            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            frames = SpriteUtilities.distributeFrames(columns, rows, frameWidth, frameHeight);

            preExplosion = new Rectangle();

            currentX = (int)startingPos.X;
            currentY = (int)startingPos.Y;

            this.animationComplete = false;

            this.collidable = new ProjectileCollidable(this);

            SoundFactory.PlaySound(SoundFactory.Instance.bombDrop);
        }

        public ref Rectangle Box
        {
            get
            {
                if (currentFrame == 0) return ref preExplosion;

                int width = (int)(sizeIncrease * frameWidth / 1.5f);
                int height = (int)(sizeIncrease * frameHeight / 1.5f);
                Rectangle Destination = new Rectangle(currentX, currentY, width, height);
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2 - 1);
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

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle(currentX, currentY, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White, 0f, origin, SpriteEffects.None, SpriteUtilities.ProjectileLayer);
        }

        public int Update(GameTime gameTime)
        {
            if (animationComplete) return -1;

            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingDelay -= timer;

            if (remainingDelay <= 0)
            {
                if (currentFrame == 0) SoundFactory.PlaySound(SoundFactory.Instance.bombBlow);

                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                    animationComplete = true;
                }
                remainingDelay = delay;
            }
            return 0;
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
            get => 4;
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