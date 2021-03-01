using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Interfaces
{
    // Reciever
    public interface ISprite
    {  
        public delegate void onAnimCompleteCallback();
	    public void Draw();
	    public void Erase();
        public int Update(GameTime gameTime);
        public Vector2 Center { get; set; }
        public Texture2D Texture { get; }
        //public Rectangle Box { get; }
    }
}
 