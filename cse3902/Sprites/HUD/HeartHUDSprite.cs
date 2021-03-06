﻿using System;
using cse3902.Interfaces;
using cse3902.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Sprites
{
    public class HeartHUDSprite : ISprite
    {
        private bool isHalf; // if !isHalf && !isEmpty then its full
        private bool isEmpty;

        private Texture2D texture;
        private SpriteBatch spriteBatch;
        private Vector2 origin;
        private Vector2 center;

        private Rectangle destination;

        private int rows;
        private int columns;

        private int frameWidth;
        private int frameHeight;
        private Rectangle[] frames;

        private enum HeartType
        {
            EMPTY = 0,
            HALF = 1,
            FULL = 2
        }

        private HeartType currentFrame;

        // Texture should include empty, half, full sprites
        public HeartHUDSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 origin)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.origin = origin;

            this.rows = 1;
            this.columns = 3;
            frameWidth = texture.Width / columns;
            frameHeight = texture.Height / rows;
            frames = SpriteUtilities.distributeFrames(columns, rows, frameWidth, frameHeight);
        }
	    
	    public bool Full
        {
            get => !isHalf && !isEmpty;
            set 
	        {
                if (value)
                {
                    isHalf = false;
                    isEmpty = false;
                    currentFrame = HeartType.FULL;
                }
                else if (isHalf && !isEmpty) currentFrame = HeartType.HALF;
                else if (!isHalf && isEmpty) currentFrame = HeartType.EMPTY;
                else currentFrame = HeartType.FULL; 
            }
        }
        
	    public bool Half
        {
            get => isHalf;
            set 
	        {
                isHalf = value;
                isEmpty = false;
                if (isHalf) currentFrame = HeartType.HALF;
                else if (isHalf && !isEmpty) currentFrame = HeartType.HALF;
                else if (!isHalf && isEmpty) currentFrame = HeartType.EMPTY;
                else currentFrame = HeartType.FULL; 
            }
        }
        
	    public bool Empty
        {
            get => isEmpty;
	        set 
	        {
                isEmpty = value;
                if (isEmpty)
                {
                    isHalf = false;
                    currentFrame = HeartType.EMPTY;
		        } 
                else if (isHalf && !isEmpty) currentFrame = HeartType.HALF;
                else if (!isHalf && isEmpty) currentFrame = HeartType.EMPTY;
                else currentFrame = HeartType.FULL; 
            }
        }

        public Vector2 Center 
	    { 
	        get => center;
            set => center = value;
		}

        public Texture2D Texture
        {
            get => texture;
        }

        public ref Rectangle Box
        {
            get
            {
                Rectangle Destination = new Rectangle((int)origin.X, (int)origin.Y, frameWidth, frameHeight);
                this.destination = Destination;
                return ref destination;
            }
        }

        public void Draw()
        {
            Rectangle Destination = new Rectangle((int)this.origin.X, (int)this.origin.Y, frameWidth, frameHeight);
            spriteBatch.Draw(texture, Destination, frames[(int)currentFrame], Color.White, 0, new Vector2(0, 0), SpriteEffects.None, HUDUtilities.HeartsLayer);
        }

        public int Update(GameTime gameTime)
        {
            // Does nothing just displays
            return 0;
        } 
    }
}
