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

        public TextSprite(SpriteBatch spriteBatch, SpriteFont spriteFont, String text)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            this.text = text;
            center = new Vector2(200, 350);
        }
        public void Draw()
        {
            spriteBatch.DrawString(spriteFont, text, center, Color.Black);
        }

        public Vector2 Center { get => center; set => center = value; }

        public Texture2D Texture
        {
            get => spriteFont.Texture; 
        }

        public Rectangle Box => throw new NotImplementedException();

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
