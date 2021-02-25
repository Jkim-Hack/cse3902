﻿using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;

namespace cse3902.Sprites.EnemySprites
{
    public class WallMasterSprite : ISprite
    {
        public enum FrameIndex
        {
            LeftUpFacing = 0,
            RightUpFacing = 2,
            LeftDownFacing = 4,
            RightDownFacing = 6
        };

        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private Vector2 center;

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

        public WallMasterSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Vector2 startingPosition)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;
            remainingDelay = delay;

            totalFrames = rows * columns;
            currentFrame = 0;

            startingFrameIndex = (int)FrameIndex.LeftUpFacing;
            endingFrameIndex = startingFrameIndex + 2;

            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            frames = new Rectangle[totalFrames];

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

        public void Draw()
        {

            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, frameWidth, frameHeight);

            spriteBatch.Begin();
            spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White);
            spriteBatch.End();
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
            get => startingFrameIndex;
            set
            {
                startingFrameIndex = value;
                endingFrameIndex = value + 2;

                if (currentFrame >= endingFrameIndex || currentFrame < startingFrameIndex) { 

                    currentFrame = value;
                }
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
