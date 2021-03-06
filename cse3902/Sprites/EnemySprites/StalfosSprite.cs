﻿using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Constants;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;

namespace cse3902.Sprites.EnemySprites
{
    public class StalfosSprite: ISprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;

        private Vector2 center;

        private int currentFrame;
        private Rectangle[] frames;
        private Vector2 size;

        private int startingFrameIndex;
        private int endingFrameIndex;

        private float remainingDelay;

        private bool isAttacking;

        private Rectangle destination;

        public StalfosSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Vector2 startingPosition)
        {
            this.spriteBatch = spriteBatch;
            spriteTexture = texture;
            remainingDelay = MovementConstants.StalfosDelay;

            currentFrame = 0;

            startingFrameIndex = 0;
            endingFrameIndex = 2;

            size.X = spriteTexture.Width / columns;
            size.Y = spriteTexture.Height / rows;
            frames = SpriteUtilities.distributeFrames(columns, rows, (int)size.X, (int)size.Y);

            center = startingPosition;

            isAttacking = false;

        }

        public void Draw()
        {
            Vector2 origin = new Vector2(size.X / 2f, size.Y / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(size.X), (int)(size.Y));
            spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White, 0, origin, SpriteEffects.None, SpriteUtilities.EnemyLayer);
        }

        public int Update(GameTime gameTime)
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
                remainingDelay = MovementConstants.StalfosDelay;
            }
            return 0;
        }

        public ref Rectangle Box
        {
            get
            {
                int width = (int)(size.X);
                int height = (int)(size.Y);
                Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, width, height);
                Destination.Offset(-Destination.Width / 2, -Destination.Height / 2);
                this.destination = Destination;
                return ref destination;
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
    

