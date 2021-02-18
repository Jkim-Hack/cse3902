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
        public enum FrameIndex
        {
            LeftFacing = 0,
            LeftRunning = 1,
            RightFacing = 2,
            RightRunning = 3,
            UpFacing = 4,
        };

        private Dictionary<FrameIndex, int[]> frameSets;

        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 center;
        private Vector2 startingPosition;
        //private LinkSword weapon

        private int totalFrames;
        private Rectangle[] frames;
        private int frameWidth;
        private int frameHeight;
        private int frameIndex;
        private int damageOffset;
        private int damage;


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
            damageOffset = columns * rows / 4;
            damage = -1;

            this.startingPosition = startingPosition;
            center = startingPosition;
            
	        distributeFrames();
        }

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
            spriteBatch.End();
        }

        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingDelay -= timer;

            if (remainingDelay <= 0)
            {
                }
            }
        }

        {
        }

        {
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

        {
            set
            {
        }

        public Rectangle Bounds
        {
            get => spriteTexture.Bounds;
        }

        public bool Damaged
        {
            set
            {  
                if(value && damage < 0)
                    this.damage = 0;
                if (!value)
                    this.damage = -1;
            }
        }
        public onAnimCompleteCallback Callback
        {
            set => callback = value;
        }
    }
}