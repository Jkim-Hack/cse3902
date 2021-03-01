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
        private const float sizeIncrease = 2f;

        private bool isDamage;

        private (Rectangle frame, float delay)[] currentFrameSet;
        private int currentFrameIndex;
        private LinkSpriteAnimationHandler animationHandler;

        private LinkDamageMaskHandler maskHandler;

        private float remainingFrameDelay;

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
        }

        public void Draw()
        {
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)Size.X, (int)Size.Y);

            spriteBatch.Draw(spriteTexture, Destination, currentFrameSet[currentFrameIndex].frame, Color.White,0, new Vector2(0,0), SpriteEffects.None, .2f);
        }
       
	    public int Update(GameTime gameTime)
        {
            Update(gameTime, null);
            return 0;
	    }

        public void Update(GameTime gameTime, onAnimCompleteCallback onAnimCompleteCallback)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingFrameDelay -= timer;

            if (remainingFrameDelay <= 0)
            {
                currentFrameIndex++;
                if (currentFrameIndex >= currentFrameSet.Length)
                {
                    currentFrameIndex = 0;
                    if (onAnimCompleteCallback != null) onAnimCompleteCallback();
                }

                remainingFrameDelay = currentFrameSet[currentFrameIndex].delay;
            }
            else if (remainingFrameDelay <= 1)
            {
		        if (isDamage) maskHandler.LoadNextMask();
            }
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
                isDamage = value;
                maskHandler.Reset();
            }
        }
    }
}