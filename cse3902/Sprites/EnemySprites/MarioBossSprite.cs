using System;
using cse3902.Constants;
using cse3902.Entities.DamageMasks;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;

namespace cse3902.Sprites.EnemySprites
{
    public class MarioBossSprite : ISprite
    {

        public enum FrameIndex
        {
            LeftStart = 0,
            LeftMidWay = 1,
            LeftComplete = 2,
            LeftFireball = 3

        };

        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

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



        public MarioBossSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Texture2D damageSequence, Vector2 startingPosition)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;
            remainingDelay = MovementConstants.MarioDelay;

            isDamage = false;
            remainingDamageDelay = DamageConstants.DamageMaskDelay;

            currentFrame = (int)FrameIndex.LeftStart;
            frameIndex.startingFrameIndex = (int)FrameIndex.LeftStart;
            frameIndex.endingFrameIndex = (int)FrameIndex.LeftFireball+1;
            size.X = spriteTexture.Width / columns;
            size.Y = spriteTexture.Height / rows;
            center = startingPosition;

            damageMaskHandler = new GenericTextureMask(texture, damageSequence, 1, 4, 1);

            frames = SpriteUtilities.distributeFrames(columns, rows, (int)size.X, (int)size.Y);

        }

        public void Draw()
        {

            Vector2 origin = new Vector2(size.X / 2f, size.Y / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(size.X), (int)(size.Y));
            spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.EnemyLayer);

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
                remainingDelay = MovementConstants.MarioDelay;
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
            get => frameIndex.startingFrameIndex;
            set
            {
                frameIndex.startingFrameIndex = value;
                frameIndex.endingFrameIndex = value - 3;

                if (currentFrame < frameIndex.endingFrameIndex || currentFrame >= frameIndex.startingFrameIndex)
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
