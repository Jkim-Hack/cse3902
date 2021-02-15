using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Sprites.EnemySprites
{
    public class AquamentusSprite: ISprite
    {

        public enum FrameIndex
        {
            RightFacing = 0,
            LeftFacing = 2
            
        };

        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private ISprite fireball1;
        private ISprite fireball2;
        private ISprite fireball3;

        private Vector2 center;
        private Vector2 startingPosition;

        private int currentFrame;
        private int totalFrames;
        private Rectangle[] frames;
        private int frameWidth;
        private int frameHeight;

        private int startingFrameIndex;
        private int endingFrameIndex;

        private const float delay = 0.2f;
        private float remainingDelay;

        private bool isAttacking;

        public AquamentusSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Vector2 startingPosition)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;
            remainingDelay = delay;

            LoadFireballs();
            
            totalFrames = rows * columns;
            currentFrame = 0;
            startingFrameIndex = (int)FrameIndex.LeftFacing;
            endingFrameIndex = startingFrameIndex + 2;
            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            frames = new Rectangle[totalFrames];

            this.startingPosition = startingPosition;
            center = startingPosition;

            isAttacking = false;

            DistributeFrames(columns);

        }

        private void DistributeFrames(int columns)
        {
            for (int i = 0; i < totalFrames; i++)
            {
                int row = (int)((float)i / (float)columns);
                int column = i % columns;
                frames[i] = new Rectangle(frameWidth * column, frameHeight * row, frameWidth, frameHeight);
            }

            
        }

        private void LoadFireballs()
        {
            //TODO: get proper texture from sprite factory

            //TODO: tweak this to position the fireballs at the mouth of aquamentus

            

            Vector2 startingPosition = new Vector2(0.0f, 0.0f);

            //get a normalized direction vectors for the fireballs

            Vector2 direction1 = Vector2.Normalize(new Vector2(-1.0f, .5f));
            Vector2 direction2 = Vector2.Normalize(new Vector2(-1.0f, .0f));
            Vector2 direction3 = Vector2.Normalize(new Vector2(-1.0f, -.5f));

            if (this.StartingFrameIndex == (int)AquamentusSprite.FrameIndex.RightFacing)
            {
                direction1 = Vector2.Normalize(new Vector2(1.0f, .5f));
                direction2 = Vector2.Normalize(new Vector2(1.0f, .0f));
                direction3 = Vector2.Normalize(new Vector2(1.0f, -.5f));

                //starting position will also need to be changed
            }

            fireball1 = new FireballSprite(spriteBatch, spriteTexture, startingPosition, direction1);
            fireball2 = new FireballSprite(spriteBatch, spriteTexture, startingPosition, direction2);
            fireball3 = new FireballSprite(spriteBatch, spriteTexture, startingPosition, direction3);
        }

       

        public void Draw()
        {
            
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, frameWidth, frameHeight);

            spriteBatch.Begin();
            spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White);
            spriteBatch.End();

            if (isAttacking)
            {
                fireball1.Draw();
                fireball2.Draw();
                fireball3.Draw();
            }
        }

        public void Erase()
        {
            spriteTexture.Dispose();
        }

        public void Update(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingDelay -= timer;

            if (remainingDelay <= 0)
            {
                currentFrame++;
                if (currentFrame == endingFrameIndex)
                {
                    currentFrame = startingFrameIndex;
                }
                remainingDelay = delay;
            }

            if (isAttacking)
            {
                fireball1.Update(gameTime);
                fireball2.Update(gameTime);
                fireball3.Update(gameTime);

            }
        }

        // I question the need for this vector
        public Vector2 StartingPosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
            get => startingFrameIndex;
            set
            {
                startingFrameIndex = value;
                endingFrameIndex = value + 2;
            }
        }

        public bool IsAttacking
        {
            get => isAttacking;
            set
            {
                isAttacking = value;
            }
        }
    }
}
