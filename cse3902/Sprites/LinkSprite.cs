using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;
using System.Collections.Generic;
using cse3902.Entities.DamageMasks;
using cse3902.Constants;

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
            Item,
            Death
        };

        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private (Vector2 current, Vector2 previous) positions;
        private Vector2 size;

        private (Rectangle frame, float delay)[] currentFrameSet;
        private int currentFrameIndex;
        private LinkSpriteAnimationHandler animationHandler;

        private DamageMaskHandler maskHandler;
        private SingleMaskHandler deathMaskHandler;

        private bool isDamaged;
        private (float frame, float damage) remainingDelay;

        private Rectangle destination;

        private bool pauseMovement;

        public LinkSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, DamageMaskHandler maskHandler, SingleMaskHandler singleMaskHandler, Vector2 startingPosition)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;

            this.maskHandler = maskHandler;
            deathMaskHandler = singleMaskHandler;

	        animationHandler = new LinkSpriteAnimationHandler(texture, rows, columns);
            size = animationHandler.FrameSize;
            currentFrameSet = animationHandler.getFrameSet(AnimationState.RightFacing);
            currentFrameIndex = 0;

            remainingDelay.frame = currentFrameSet[currentFrameIndex].delay;
            remainingDelay.damage = -1;
            isDamaged = false;

            positions.current = startingPosition;
            positions.previous = positions.current;
            
            pauseMovement = false;
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(size.X / 2f, size.Y / 2f);
            Rectangle Destination = new Rectangle((int)positions.current.X, (int)positions.current.Y, (int)(size.X), (int)(size.Y));
            spriteBatch.Draw(spriteTexture, Destination, currentFrameSet[currentFrameIndex].frame, Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.LinkLayer);
        }
       

        public int Update(GameTime gameTime)
        {
            int returnCode = 0;
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (!this.pauseMovement) returnCode = UpdateFrame(timer);

            if (isDamaged)
            {
                remainingDelay.damage -= timer;
                if (remainingDelay.damage < 0)
                {
                    remainingDelay.damage = DamageConstants.DamageMaskDelay;
                    maskHandler.LoadNextMask();
                }
            }
            
            return returnCode;
        }

        private int UpdateFrame(float timer)
        {
            int returnCode = 0;

            remainingDelay.frame -= timer;

            if (remainingDelay.frame <= 0)
            {
                currentFrameIndex++;
                if (currentFrameIndex >= currentFrameSet.Length)
                {
                    currentFrameIndex = 0;
                    returnCode = -1;
                }

                remainingDelay.frame = currentFrameSet[currentFrameIndex].delay;
            }

            return returnCode;
        }

        public void setFrameSet(AnimationState animState)
        {
            currentFrameSet = animationHandler.getFrameSet(animState);
            currentFrameIndex = 0;
            remainingDelay.frame = currentFrameSet[currentFrameIndex].delay;
	    }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public ref Rectangle Box
        {
            get
            {
                int width = (int)(size.X * LinkConstants.hitboxSizeModifier);
                int height = (int)(size.Y * LinkConstants.hitboxSizeModifier);
                Rectangle Destination = new Rectangle((int)positions.current.X, (int)positions.current.Y, width, height);
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
            }
        }

        public Vector2 Center
        {
            get => positions.current;
            set => positions.current = value;
        }

        public Vector2 PreviousCenter
        {
            get => positions.previous;
            set => positions.previous = value;
        }

        public Texture2D Texture
        {
            get => spriteTexture;
        }

        public Vector2 Size
        {
            get => size;
        }

        public bool Damaged
        {
            get => isDamaged;
            set 
	        {
                isDamaged = value;
                remainingDelay.damage = DamageConstants.DamageMaskDelay;
                maskHandler.Reset();
            }
        }

        public bool PauseMovement
        {
            set => this.pauseMovement = value;
        }

        public DamageMaskHandler DamageMaskHandler
        {
            get => this.maskHandler;
        }

        public SingleMaskHandler DeathMaskHandler
        {
            get => this.deathMaskHandler;
        }
    }
}