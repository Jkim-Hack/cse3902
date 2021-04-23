using System;
using cse3902.Interfaces;
using cse3902.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;

namespace cse3902.Sprites.EnemySprites
{
    public class WallMasterSprite : ISprite
    {
        public enum FrameIndex
        {
            LeftUpFacing = 0,
            RightUpFacing = 2,
            LeftDownFacing = 4,
            RightDownFacing = 6
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

        private float remainingDelay;

        private bool isAttacking;

        private Rectangle destination;


        public WallMasterSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Vector2 startingPosition)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;
            remainingDelay = MovementConstants.WallMasterDelay;

            currentFrame = 0;

            startingFrameIndex = (int)FrameIndex.LeftUpFacing;
            endingFrameIndex = startingFrameIndex + 2;

            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            frames = SpriteUtilities.distributeFrames(columns, rows, frameWidth, frameHeight);

            center = startingPosition;

            isAttacking = false;
        }

        public void Draw()
        {

            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(frameWidth), (int)(frameHeight));
            spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.EnemyLayer);
        }

        public int Update(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingDelay -= timer;

            if (remainingDelay <= 0)
            {
                currentFrame++;
                if (currentFrame == endingFrameIndex)
                {
                    currentFrame = startingFrameIndex;
                }
                remainingDelay = MovementConstants.WallMasterDelay;
            }
            return 0;
        }

        public ref Rectangle Box
        {
            get
            {
                int width = (int)(frameWidth);
                int height = (int)(frameHeight);
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
            get => startingFrameIndex;
            set
            {
                startingFrameIndex = value;
                endingFrameIndex = value + 2;

                if (currentFrame >= endingFrameIndex || currentFrame < startingFrameIndex) { 

                    currentFrame = value;
                }
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
