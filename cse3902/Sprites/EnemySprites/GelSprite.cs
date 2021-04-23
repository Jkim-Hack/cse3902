using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Constants;
using static cse3902.Interfaces.ISprite;

namespace cse3902.Sprites.EnemySprites
{
    public class GelSprite : ISprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private Vector2 center;
        private int currentFrame;
        private Rectangle[] frames;
        private Vector2 size;

        private (int startingFrameIndex, int endingFrameIndex) frameIndex;

        private float remainingDelay;

        private bool isAttacking;

        private Rectangle destination;

        private const float sizeIncrease = 1f;

        public GelSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Vector2 startingPosition)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;
            remainingDelay = MovementConstants.GelDelay;

            currentFrame = 0;

            frameIndex.startingFrameIndex = 0;
            frameIndex.endingFrameIndex = 2;

            size.X = spriteTexture.Width / columns;
            size.Y = spriteTexture.Height / rows;
            frames = SpriteUtilities.distributeFrames(columns, rows, (int)size.X, (int)size.Y);

            center = startingPosition;

            isAttacking = false;
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(size.X / 2f, size.Y / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(sizeIncrease * size.X), (int)(sizeIncrease * size.Y));
            spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.EnemyLayer);
        }

        public int Update(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingDelay -= timer;

            if (remainingDelay <= 0)
            {
                currentFrame++;
                if (currentFrame == frameIndex.endingFrameIndex)
                {
                    currentFrame = frameIndex.startingFrameIndex;
                }
                remainingDelay = MovementConstants.GelDelay;
            }
            return 0;
        }

        public ref Rectangle Box
        {
            get
            {
                int width = (int)(sizeIncrease * size.X);
                int height = (int)(sizeIncrease * size.Y);
                Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, width, height);
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
            }
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
            get => frameIndex.startingFrameIndex;
            set
            {
                frameIndex.startingFrameIndex = value;
                frameIndex.endingFrameIndex = value + 2;
            }
        }

        public bool IsAttacking
        {
            get => isAttacking;
            set
            {
                isAttacking = value;
            }
        }
    }
}
