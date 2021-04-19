using System;
using cse3902.Constants;
using cse3902.Entities.DamageMasks;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;

namespace cse3902.Sprites.EnemySprites
{
    public class BoggusBossSprite : ISprite
    {

        public enum FrameIndex
        {
            RightFacing = 0,
            LeftFacing = 2

        };

        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private Vector2 center;

        private int currentFrame;
        private Rectangle[] frames;
        private int frameWidth;
        private int frameHeight;

        private int startingFrameIndex;
        private int endingFrameIndex;

        private const float delay = 0.2f;
        private float remainingDelay;

        private bool isDamage;
        private DamageMaskHandler damageMaskHandler;

        private Rectangle destination;

        private float remainingDamageDelay;

        private const float sizeIncrease = 1f;


        public BoggusBossSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Texture2D damageSequence, Vector2 startingPosition)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;
            remainingDelay = delay;

            isDamage = false;
            remainingDamageDelay = DamageConstants.DamageMaskDelay;

            currentFrame = 0;
            startingFrameIndex = (int)FrameIndex.LeftFacing;
            endingFrameIndex = startingFrameIndex + 2;
            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            center = startingPosition;

            damageMaskHandler = new DamageMaskHandler(texture, damageSequence, 1, 4, 1);

            frames = SpriteUtilities.distributeFrames(columns, rows, frameWidth, frameHeight);

        }

        public void Draw()
        {

            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(sizeIncrease * frameWidth), (int)(sizeIncrease * frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.EnemyLayer);

        }

        public ref Rectangle Box
        {
            get
            {
                int width = (int)(sizeIncrease * frameWidth);
                int height = (int)(sizeIncrease * frameHeight);
                Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, width, height);
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
            }
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public int Update(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingDelay -= timer;

            if (isDamage)
            {
                remainingDamageDelay -= timer;
                if (remainingDamageDelay < 0)
                {
                    remainingDamageDelay = DamageConstants.DamageMaskDelay;
                    damageMaskHandler.LoadNextMask();
                }
            }

            if (remainingDelay <= 0)
            {
                currentFrame++;
                if (currentFrame == endingFrameIndex)
                {
                    currentFrame = startingFrameIndex;
                }
                remainingDelay = delay;
            }
            return 0;
        }

        public Vector2 Center
        {
            get => center;
            set => center = value;
        }

        public Texture2D Texture
        {
            get => spriteTexture;
        }

        public int StartingFrameIndex
        {
            get => startingFrameIndex;
            set
            {
                startingFrameIndex = value;
                endingFrameIndex = value + 2;

                if (currentFrame >= endingFrameIndex || currentFrame < startingFrameIndex)
                {

                    currentFrame = value;
                }
            }
        }

        public bool Damaged
        {
            get => isDamage;
            set
            {
                remainingDamageDelay = DamageConstants.DamageMaskDelay;
                isDamage = value;
                damageMaskHandler.Reset();
            }
        }
    }
}
