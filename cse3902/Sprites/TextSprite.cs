using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static cse3902.Interfaces.ISprite;

namespace cse3902.Sprites
{
    public class TextSprite : ISprite
    {
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private String text;

        private Vector2 center;
        private Vector2 startingPosition;

        public TextSprite(SpriteBatch spriteBatch, SpriteFont spriteFont, String text)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            this.text = text;
            center = new Vector2(200, 350);
        }

        public Vector2 StartingPosition { get => startingPosition; set => startingPosition = value; }
        public Vector2 Center { get => center; set => center = value; }

        public Texture2D Texture
        {
            get => spriteFont.Texture; 
        }

        public void Draw()
        {
	        spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, text, center, Color.Black); 
	        spriteBatch.End(); 
	    }
	    
	    public void Erase()
        {
            spriteFont.Texture.Dispose();
        }

        public int Update(GameTime gameTime)
        {
            return 0;
        }
    }
}
