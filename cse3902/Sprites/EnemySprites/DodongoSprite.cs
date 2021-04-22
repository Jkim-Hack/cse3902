using System;
using cse3902.Entities.DamageMasks;
using cse3902.Interfaces;
using cse3902.Rooms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;

namespace cse3902.Sprites.EnemySprites
{
    public class DodongoSprite : ISprite
    {
        public enum FrameIndex
        {
            DownFacing = 0,
            DownBomb = 1,
            UpFacing = 2,
            UpBomb = 3,
            RightFacing = 4,
            RightBomb = 6,
            LeftFacing = 7,
            LeftBomb = 9

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
        private GenericTextureMask damageMaskHandler;

        private Rectangle destination;

        private float remainingDamageDelay;
        private const float damageDelay = .05f;

        private const float sizeIncrease = .7f;


        public DodongoSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Texture2D damageSequence, Vector2 startingPosition)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;
            remainingDelay = delay;

            isDamage = false;
            remainingDamageDelay = damageDelay;

            currentFrame = 4;

            startingFrameIndex = 0;
            endingFrameIndex = 8;

            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            frames = SpriteUtilities.distributeFrames(columns, rows, frameWidth, frameHeight);

            center = startingPosition;

            damageMaskHandler = new GenericTextureMask(texture, damageSequence, 1, 4, 3);

        }

        public void Draw()
        {
            Vector2 origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, frameWidth, frameHeight);
            if (this.IsReversible && currentFrame > startingFrameIndex)
            {
                spriteBatch.Draw(spriteTexture, Destination, frames[startingFrameIndex], Color.White, 0, origin, SpriteEffects.FlipHorizontally, SpriteUtilities.EnemyLayer);
            } else
            {
                spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.EnemyLayer);
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
                    remainingDamageDelay = damageDelay;
                    damageMaskHandler.LoadNextMask();
                }
            }

            if (remainingDelay <= 0)
            {
                if (this.StartingFrameIndex == (int)DodongoSprite.FrameIndex.DownBomb || this.StartingFrameIndex == (int)DodongoSprite.FrameIndex.UpBomb || this.StartingFrameIndex == (int)DodongoSprite.FrameIndex.LeftBomb || this.StartingFrameIndex == (int)DodongoSprite.FrameIndex.RightBomb)
                {
                    return 0;
                }
                currentFrame++;
                if (currentFrame == endingFrameIndex)
                {
                    currentFrame = startingFrameIndex;
                }
                remainingDelay = delay;
            }
            return 0;
        }

        public ref Rectangle Box
        {
            get
            {
                Rectangle Destination;
                if (StartingFrameIndex == (int)DodongoSprite.FrameIndex.DownFacing || StartingFrameIndex == (int)DodongoSprite.FrameIndex.UpFacing)
                {
                    Destination = new Rectangle((int)center.X, (int)center.Y, RoomUtilities.BLOCK_SIDE, RoomUtilities.BLOCK_SIDE);
                    Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                } else
                {
                    int width = (int)(sizeIncrease * frameWidth);
                    int height = (int)(frameHeight);
                    width += Math.Abs(height - width);
                    Destination = new Rectangle((int)center.X, (int)center.Y, width, height);
                    Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                }

                
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
                remainingDamageDelay = damageDelay;
                isDamage = value;
                damageMaskHandler.Reset();
            }
        }

        private bool IsReversible
        {
            get
            {
                if (this.StartingFrameIndex == (int)DodongoSprite.FrameIndex.DownFacing || this.StartingFrameIndex == (int)DodongoSprite.FrameIndex.UpFacing)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }
    }
}
