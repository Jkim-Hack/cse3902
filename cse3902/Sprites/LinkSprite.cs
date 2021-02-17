using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;
using System.Collections.Generic;

namespace cse3902.Sprites
{
    public class LinkSprite : ISprite
    {
        public enum FrameIndex
        {
            LeftFacing = 0,
            LeftRunning = 1,
            RightFacing = 2,
            RightRunning = 3,
            UpFacing = 4,
            UpRunning = 5,
            DownFacing = 6,
            DownRunning = 7,
            LeftAttack = 8,
            RightAttack = 9,
            UpAttack = 10,
            DownAttack = 11
        };

        private Dictionary<FrameIndex, int[]> frameSets;

        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 center;
        private Vector2 startingPosition;
        //private LinkSword weapon

        private Rectangle currentFrame;
        private int totalFrames;
        private Rectangle[] frames;
        private int frameWidth;
        private int frameHeight;
        private int frameIndex;
        private int damageOffset;
        private int damage;

        private int[] currentFrameSet;
        public delegate void onAnimCompleteCallback();
        private onAnimCompleteCallback callback;

        private const float delay = 0.2f;
        private float remainingDelay;

        public LinkSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Vector2 startingPosition)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;
            remainingDelay = delay;

            distributeFrames(columns);
            generateFrameSets();

            totalFrames = rows * columns;
            currentFrameSet = frameSets[FrameIndex.LeftFacing];
            frameIndex = 0;
            currentFrame = frames[currentFrameSet[frameIndex]];

            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            frames = new Rectangle[totalFrames];
            damageOffset = columns * rows / 4;
            damage = -1;

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
            frameSets = new Dictionary<FrameIndex, int[]>()
            {
                { FrameIndex.LeftFacing, new int[] {0}},
                { FrameIndex.LeftRunning, new int[] { 0, 1 } },
                { FrameIndex.RightFacing, new int[] {2}},
                { FrameIndex.RightRunning, new int[] { 2, 3 } },
                { FrameIndex.UpFacing, new int[] {4}},
                { FrameIndex.UpRunning, new int[] { 4, 5 } },
                { FrameIndex.DownFacing, new int[] {6}},
                { FrameIndex.DownRunning, new int[] { 6, 8 } },
                { FrameIndex.LeftAttack, new int[] {9, 9, 1, 0}},
                { FrameIndex.RightAttack, new int[] { 10, 10, 1, 0 } },
                { FrameIndex.UpAttack, new int[] {11, 11, 5, 6}},
                { FrameIndex.DownAttack, new int[] {12, 12, 5, 6}},
            };
        }

        public void Draw()
        {
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, frameWidth, frameHeight);

            spriteBatch.Begin();
            spriteBatch.Draw(spriteTexture, Destination, currentFrame, Color.White);
            spriteBatch.End();
        }

        
	    public void Update(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingDelay -= timer;

            if (remainingDelay <= 0)
            {
                frameIndex++;
                if (currentFrameSet.Length <= frameIndex)
                {
                    frameIndex = 0;
                    callback();
                }
                int frameNum = currentFrameSet[frameIndex];
                if (damage > 0)
                {
                    damage = (damage + 1) % 4;
                    frameNum += damage * damageOffset;
                }
                currentFrame = frames[frameNum];
            }
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

        public onAnimCompleteCallback Callback
        {
            set => callback = value;
        }
    }
}