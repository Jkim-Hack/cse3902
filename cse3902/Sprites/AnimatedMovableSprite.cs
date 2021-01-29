using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Sprites
{
    public class AnimatedMovableSprite : ISprite
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

        private const float delay = 0.2f;
        private float remainingDelay;

        public AnimatedMovableSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns)
        {
            this.spriteBatch = spriteBatch;
	        spriteTexture = texture;
            speed = 100f;
            direction = new Vector2(1, 0);
            remainingDelay = delay;
            startingPosition = new Vector2(0, 200);
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

        public AnimatedMovableSprite(SpriteBatch spriteBatch, Texture2D texture, int rows, int columns, Vector2 startingPosition)
        { 
            this.spriteBatch = spriteBatch;
	        spriteTexture = texture;
            speed = 100f;
            direction = new Vector2(1, 0);
            remainingDelay = delay;
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
		        int row = (int)((float)i / (float)columns);
		        int column = i % columns;
                frames[i] = new Rectangle(frameWidth * column, frameHeight * row, frameWidth, frameHeight);
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
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, frameWidth, frameHeight);
           
	        spriteBatch.Begin();
	        spriteBatch.Draw(spriteTexture, Destination, frames[currentFrame], Color.White);
	        spriteBatch.End(); 
        }

        public void Update(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingDelay -= timer;

            if (remainingDelay <= 0)
            {
		        currentFrame++;
		        if (currentFrame == totalFrames) {
			        currentFrame = 0;
		        }
                remainingDelay = delay;
            }
            if (center.X > 800 + frameWidth)
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
