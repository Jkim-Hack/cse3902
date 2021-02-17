using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Interfaces
{
    // Reciever
    public interface ISprite
    {
        
	    public void Draw();
	    public void Erase();
        public void Update(GameTime gameTime);
        public Vector2 StartingPosition { get; set; }
        public Vector2 Center { get; set; }
        public Texture2D Texture { get; }
    }
}
 