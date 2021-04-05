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
            GameWon,
            Death
        };

        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Texture2D rectangleTexture;

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

        private bool gameWon;
        private int gameWinFlashDelay;
        private int gameWinRectangleWidth;

        private bool death;
        private int deathColorChangeDelay;
        private Color deathColor;

        public LinkSprite(SpriteBatch spriteBatch, Texture2D texture, Texture2D rectangle, int rows, int columns, DamageMaskHandler maskHandler, SingleMaskHandler singleMaskHandler, Vector2 startingPosition)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;
            rectangleTexture = rectangle;
            rectangleTexture.SetData(new Color[] { Color.White });

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

            gameWon = false;
            gameWinFlashDelay = -50;
            gameWinRectangleWidth = 0;

            deathColorChangeDelay = 0;
            deathColor = Color.Red;
        }

        public void Draw()
        {
            Vector2 origin = new Vector2(size.X / 2f, size.Y / 2f);
            Rectangle Destination = new Rectangle((int)positions.current.X, (int)positions.current.Y, (int)(size.X), (int)(size.Y));
            spriteBatch.Draw(spriteTexture, Destination, currentFrameSet[currentFrameIndex].frame, Color.White, 0, origin, SpriteEffects.None, (gameWon || death) ? 0 : SpriteUtilities.LinkLayer);

            int roomWidth = DimensionConstants.OriginalWindowWidth;
            int roomHeight = DimensionConstants.GameplayHeight / 3;
            DrawGameWinAnim(roomWidth, roomHeight);
            DrawDeathAnim(roomWidth, roomHeight);
        }

        private void DrawGameWinAnim(int width, int height)
        {
            if (gameWinFlashDelay > 0 && gameWinFlashDelay <= 60 && ((gameWinFlashDelay / 5) % 2 == 0))
            {
                DrawRectangle(Color.White * 0.75f, new Rectangle(width * 5, height * 1, width, height), SpriteUtilities.GameWonLayer);
            }

            if (gameWon)
            {
                DrawRectangle(Color.Black, new Rectangle(width * 5, height * 1, gameWinRectangleWidth, height), SpriteUtilities.GameWonLayer);
                DrawRectangle(Color.Black, new Rectangle((width * 6) - gameWinRectangleWidth, height * 1, gameWinRectangleWidth, height), SpriteUtilities.GameWonLayer);
            }
        }

        private void DrawDeathAnim(int width, int height)
        {
            if (deathColorChangeDelay > 0)
            {
                deathColorChangeDelay--;
                DrawRectangle(deathColor * 0.75f, new Rectangle(0, 0, width * 6, height * 6), SpriteUtilities.DeathEffectLayer);
            }
        }

        private void DrawRectangle(Color color, Rectangle destination, float layer)
        {
            spriteBatch.Draw(rectangleTexture, destination, null, color, 0, new Vector2(), SpriteEffects.None, layer);
        }
     
        public int Update(GameTime gameTime)
        {
            int returnCode = 0;
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (!this.pauseMovement) returnCode = UpdateFrame(timer);

            if (isDamaged && !maskHandler.Disabled)
            {
                remainingDelay.damage -= timer;
                if (remainingDelay.damage < 0)
                {
                    remainingDelay.damage = DamageConstants.DamageMaskDelay;
                    maskHandler.LoadNextMask();
                }
            }

            if (gameWinFlashDelay > -50) gameWinFlashDelay--;
            if (gameWon && gameWinFlashDelay == -50) gameWinRectangleWidth++;
            
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

        public void SetGameWon()
        {
            gameWon = true;
            gameWinFlashDelay = 100;
        }

        public void SetDeath()
        {
            death = true;
            deathColorChangeDelay = 135;
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