using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Sprites
{
    public class StaticMovableSprite : ISprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D spriteTexture;
 	    private Vector2 center;
        private Vector2 startingPosition;
        public float speed { get; set; }
        public Vector2 direction { get; set; }

	    private int rows;
        private int columns;
	    private int currentFrame;
        private int totalFrames;
        private Rectangle[] frames;
        private int frameWidth;
        private int frameHeight;


        public StaticMovableSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns)
        {
            this.spriteBatch = spriteBatch;
	        spriteTexture = texture;
            speed = 100f;
            direction = new Vector2(0, 1);
            startingPosition = new Vector2(400, 0);
            center = startingPosition;
            this.rows = rows;
            this.columns = columns;
            currentFrame = 0;
            totalFrames = rows * columns;
            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            frames = new Rectangle[totalFrames];
            distributeFrames();
	    }

        public StaticMovableSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Vector2 startingPosition)
        { 
            this.spriteBatch = spriteBatch;
	        spriteTexture = texture;
            speed = 100f;
            direction = new Vector2(0, 1);
            this.rows = rows;
            this.columns = columns;
            currentFrame = 0;
            totalFrames = rows * columns;
            this.startingPosition = startingPosition;
            center = startingPosition;
            frameWidth = spriteTexture.Width / columns;
            frameHeight = spriteTexture.Height / rows;
            frames = new Rectangle[totalFrames];
            distributeFrames();
	    }
       
    	private void distributeFrames()
        {
            for (int i = 0; i < totalFrames; i++) { 
		        int Row = (int)((float)i / (float)columns);
		        int Column = i % columns;
                frames[i] = new Rectangle(frameWidth * Column, frameHeight * Row, frameWidth, frameHeight);
	        }
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

        public void Draw()
        {
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, frameWidth,frameHeight);
           
	        spriteBatch.Begin();
	        spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White);
	        spriteBatch.End(); 
        }

        public void Update(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (center.Y > 450 + frameHeight)
            {
                center = startingPosition;
            }
            center += direction * speed * timer;

        }

        public void Erase()
        {
           spriteTexture.Dispose();
	    }
    }
}
