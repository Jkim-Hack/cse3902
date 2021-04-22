using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cse3902.Rooms;
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
            GameWon,
            Death
        };

        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private (Vector2 current, Vector2 previous) positions;
        private Vector2 size;

        private (Rectangle frame, float delay)[] currentFrameSet;
        private int currentFrameIndex;
        private LinkSpriteAnimationHandler animationHandler;
        private float remainingFrameDelay;

        private Rectangle destination;

        private bool pauseMovement;

        private LinkEffectsManager linkEffectsManager;

        public LinkSprite(SpriteBatch spriteBatch, Texture2D texture, Texture2D rectangle, int rows, int columns, GenericTextureMask maskHandler, SingleMaskHandler singleMaskHandler, Vector2 startingPosition)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;

	        animationHandler = new LinkSpriteAnimationHandler(texture, rows, columns);
            size = animationHandler.FrameSize;
            currentFrameSet = animationHandler.getFrameSet(AnimationState.RightFacing);
            currentFrameIndex = 0;

            remainingFrameDelay = currentFrameSet[currentFrameIndex].delay;

            positions.current = startingPosition;
            positions.previous = positions.current;
            
            pauseMovement = false;

            linkEffectsManager = new LinkEffectsManager(spriteBatch, rectangle, maskHandler, singleMaskHandler);
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(size.X / 2f, size.Y / 2f);
            Rectangle Destination = new Rectangle((int)positions.current.X, (int)positions.current.Y, (int)(size.X), (int)(size.Y));
            spriteBatch.Draw(spriteTexture, Destination, currentFrameSet[currentFrameIndex].frame, Color.White, 0, origin, SpriteEffects.None, linkEffectsManager.IsTopLayer ? 0 : SpriteUtilities.LinkLayer);
            linkEffectsManager.Draw();
	    }
 
        public int Update(GameTime gameTime)
        {
            int returnCode = 0;
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (!this.pauseMovement) returnCode = UpdateFrame(timer);

            linkEffectsManager.Update(gameTime);
            
            return returnCode;
        }

        private int UpdateFrame(float timer)
        {
            int returnCode = 0;

            remainingFrameDelay -= timer;

            if (remainingFrameDelay <= 0)
            {
                currentFrameIndex++;
                if (currentFrameIndex >= currentFrameSet.Length)
                {
                    currentFrameIndex = 0;
                    returnCode = -1;
                }

                remainingFrameDelay = currentFrameSet[currentFrameIndex].delay;
            }

            return returnCode;
        }

        public void setFrameSet(AnimationState animState)
        {
            currentFrameSet = animationHandler.getFrameSet(animState);
            currentFrameIndex = 0;
            remainingFrameDelay = currentFrameSet[currentFrameIndex].delay;
	    }

        public void SetGameWon(bool state)
        {
            linkEffectsManager.SetGameWon(state);
        }

        public void SetDeath()
        {
            linkEffectsManager.SetDeath();
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
            get => linkEffectsManager.Damaged;
            set 
	        {
                linkEffectsManager.Damaged = value;
            }
        }

        public bool PauseMovement
        {
            set => this.pauseMovement = value;
        }
	    
	    public GenericTextureMask DamageMaskHandler
        {
            get => linkEffectsManager.DamageMaskHandler;
        }

        public SingleMaskHandler DeathMaskHandler
        {
            get => linkEffectsManager.DeathMaskHandler;
        }
    }
}