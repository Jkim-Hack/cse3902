using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;
using System.Collections.Generic;
using System;

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
            DownAttack
        };

        // [starting, ending)
        private Dictionary<AnimationState, (int starting, int ending)> frameSets;

        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 center;
        private Vector2 startingPosition;

        private Rectangle[] frames;
        private int totalFrames;
        private Rectangle currentFrame;
	    private int frameWidth;
        private int frameHeight;
        private int currentFrameIndex;
        private (int starting, int ending) currentFrameSetRange;

        private const int damageOffset = 22;
        private int damage;
        private bool isBeingDamaged;
        private const int damagedEndIndex = 4;

        private const float delay = 0.2f;
        private float remainingDelay;

        public LinkSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Vector2 startingPosition)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;
            remainingDelay = delay;
            
	        totalFrames = rows * columns;
            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            frames = new Rectangle[totalFrames];
            distributeFrames(columns);
            generateFrameSets();

            currentFrameSetRange = frameSets[AnimationState.LeftFacing];
            currentFrame = frames[currentFrameSetRange.starting];
            currentFrameIndex = frameSets[AnimationState.LeftFacing].starting;
            
            damage = 0;
            isBeingDamaged = false;

            this.startingPosition = startingPosition;
            center = startingPosition;
        }

        private void distributeFrames(int columns)
        {
            for (int i = 0; i < totalFrames; i++)
            {
                int row = (int)((float)i / (float)columns);
                int column = i % columns;
                frames[i] = new Rectangle(frameWidth * column, frameHeight * row, frameWidth, frameHeight);
            }
        }

        private void generateFrameSets()
        {
            frameSets = new Dictionary<AnimationState, (int starting, int ending)>()
            {
                { AnimationState.LeftFacing, (2, 3) },
                { AnimationState.LeftRunning, (2, 4) },
                { AnimationState.RightFacing, (0, 1)},
                { AnimationState.RightRunning, (0, 2) },
                { AnimationState.UpFacing, (4, 5) },
                { AnimationState.UpRunning, (4, 6) },
                { AnimationState.DownFacing, (6, 7) },
                { AnimationState.DownRunning, (6, 8) },
                { AnimationState.LeftAttack, (9, 10) },
                { AnimationState.RightAttack, (8, 9) },
                { AnimationState.UpAttack, (10, 11) },
                { AnimationState.DownAttack, (11, 12) },
            };
        }

        public void Draw()
        {
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, frameWidth, frameHeight);

            spriteBatch.Begin();
            spriteBatch.Draw(spriteTexture, Destination, currentFrame, Color.White);
            spriteBatch.End();
        }

        
	    public void Update(GameTime gameTime, onAnimCompleteCallback animationCompleteCallback)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingDelay -= timer;

            if (remainingDelay <= 0)
            {
                remainingDelay = delay;
                
		        if (isBeingDamaged)
                {
                    if (damage == damagedEndIndex) damage = 0;
                    currentFrameIndex += damage * damageOffset;
                }
                
                if ((currentFrameSetRange.ending + damage * damageOffset) == currentFrameIndex)
                {
                    currentFrameIndex = currentFrameSetRange.starting;
                    animationCompleteCallback();
                }

                if (isBeingDamaged) damage++;

		        currentFrame = frames[currentFrameIndex];
                currentFrameIndex++;
            }
        }

        public void setFrameSet(AnimationState index)
        {
            remainingDelay = delay;
            currentFrameIndex = frameSets[index].starting;
            currentFrameSetRange = frameSets[index];
            currentFrame = frames[currentFrameIndex];
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

        public Rectangle Bounds
        {
            get => spriteTexture.Bounds;
        }

        public bool Damaged
        {
            set 
            {
                if (value)
                {
                    this.isBeingDamaged = true;
                }
                if (!value)
                {
                    this.isBeingDamaged = false;
                    currentFrameIndex -= damage * damageOffset;
                }
			    this.damage = 0;
            }
        }
    }
}