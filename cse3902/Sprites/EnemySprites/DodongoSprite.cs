using System;
using cse3902.Entities.DamageMasks;
using cse3902.Interfaces;
using cse3902.Rooms;
using cse3902.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        private (SpriteBatch spriteBatch, Texture2D spriteTexture) spriteInfo;
        private Vector2 center;

        private int currentFrame;
        private Rectangle[] frames;
        private Vector2 size;

        private (int startingFrameIndex, int endingFrameIndex) frameIndex;

        private float remainingDelay;

        private bool isDamage;
        private GenericTextureMask damageMaskHandler;

        private Rectangle destination;

        private float remainingDamageDelay;
        private const float damageDelay = .05f;


        public DodongoSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Texture2D damageSequence, Vector2 startingPosition)
        {
            this.spriteInfo.spriteBatch = spriteBatch;
            spriteInfo.spriteTexture = texture;
            remainingDelay = MovementConstants.DodongoDelay;

            isDamage = false;
            remainingDamageDelay = damageDelay;

            currentFrame = (int)FrameIndex.RightFacing;

            frameIndex.startingFrameIndex = (int)FrameIndex.DownFacing;
            frameIndex.endingFrameIndex = (int)FrameIndex.LeftFacing+1;

            size.X = spriteInfo.spriteTexture.Width / columns;
            size.Y = spriteInfo.spriteTexture.Height / rows;
            frames = SpriteUtilities.distributeFrames(columns, rows, (int)size.X, (int)size.Y);

            center = startingPosition;

            damageMaskHandler = new GenericTextureMask(texture, damageSequence, 1, 4, 3);

        }

        public void Draw()
        {
            Vector2 origin = new Vector2(size.X / 2f, size.Y / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)size.X, (int)size.Y);
            if (this.IsReversible && currentFrame > frameIndex.startingFrameIndex)
            {
                spriteInfo.spriteBatch.Draw(spriteInfo.spriteTexture, Destination, frames[frameIndex.startingFrameIndex], Color.White, 0, origin, SpriteEffects.FlipHorizontally, SpriteUtilities.EnemyLayer);
            } else
            {
                spriteInfo.spriteBatch.Draw(spriteInfo.spriteTexture, Destination, frames[currentFrame], Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.EnemyLayer);
            }
            
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
                if (currentFrame == frameIndex.endingFrameIndex)
                {
                    currentFrame = frameIndex.startingFrameIndex;
                }
                remainingDelay = MovementConstants.DodongoDelay;
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
                    int width = (int)(MovementConstants.DodongoSizeIncrease * size.X);
                    int height = (int)(size.Y);
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
            get => spriteInfo.spriteTexture;
        }

        public int StartingFrameIndex
        {
            get => frameIndex.startingFrameIndex;
            set
            {
                frameIndex.startingFrameIndex = value;
                frameIndex.endingFrameIndex = value + 2;

                if (currentFrame >= frameIndex.endingFrameIndex || currentFrame < frameIndex.startingFrameIndex)
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
