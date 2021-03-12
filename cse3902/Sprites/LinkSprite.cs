using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;
using System.Collections.Generic;

namespace cse3902.Sprites
{
    public class LinkSprite : ISprite
    {
        public enum AnimationState
        {
            LeftFacing,
            LeftRunning,
            RightFacing,
            RightRunning,
            UpFacing,
            UpRunning,
            DownFacing,
            DownRunning,
            LeftAttack,
            RightAttack,
            UpAttack,
            DownAttack,
        };

        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 center;
        private Vector2 startingPosition;
        private Vector2 size;
        private const float sizeIncrease = 1f;

        private bool isDamage;

        private (Rectangle frame, float delay)[] currentFrameSet;
        private int currentFrameIndex;
        private LinkSpriteAnimationHandler animationHandler;

        private LinkDamageMaskHandler maskHandler;

        private float remainingFrameDelay;
        private float remainingDamageDelay;

        private Rectangle destination;

        private const float damageDelay = .2f;

        private Boolean pauseMovement;

        public LinkSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Vector2 startingPosition)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;

            animationHandler = new LinkSpriteAnimationHandler(texture, rows, columns);
            size = animationHandler.FrameSize;
            currentFrameSet = animationHandler.getFrameSet(AnimationState.RightFacing);
            currentFrameIndex = 0;

            remainingFrameDelay = currentFrameSet[currentFrameIndex].delay;

            isDamage = false;

            maskHandler = new LinkDamageMaskHandler(texture);

	        this.startingPosition = startingPosition;
            center = startingPosition;

            pauseMovement = false;
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(size.X / 2f, size.Y / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(sizeIncrease * size.X), (int)(sizeIncrease * size.Y));
            spriteBatch.Draw(spriteTexture, Destination, currentFrameSet[currentFrameIndex].frame, Color.White, 0, origin, SpriteEffects.None, 0.2f);
        }
       

        public int Update(GameTime gameTime)
        {
            int ret = 0;
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (!this.pauseMovement) ret = UpdateFrame(timer);

            if (isDamage)
            {
                remainingDamageDelay -= timer;
                if (remainingDamageDelay < 0)
                {
                    remainingDamageDelay = damageDelay;
                    maskHandler.LoadNextMask();
                }
            }
            
            return ret;
        }

        private int UpdateFrame(float timer)
        {
            int ret = 0;

            remainingFrameDelay -= timer;

            if (remainingFrameDelay <= 0)
            {
                currentFrameIndex++;
                if (currentFrameIndex >= currentFrameSet.Length)
                {
                    currentFrameIndex = 0;
                    ret = -1;
                }

                remainingFrameDelay = currentFrameSet[currentFrameIndex].delay;
            }

            return ret;
        }

        public void setFrameSet(AnimationState animState)
        {
            currentFrameSet = animationHandler.getFrameSet(animState);
            currentFrameIndex = 0;
            remainingFrameDelay = currentFrameSet[currentFrameIndex].delay;
	    }

        public void Erase()
        {
            spriteTexture.Dispose();
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

        public Vector2 StartingPosition
        {
            get => this.startingPosition;
            set
            {
                this.startingPosition = value;
                this.center = value;
            }
        }

        public Vector2 Size
        {
            get => size * sizeIncrease;
        }

        public bool Damaged
        {
            get => isDamage;
            set 
	        {
                remainingDamageDelay = damageDelay;
                isDamage = value;
                maskHandler.Reset();
            }
        }

        public Boolean PauseMovement
        {
            set => this.pauseMovement = value;
        }
    }
}