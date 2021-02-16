using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;

namespace cse3902.Sprites
{
    public class LinkSprite : ISprite
    {
        public enum FrameIndex
        {
            LeftFacing = 0,
            RightFacing = 2,
            UpFacing = 4,
            DownFacing = 6,
            ItemLeft = 8,
            ItemRight = 9,
            ItemUp = 11,
            ItemDown = 10
        };
        //this is an idea
        //private Dictionary<FrameIndex, Rectangle[]> frameSets;

        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
 	    private Vector2 center;
        private Vector2 startingPosition;
        //private LinkSword weapon

	    private int currentFrame;
        private int totalFrames;
        private Rectangle[] frames;
        private int frameWidth;
        private int frameHeight;
        private int startingFrameIndex;
        private int endingFrameIndex;
        private bool spriteLock;


        private Rectangle[] currentFrameSet;

        private const float delay = 0.2f;
        private float remainingDelay;
        
        public LinkSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Vector2 startingPosition)
        { 
            this.spriteBatch = spriteBatch;
	        spriteTexture = texture;
            remainingDelay = delay;

            distributeFrames(columns);
            //idea instead of starting and ending indexes
            //generateFrameSets();

	        totalFrames = rows * columns;
            currentFrame = 0;
            startingFrameIndex = (int)FrameIndex.RightFacing;
            endingFrameIndex = startingFrameIndex + 2;
            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            frames = new Rectangle[totalFrames];
            spriteLock = false;
            
	        this.startingPosition = startingPosition;
            center = startingPosition;
            
	    }

        private void distributeFrames(int columns)
        {
            for (int i = 0; i < totalFrames; i++) { 
		        int row = (int)((float)i / (float)columns);
		        int column = i % columns;
                frames[i] = new Rectangle(frameWidth * column, frameHeight * row, frameWidth, frameHeight);
	        }
        }

        /*Idea instead of currentframeindex and endingFrameIndex
        private void generateFrameSets()
        {
            frameSets = new Dictionary<FrameIndex, Rectangle[]>()
            {
                { FrameIndex.LeftFacing, new Rectangle[] {frames[0] }},
                { FrameIndex.LeftRunning, new Rectangle[] { frames[0], frames[1] } },
                { FrameIndex.LeftRunning, new Rectangle[] { frames[0], frames[1] } }
            };
        }*/

        public void Draw()
        {
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, frameWidth, frameHeight);
           
	        spriteBatch.Begin();
	        spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White);
	        spriteBatch.End(); 
        }

        
	    public void Update(GameTime gameTime, onAnimCompleteCallback animCompleteCallback)
        {
            var timer = (float) gameTime.ElapsedGameTime.TotalSeconds;
            
	        currentFrame++;
            if (currentFrame == endingFrameIndex)
            {
                animCompleteCallback();
                currentFrame = startingFrameIndex;
            }
            remainingDelay = delay;
        }

        public void Erase()
        {
            spriteTexture.Dispose();
	    }
        
	    public Vector2 StartingPosition
        {
            get => startingPosition;
            set 
	        { 
		        startingPosition = value;
                Center = value;
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
                if (startingFrameIndex != value)
                {

                    remainingDelay = delay;
                    startingFrameIndex = value;
                    if (value > 7)
                    {
                        endingFrameIndex = value + 2;
                    }
                    else
                    {
                        endingFrameIndex = value + 1;
                    }
                }
            }
        }
      
        public Rectangle Bounds
        {
            get => spriteTexture.Bounds;
        }
    }
}
