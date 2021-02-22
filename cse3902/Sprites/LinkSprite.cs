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

        private (Rectangle frame, float delay)[] currentFrameSet;
        private int currentFrameIndex;
        private LinkSpriteAnimationHandler animationHandler;
        private AnimationState currentAnimationState;

        private float remainingDelay;

        public LinkSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Vector2 startingPosition)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;

            animationHandler = new LinkSpriteAnimationHandler(texture, rows, columns, AnimationState.RightFacing);
            size = animationHandler.FrameSize;
            currentFrameSet = animationHandler.getFrameSet(AnimationState.RightFacing);
            currentAnimationState = AnimationState.RightFacing;
            currentFrameIndex = 0;

            remainingDelay = currentFrameSet[currentFrameIndex].delay;
            
	        this.startingPosition = startingPosition;
            center = startingPosition;
        }

        public void Draw()
        {
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(size.X * sizeIncrease), (int)(size.Y* sizeIncrease));

            spriteBatch.Begin();
            spriteBatch.Draw(spriteTexture, Destination, currentFrameSet[currentFrameIndex].frame, Color.White);
            spriteBatch.End();
        }

        
	    public void Update(GameTime gameTime, onAnimCompleteCallback animationCompleteCallback)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingDelay -= timer;

            if (remainingDelay <= 0)
            {
		        currentFrameIndex++;
                if (currentFrameIndex >= currentFrameSet.Length)
                {
                    currentFrameIndex = 0;
                    animationCompleteCallback();
                }
                /*
                if (Damaged && (currentFrameIndex * 4 == currentFrameSet.Length))
                {
                    animationCompleteCallback();
                }
                */
                remainingDelay = currentFrameSet[currentFrameIndex].delay;
            }
        }

        public void setFrameSet(AnimationState animState)
        {
            currentAnimationState = animState;
            currentFrameSet = animationHandler.getFrameSet(animState);
            currentFrameIndex = 0;
            remainingDelay = currentFrameSet[currentFrameIndex].delay;
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
            get => animationHandler.Damage;
            set
	        {
                animationHandler.Damage = value;
                setFrameSet(currentAnimationState);
            } 
        }
    }
}