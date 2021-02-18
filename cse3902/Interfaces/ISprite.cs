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
        public void Update(GameTime gameTime, onAnimCompleteCallback animationCallback);
        public Vector2 StartingPosition { get; set; }
        public Vector2 Center { get; set; }
        public Texture2D Texture { get; }
    }
}
 