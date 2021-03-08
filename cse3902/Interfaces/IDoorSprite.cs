using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace cse3902.Interfaces
{
    public interface IDoorSprite
    {
        public void Draw();
        public void Erase();
        public int Update(GameTime gameTime);
        public Vector2 Center { get; set; }
        public List<Rectangle> Boxes { get; }
    }
}
