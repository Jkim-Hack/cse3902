using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.HUD.HUDItems
{
    public class HealthHUDItem : IHUDItem
    {
        private Vector2 center;
        private Texture2D texture;

        public HealthHUDItem(Texture2D UITexture, Vector2 centerPosition)
        {
            center = centerPosition;
            texture = UITexture; 
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
            get; set;
        }

        public void Draw()
        {
            throw new NotImplementedException();
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
