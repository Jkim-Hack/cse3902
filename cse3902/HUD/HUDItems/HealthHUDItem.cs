using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.HUD.HUDItems
{
    public class HealthHUDItem : IHUDItem
    {
        private Vector2 center;
        private Texture2D spriteTexture;
        private Rectangle box;
        private Vector2 size;
        private SpriteBatch spriteBatch;

        public HealthHUDItem(Game1 game, Texture2D UITexture, Vector2 centerPosition)
        {
            center = centerPosition;
            spriteTexture = UITexture;
            size = new Vector2(spriteTexture.Bounds.Width, spriteTexture.Bounds.Height);
            box = new Rectangle((int)(center.X - (size.X/2)), (int)(center.Y - (size.Y/2)), (int)size.X, (int)size.Y);
            spriteBatch = game.SpriteBatch;
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

        public ref Rectangle Box
        {
            get => ref box;
        }

        public void Draw()
        { 
            Vector2 origin = new Vector2(size.X / 2f, size.Y / 2f);
            Rectangle Destination = new Rectangle((int)center.X, (int)center.Y, (int)(size.X), (int)(size.Y));
            spriteBatch.Draw(spriteTexture, Destination, Color.White);
        }

        public void Erase()
        {
            throw new NotImplementedException();
        }

        public int Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
