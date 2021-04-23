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
            MouthOpen = 0,
            MouthClosed = 2

        };

        private (SpriteBatch spriteBatch, Texture2D spriteTexture) spriteInfo;

        private Vector2 center;

        private int currentFrame;
        private Rectangle[] frames;
        private Vector2 size;

        private (int startingFrameIndex, int endingFrameIndex) frameIndex;


        private const float delay = MovementConstants.AquamentusDelay;
        private float remainingDelay;

        private bool isDamage;
        private GenericTextureMask damageMaskHandler;

        private Rectangle destination;

        private float remainingDamageDelay;



        public BoggusBossSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Texture2D damageSequence, Vector2 startingPosition)
        {
            this.spriteInfo.spriteBatch = spriteBatch;
            spriteInfo.spriteTexture = texture;
            remainingDelay = delay;

            isDamage = false;
            remainingDamageDelay = DamageConstants.DamageMaskDelay;

            currentFrame = 2;
            frameIndex.startingFrameIndex = (int)FrameIndex.MouthClosed;
            frameIndex.endingFrameIndex = frameIndex.startingFrameIndex + 2;
            size.X = spriteInfo.spriteTexture.Width / columns;
            size.Y = spriteInfo.spriteTexture.Height / rows;
            center = startingPosition;

            damageMaskHandler = new GenericTextureMask(texture, damageSequence, 1, 4, 1);

            frames = SpriteUtilities.distributeFrames(columns, rows, (int)size.X, (int)size.Y);

        }

        public void Draw()
        {

            Vector2 origin = new Vector2(size.X / 2f, size.Y / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(size.X), (int)(size.Y));
            spriteInfo.spriteBatch.Draw(spriteInfo.spriteTexture, Destination, frames[currentFrame], Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.EnemyLayer);

        }

        public ref Rectangle Box
        {
            get
            {
                int width = (int)(size.X);
                int height = (int)(size.Y);
                Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, width, height);
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
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
                    remainingDamageDelay = DamageConstants.DamageMaskDelay;
                    damageMaskHandler.LoadNextMask();
                }
            }

            if (remainingDelay <= 0)
            {
                currentFrame++;
                if (currentFrame == frameIndex.endingFrameIndex)
                {
                    currentFrame = frameIndex.startingFrameIndex;
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
                remainingDamageDelay = DamageConstants.DamageMaskDelay;
                isDamage = value;
                damageMaskHandler.Reset();
            }
        }
    }
}
